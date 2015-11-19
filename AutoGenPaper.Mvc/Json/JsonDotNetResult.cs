using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AutoGenPaper.Mvc
{
	public class JsonDotNetResult : JsonResult
	{
		public JsonDotNetResult()
		{
			this.ContentType = "application/json";
		}

		public JsonDotNetResult(JsonResult existing)
		{
			this.ContentEncoding = existing.ContentEncoding;
			this.ContentType = !string.IsNullOrWhiteSpace(existing.ContentType) ? existing.ContentType : "application/json";
			this.Data = existing.Data;
			this.JsonRequestBehavior = existing.JsonRequestBehavior;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				// Delegate back to allow the default exception to be thrown
				base.ExecuteResult(context);
			}

			HttpResponseBase response = context.HttpContext.Response;
			response.ContentType = this.ContentType;

			if (this.ContentEncoding != null)
			{
				response.ContentEncoding = this.ContentEncoding;
			}

			if (this.Data != null)
			{
				// Replace with your favourite serializer.  
				new Newtonsoft.Json.JsonSerializer().Serialize( response.Output, this.Data );
			}
		}
	}
}
