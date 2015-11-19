using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoGenPaper.Common;

namespace AutoGenPaper.Website.Controllers
{
	[Authorize(Roles = "管理员")]
    public class LogController : Controller
    {
        private AGPDataContext db = new AGPDataContext();

        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View();
        }
	}
}