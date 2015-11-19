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
	[Authorize(Roles = "审核员")]
	public class QuestionVerifyController : Controller
    {
        private AGPDataContext db = new AGPDataContext();

        //
        // GET: /QuestionVerify/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /QuestionVerify/Edit/5

        public ActionResult Edit(int id)
        {
			var q = db.Questions.Find(id);
			if (q == null || q.State == (int)AGPDefine.CommitType.Normal)
			{
				return HttpNotFound();
			}
			ViewBag.VerifyType = EnumHelper.GetEnumDescriptionEx((AGPDefine.CommitType)q.State);
			ViewBag.Id = id;
            return View();
        }

        //
        // POST: /QuestionVerify/Edit/5

		[ActionName("Edit")]
        [HttpPost]
        public ActionResult Ajax_Edit(int id)
        {
            if (Request.IsAjaxRequest())
            {
				var q = db.Questions.Find(id);
				if (q == null || q.State == (int)AGPDefine.CommitType.Normal)
				{
					LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Warning, AGPDefine.LogEventType.NoAccess, AGPDefine.LogObjectType.Question, "越权审阅");
					return Content("越权操作");
				}
				if (q.State == (int)AGPDefine.CommitType.Insert)
				{
					q.State = (int)AGPDefine.CommitType.Normal;
				}
				else if (q.State == (int)AGPDefine.CommitType.Delete)
				{
					QuestionHelper.DeleteQuestion(db, id);
				}
				db.SaveChanges();
				return Content("已审阅");
            }
			return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}