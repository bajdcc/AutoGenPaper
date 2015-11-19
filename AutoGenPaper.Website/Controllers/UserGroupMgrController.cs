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
	[Authorize(Roles = "管理员")]
	public class UserGroupMgrController : Controller
    {
        private AGPDataContext db = new AGPDataContext();

        //
        // GET: /UserGroupMgr/

        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        //
        // GET: /UserGroupMgr/Details/5

        public ActionResult Details(int id = 0)
        {
            UserGroup usergroup = db.Groups.Find(id);
            if (usergroup == null)
            {
                return HttpNotFound();
            }
			ViewData["Member"] = usergroup.Users.ToList().ConvertAll<SelectListItem>(a => new SelectListItem() { Text = a.UserName, Value = "" });
			return View(usergroup);
        }

        //
        // GET: /UserGroupMgr/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserGroupMgr/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(usergroup);
                db.SaveChanges();
				LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Insert, AGPDefine.LogObjectType.Group, null);
                return RedirectToAction("Index");
            }
			return View(usergroup);
        }

        //
        // GET: /UserGroupMgr/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserGroup usergroup = db.Groups.Find(id);
            if (usergroup == null)
            {
                return HttpNotFound();
            }
			ViewData["Member"] = db.Users.Include(a => a.UserGroups).ToList()
				.ConvertAll<SelectListItem>(a => new SelectListItem()
				{ Text = a.UserName, Value = a.UserId.ToString(), Selected = a.UserGroups.Contains(usergroup) });
			return View(usergroup);
        }

        //
        // POST: /UserGroupMgr/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
				var group = db.Groups.Find(usergroup.GroupId);
				if (group != null)
				{
					usergroup.Users = new List<User>();
					usergroup.Items.ForEach(
						a =>
						{
							int id;
							if (int.TryParse(a, out id))
							{
								var user = db.Users.Find(id);
								if (user != null)
								{
									usergroup.Users.Add(user);
									user.UserGroups.Add(group);
								}
							}
						});
					db.SaveChanges();
					LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Update, AGPDefine.LogObjectType.Group, null);
				}
            }
			return RedirectToAction("Details", new { id = usergroup.GroupId });
		}

        //
        // GET: /UserGroupMgr/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserGroup usergroup = db.Groups.Find(id);
            if (usergroup == null)
            {
                return HttpNotFound();
            }
            return View(usergroup);
        }

        //
        // POST: /UserGroupMgr/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserGroup usergroup = db.Groups.Find(id);
			usergroup.Users.Clear();
            db.Groups.Remove(usergroup);
            db.SaveChanges();
			LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Delete, AGPDefine.LogObjectType.Group, null);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}