using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoGenPaper.Common;
using AutoGenPaper.Mvc;

namespace AutoGenPaper.Website.Controllers
{
	[Authorize(Roles = "教师")]
	public class QuestionMgrController : Controller
    {
        private AGPDataContext db = new AGPDataContext();

		private void AddViewData()
		{
			var t = EnumHelper.GetFields(typeof(AGPDefine.QuestionType)).ToList()
				.ConvertAll<SelectListItem>(a => new SelectListItem()
				{
					Text = EnumHelper.GetEnumDescription(a),
					Value = a.GetRawConstantValue().ToString()
				});
			var c = db.Catalogs.ToList()
				.ConvertAll<SelectListItem>(a => new SelectListItem()
				{
					Text = a.Info.Name,
					Value = a.CatalogId.ToString()
				});
			var d = Enumerable.Range(1, 10).ToList()
				.ConvertAll<SelectListItem>(a => new SelectListItem()
				{
					Text = a.ToString(),
					Value = a.ToString()
				});
			t.First().Selected = true;
			c.First().Selected = true;
			d.First().Selected = true;
			ViewData["Type"] = t;
			ViewData["Catalog"] = c;
			ViewData["Difficulty"] = d;
		}

        //
        // GET: /QuestionMgr/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /QuestionMgr/Details/5

        public ActionResult Details(int id = 0)
        {
			if (db.Questions.Find(id).State != (int)AGPDefine.CommitType.Normal)
			{
				LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Warning, AGPDefine.LogEventType.NoAccess, AGPDefine.LogObjectType.Question, null);
				return HttpNotFound("越权访问");
			}
			ViewBag.Id = id;
            return View();
        }

        //
        // GET: /QuestionMgr/Create

        public ActionResult Create()
        {
			QuestionProxyModel m = new QuestionProxyModel()
			{
				Name = "Example",
				Points = 1,
				Answer = "AB",
				Option = "abcd$abcd"
			};
			AddViewData();
            return View(m);
        }

        //
        // POST: /QuestionMgr/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionProxyModel question)
        {
			AddViewData();
			bool success = false;
			int id = 0;
            if (ModelState.IsValid)
            {
				success = QuestionHelper.AddQuestion(db, User.Identity.Name, question, ModelState, out id);
				if (success)
				{
					if (id != 0)
					{
						LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Insert, AGPDefine.LogObjectType.Question, null);
						return RedirectToAction("Index");
					}
				}
            }

			return View(question);
        }

        //
        // GET: /QuestionMgr/Edit/5

        public ActionResult Edit(int id = 0)
        {
			return Content("尚未实现");
		}

        //
        // GET: /QuestionMgr/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Question question = db.Questions.Find(id);
			if (question == null || question.State != (int)AGPDefine.CommitType.Normal)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /QuestionMgr/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
			if (question != null)
			{
				question.State = (int)AGPDefine.CommitType.Delete;
				LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Delete, AGPDefine.LogObjectType.Question, null);
				db.SaveChanges();
			}
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}