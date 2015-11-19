using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using AutoGenPaper.Common;
using AutoGenPaper.Mvc.Attributes;
using AutoGenPaper.Mvc;

namespace AutoGenPaper.Website.Controllers
{
	[Authorize]
	[InitializeSimpleMembership]
	public class AccountController : Controller
	{
		private readonly string[] roles = new string[]
		{
			"教师",
			"审核员",
			"教研员"
		};

		private void AddViewData()
		{
			List<SelectListItem> items = new List<SelectListItem>();
			items.Add(new SelectListItem() { Text = "请选择", Value = "0", Selected = true });
			for (int i = 0; i <= 2; i++)
			{
				items.Add(new SelectListItem() { Text = roles[i], Value = (i + 1).ToString() });
			}
			ViewData["RoleType"] = items;
		}

		public ActionResult Empty()
		{
			return new EmptyResult();
		}

		[ActionName("Profile")]
		public ActionResult ShowProfile()
		{
			return View();
		}

		//
		// GET: /Account/Login

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			if (Request.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}

			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				using (var db = new AGPDataContext())
				{
					var uc = db.Users.Count(a => a.UserName == model.UserName && a.State == (int)AGPDefine.CommitType.Normal);
					if (uc == 0)
					{
						ModelState.AddModelError("UserName", ErrorCodeToString(MembershipCreateStatus.InvalidUserName));
					}
					else if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
					{
						LogHelper.Log(db, model.UserName, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Login, AGPDefine.LogObjectType.User, null);
						return RedirectToLocal(returnUrl);
					}
				}
			}

			// 如果我们进行到这一步时某个地方出错，则重新显示表单
			ModelState.AddModelError("", "提供的用户名或密码不正确。");
			return View(model);
		}

		//
		// POST: /Account/LogOff

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			using (var db = new AGPDataContext())
			{
				LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Logout, AGPDefine.LogObjectType.User, null);
			}
			WebSecurity.Logout();

			return RedirectToAction("Index", "Home");
		}

		//
		// GET: /Account/Register

		[AllowAnonymous]
		public ActionResult Register()
		{
			AddViewData();
			return View();
		}

		//
		// POST: /Account/Register

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterModel model)
		{
			AddViewData();
			if (ModelState.IsValid)
			{
				// 尝试注册用户
				try
				{
					WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { RealName = model.RealName, State = (int)AGPDefine.CommitType.Normal });
					Roles.AddUsersToRole(new string[] { model.UserName }, roles[model.UserRole - 1]);
					WebSecurity.Login(model.UserName, model.Password);
					using (var db = new AGPDataContext())
					{
						LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Register, AGPDefine.LogObjectType.User, null);
					}
					return RedirectToAction("Index", "Home");
				}
				catch (MembershipCreateUserException e)
				{
					ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
				}
			}

			model.UserName = string.Empty;
			// 如果我们进行到这一步时某个地方出错，则重新显示表单
			return View(model);
		}

		//
		// GET: /Account/Manage

		public ActionResult Manage(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
				message == ManageMessageId.ChangePasswordSuccess ? "你的密码已更改。"
				: message == ManageMessageId.SetPasswordSuccess ? "已设置你的密码。"
				: "";
			ViewBag.ReturnUrl = Url.Action("Manage");
			return View();
		}

		//
		// POST: /Account/Manage

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Manage(LocalPasswordModel model)
		{
			ViewBag.ReturnUrl = Url.Action("Manage");
			if (ModelState.IsValid)
			{
				// 在某些出错情况下，ChangePassword 将引发异常，而不是返回 false。
				bool changePasswordSucceeded;
				try
				{
					changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
				}
				catch (Exception)
				{
					changePasswordSucceeded = false;
				}

				if (changePasswordSucceeded)
				{
					using (var db = new AGPDataContext())
					{
						LogHelper.Log(db, User.Identity.Name, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Update, AGPDefine.LogObjectType.User, "");
					}
					return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
				}
				else
				{
					ModelState.AddModelError("", "当前密码不正确或新密码无效。");
				}
			}

			// 如果我们进行到这一步时某个地方出错，则重新显示表单
			return View(model);
		}

		#region 帮助程序
		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public enum ManageMessageId
		{
			ChangePasswordSuccess,
			SetPasswordSuccess,
		}

		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
			// 状态代码的完整列表。
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "用户名已存在。请输入其他用户名。";

				case MembershipCreateStatus.DuplicateEmail:
					return "该电子邮件地址的用户名已存在。请输入其他电子邮件地址。";

				case MembershipCreateStatus.InvalidPassword:
					return "提供的密码无效。请输入有效的密码值。";

				case MembershipCreateStatus.InvalidEmail:
					return "提供的电子邮件地址无效。请检查该值并重试。";

				case MembershipCreateStatus.InvalidAnswer:
					return "提供的密码取回答案无效。请检查该值并重试。";

				case MembershipCreateStatus.InvalidQuestion:
					return "提供的密码取回问题无效。请检查该值并重试。";

				case MembershipCreateStatus.InvalidUserName:
					return "提供的用户名无效。请检查该值并重试。";

				case MembershipCreateStatus.ProviderError:
					return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

				case MembershipCreateStatus.UserRejected:
					return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

				default:
					return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
			}
		}
		#endregion
	}
}
