using AutoGenPaper.Website.Controllers;
using BootstrapMvcSample.Controllers;
using NavigationRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bootstrap
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			routes.MapNavigationRoute<HomeController>("快速通道", c => c.Empty(), includedInNav: true)
				.AddChildRoute<HomeController>("主页", c => c.Index(), includedInNav: true)
				.AddChildRoute<HomeController>("关于", c => c.About(), includedInNav: true)
				.AddChildRoute<HomeController>("功能", c => c.Features(), includedInNav: true);

			routes.MapNavigationRoute<AccountController>("帐户", c => c.Empty(), includedInNav: true)
				.AddChildRoute<AccountController>("帐户设置", c => c.ShowProfile(), includedInNav: true)
				.AddChildRoute<AccountController>("更改密码", c => c.Manage(null as AccountController.ManageMessageId?), includedInNav: true)
				.AddChildRoute<AccountController>("登录", c => c.Login(""))
				.AddChildRoute<AccountController>("注册", c => c.Register());

			routes.MapNavigationRoute<UserMgrController>("用户管理", c => c.Index())
				.AddChildRoute<UserMgrController>("删除用户", c => c.Delete(0));

			routes.MapNavigationRoute<UserGroupMgrController>("用户组管理", c => c.Details(0))
				.AddChildRoute<UserGroupMgrController>("添加用户组", c => c.Create())
				.AddChildRoute<UserGroupMgrController>("删除用户组", c => c.Delete(0))
				.AddChildRoute<UserGroupMgrController>("查看用户组", c => c.Index())
				.AddChildRoute<UserGroupMgrController>("编辑用户组", c => c.Edit(0));

			routes.MapNavigationRoute<LogController>("日志管理", c => null)
				.AddChildRoute<LogController>("查看日志", c => c.Index());

			routes.MapNavigationRoute<QuestionMgrController>("试题管理", c => c.Details(0))
				.AddChildRoute<QuestionMgrController>("添加试题", c => c.Create())
				.AddChildRoute<QuestionMgrController>("删除试题", c => c.Delete(0))
				.AddChildRoute<QuestionMgrController>("查看试题", c => c.Index())
				.AddChildRoute<QuestionMgrController>("编辑试题", c => c.Edit(0));

			routes.MapNavigationRoute<PaperMgrController>("试卷管理", c => c.Details(0))
				.AddChildRoute<PaperMgrController>("添加试卷", c => c.Create())
				.AddChildRoute<PaperMgrController>("删除试卷", c => c.Delete(0))
				.AddChildRoute<PaperMgrController>("查看试卷", c => c.Index())
				.AddChildRoute<PaperMgrController>("编辑试卷", c => c.Edit(0));

			routes.MapNavigationRoute<PaperMgrController>("试卷反馈", c => null)
				.AddChildRoute<PaperMgrController>("试卷反馈", c => null);

			routes.MapNavigationRoute<QuestionVerifyController>("试题审核", c => null)
				.AddChildRoute<QuestionVerifyController>("未审试题", c => c.Index());

			routes.MapNavigationRoute<PaperVerifyController>("试卷审核", c => null)
				.AddChildRoute<PaperVerifyController>("未审试卷", c => c.Index());
		}
    }
}
