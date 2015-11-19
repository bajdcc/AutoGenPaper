using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

namespace System.Web.Mvc.Ajax
{
	public static class AjaxExtensions
	{
		public static MvcForm EasyUIBeginForm(this AjaxHelper ajaxHelper)
		{
			return ajaxHelper.BeginForm(new AjaxOptions
			{
				HttpMethod = "Post",
				UpdateTargetId = "async_result",
				OnFailure = "async_fail",
				OnSuccess = "async_success",
				InsertionMode = InsertionMode.Replace
			});
		}

		public static MvcForm EasyUIBeginForm(this AjaxHelper ajaxHelper, object htmlAttributes)
		{
			return ajaxHelper.BeginForm(null, null, new AjaxOptions
			{
				HttpMethod = "Post",
				UpdateTargetId = "async_result",
				OnFailure = "async_fail",
				OnSuccess = "async_success",
				InsertionMode = InsertionMode.Replace,
			}, htmlAttributes);
		}
	}
}
