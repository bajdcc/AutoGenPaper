using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoGenPaper.Common;
using WebMatrix.WebData;
using AutoGenPaper.Mvc;

namespace AutoGenPaper.Website.Controllers
{
	[Authorize(Roles = "管理员")]
	public class UserMgrController : Controller
    {
        private AGPDataContext db = new AGPDataContext();

        //
        // GET: /UserMgr/

        public ActionResult Index()
        {
            return View(db.Users.Where(a => a.State == (int)AGPDefine.CommitType.Normal));
        }

        //
        // GET: /UserMgr/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null || user.State != (int)AGPDefine.CommitType.Normal)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /UserMgr/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
			user.State = (int)AGPDefine.CommitType.Lock;
			LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Lock, AGPDefine.LogObjectType.User, null);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}