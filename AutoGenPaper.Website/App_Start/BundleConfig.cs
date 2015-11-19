﻿using System.Web;
using System.Web.Optimization;

namespace AutoGenPaper.Website
{
	public class BundleConfig
	{
		// 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/Js")
						.Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-migrate-{version}.js",
						"~/Scripts/jquery.validate.js",
						"~/Scripts/jquery.validate.unobtrusive.js",
						"~/Scripts/jquery.unobtrusive-ajax.js",
						"~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js")
						.Include(
						"~/Scripts/jquery-ui-{version}.js")
						.Include(
						"~/Scripts/jquery.easyui-{version}.js",
						"~/Scripts/locale/easyui-lang-zh_CN.js")
						.Include(
						"~/Scripts/bootstrap.js"
						));

			// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
			// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Css")
						.Include(
						"~/Content/Site.css",
						"~/Content/themes/bootstrap/easyui.css",
						"~/Content/themes/icon.css",
						"~/Content/themes/color.css")
						.Include(
						"~/Content/bootstrap.css",
						"~/Content/bootstrap-responsive.css",
						"~/Content/bootstrap-mvc-validation.css"));

			bundles.Add(new StyleBundle("~/Css.ui").Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css"));
		}
	}
}