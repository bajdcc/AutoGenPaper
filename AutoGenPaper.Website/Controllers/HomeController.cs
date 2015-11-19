using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoGenPaper.Website.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Empty()
		{
			return new EmptyResult();
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Features()
		{
			return View();
		}
	}
}
