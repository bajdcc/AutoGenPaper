using AutoGenPaper.Mvc;
using NavigationRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
	public static partial class AGPAjaxActionHelper
	{
		public static IHtmlString AGP_SubmitButton(this HtmlHelper htmlHelper, string submitText)
		{
			return htmlHelper.AGP_SubmitButton(submitText, null);
		}
		public static IHtmlString AGP_SubmitButton(this HtmlHelper htmlHelper, string submitText, object htmlAttributes)
		{
			return htmlHelper.AGP_SubmitButton(submitText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}
		public static IHtmlString AGP_SubmitButton(this HtmlHelper htmlHelper, string submitText, IDictionary<string, object> htmlAttributes)
		{
			var tag = new TagBuilder("input");
			tag.MergeAttributes(htmlAttributes);
			tag.MergeAttribute("type", "submit", true);
			tag.MergeAttribute("value", submitText, true);
			tag.AddCssClass("btn btn-default");
			return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
		}
	}

	public static partial class AGPAjaxActionHelper
	{
		private static readonly string Common_NullOrEmpty = "Value cannot be null or empty.";

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary(), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, null, routeValues, htmlAttributes, autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(AGPAjaxHtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes, autoclose, autolink));
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), autoclose, autolink);
		}

		public static IHtmlString AGPActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(AGPAjaxHtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, autoclose, autolink));
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, new RouteValueDictionary(routeValues), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, routeName, null, autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, routeValues, new RouteValueDictionary(), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues, object htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, routeName, new RouteValueDictionary(routeValues), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, routeName, routeValues, new RouteValueDictionary(), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, null, routeValues, htmlAttributes, autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, routeName, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(AGPAjaxHtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes, autoclose, autolink));
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return htmlHelper.AGPRouteLink(linkText, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), autoclose, autolink);
		}

		public static IHtmlString AGPRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(AGPAjaxHtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes, autoclose, autolink));
		}
	}

	public static class AGPAjaxHtmlHelper
	{
		private static string GetRouteDisplayName(string actionName, string controllerName)
		{
			foreach (var navigationRoute in RouteTable.Routes.OfType<NamedRoute>())
			{
				if (navigationRoute.Defaults != null &&
					navigationRoute.Defaults["action"].ToString() == actionName &&
					navigationRoute.Defaults["controller"].ToString() == controllerName)
				{
					return navigationRoute.DisplayName;
				}
				foreach (var navigationChildRoute in navigationRoute.Children)
				{
					if (navigationRoute.Defaults != null &&
						navigationChildRoute.Defaults["action"].ToString() == actionName &&
						navigationChildRoute.Defaults["controller"].ToString() == controllerName)
					{
						return navigationChildRoute.DisplayName;
					}
				}
			}
			return null;
		}

		public static string GenerateLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return GenerateLink(requestContext, routeCollection, linkText, routeName, actionName, controllerName, null, null, null, routeValues, htmlAttributes, autoclose, autolink);
		}

		public static string GenerateLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, true, autoclose, autolink);
		}

		private static string GenerateLinkInternal(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues, bool autoclose = true, bool autolink = true)
		{
			string str = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
			TagBuilder builder = new TagBuilder("a")
			{
				InnerHtml = linkText
			};
			string html = "";
			if (autolink)
			{
				if (actionName == null)
					actionName = requestContext.RouteData.Values["action"].ToString();
				if (controllerName == null)
					controllerName = requestContext.RouteData.Values["controller"].ToString();
				var displayName = GetRouteDisplayName(actionName, controllerName);
				if (!string.IsNullOrEmpty(displayName))
				{
					html = displayName;
				}
				else
				{
					html = builder.InnerHtml;
				}
			}
			else
			{
				html = builder.InnerHtml;
			}
			builder.MergeAttributes<string, object>(htmlAttributes);
			builder.MergeAttribute("href", "javascript:void(0)");
			builder.MergeAttribute("onclick", string.Format("javascript:{0}('{1}','{2}',{3});",
				JsStrings.RPC_ShowTab, html, str, autoclose.ToString().ToLower()));
			return builder.ToString(TagRenderMode.Normal);
		}

		public static string GenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return GenerateRouteLink(requestContext, routeCollection, linkText, routeName, null, null, null, routeValues, htmlAttributes, autoclose, autolink);
		}

		public static string GenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool autoclose = true, bool autolink = true)
		{
			return GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, null, null, protocol, hostName, fragment, routeValues, htmlAttributes, false, autoclose, autolink);
		}
	}
}