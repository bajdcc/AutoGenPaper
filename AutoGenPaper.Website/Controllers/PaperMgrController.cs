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
	public class PaperMgrController : Controller
    {
        private AGPDataContext db = new AGPDataContext();

		[NonAction]
		private void AddViewData()
		{
			ViewData["DefaultSelection"] = new List<SelectListItem>();
			ViewData["Difficulty"] = new List<SelectListItem>()
			{
				new SelectListItem() { Text = "容易", Value = "0", Selected = true },
				new SelectListItem() { Text = "中等", Value = "1" },
				new SelectListItem() { Text = "困难", Value = "2" },
			};
		}

        //
        // GET: /PaperMgr/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PaperMgr/Details/5

        public ActionResult Details(int id = 0)
        {
			if (db.Papers.Find(id).State != (int)AGPDefine.CommitType.Normal)
			{
				LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Warning, AGPDefine.LogEventType.NoAccess, AGPDefine.LogObjectType.Paper, null);
				return HttpNotFound("越权访问");
			}
			ViewBag.Id = id;
            return View();
        }

        //
        // GET: /PaperMgr/Create

        public ActionResult Create()
        {
			AddViewData();
			return View(new AGPGenPaperStrategiesModel());
        }

        //
        // POST: /PaperMgr/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create(AGPGenPaperStrategiesModel model)
        {
			AddViewData();
			if (ModelState.IsValid)
            {
				var error = ActionValidateHelper.ValidateGenPaperStrategies(db, model, ModelState);
				if (error)
				{
					ModelState.AddModelError("", "操作失败");
					return View(model);
				}

				var id = PaperHelper.GeneratePaper(db, User.Identity.Name, model);
				LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Insert, AGPDefine.LogObjectType.Paper, null);
                return RedirectToAction("Details", new { id = id });
            }

			return View(model);
        }

		//
		// GET: /PaperMgr/

		public ActionResult Edit(int id = 0)
		{
			return Content("尚未实现");
		}

        //
        // GET: /PaperMgr/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Paper paper = db.Papers.Find(id);
			if (paper == null || paper.State != (int)AGPDefine.CommitType.Normal)
            {
                return HttpNotFound();
            }
            return View(paper);
        }

        //
        // POST: /PaperMgr/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			var paper = db.Papers.Find(id);
			if (paper == null || paper.State != (int)AGPDefine.CommitType.Normal)
			{
				return HttpNotFound();
			}

			paper.State = (int)AGPDefine.CommitType.Delete;
			db.SaveChanges();
			LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Delete, AGPDefine.LogObjectType.Paper, null);
			return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}