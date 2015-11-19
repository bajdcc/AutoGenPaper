using AutoGenPaper.Common;
using AutoGenPaper.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace System.Web.Mvc.Html
{
	public static class AGPHtmlHelper
	{
		public static MvcHtmlString AGP_DisplayForPaper(this HtmlHelper html, int id)
		{
			using (var db = new AGPDataContext())
			{
				var paper = db.Papers.Find(id);
				if (paper == null)
				{
					return new MvcHtmlString("抱歉，无考卷信息。");
				}

				var title = paper.Info.Name; // 试卷名称
				var PQs = from x in db.PQs.Include(a => a.Question).Include(a => a.Question.Catalog)
								where x.PaperId == paper.PaperId
								select x;

				var model = ConvertHelper.ConvertPaperForPrint(title, ConvertHelper.DefaultSubTitle, PQs);

				var sb = new StringBuilder();
				var document = new TagBuilder("div");
				document.AddCssClass("paper");

				//主标题和副标题
				{
					var tag_title = new TagBuilder("h2")
					{
						InnerHtml = !string.IsNullOrEmpty(model.Title) ? HttpUtility.HtmlEncode(model.Title) : string.Empty
					};
					tag_title.AddCssClass("text-center");
					sb.AppendLine(tag_title.ToString());
					var tag_subtitle = new TagBuilder("h5")
					{
						InnerHtml = !string.IsNullOrEmpty(model.SubTitle) ? HttpUtility.HtmlEncode(model.SubTitle) : string.Empty
					};
					tag_subtitle.AddCssClass("text-center");
					sb.AppendLine(tag_subtitle.ToString());
				}

				//题目
				{
					foreach (var q in model.Items)
					{
						var question_div = new TagBuilder("div");
						question_div.AddCssClass("question");

						var tag_group = new TagBuilder("div");

						if (q.IsGroup())
						{
							tag_group.InnerHtml = !string.IsNullOrEmpty(q.GetTitle()) ? HttpUtility.HtmlEncode(q.GetTitle()) : string.Empty;
							tag_group.AddCssClass("question-nav");
						}
						else
						{
							tag_group.AddCssClass("question-group");

							var tag_content = new TagBuilder("div")
							{
								InnerHtml = !string.IsNullOrEmpty(q.GetTitle()) ? HttpUtility.HtmlEncode(q.GetTitle()) : string.Empty
							};
							tag_content.AddCssClass("question-content");
							tag_content.InnerHtml = q.GetId().ToString() + ". " + tag_content.InnerHtml;

							if (q.HasOptions())
							{
								var tag_ol = new TagBuilder("ol");
								foreach (var o in q.GetOptions())
								{
									var tag_option = new TagBuilder("li")
									{
										InnerHtml = !string.IsNullOrEmpty(o) ? HttpUtility.HtmlEncode(o) : string.Empty
									};
									tag_ol.InnerHtml += tag_option.ToString();
								}
								tag_content.InnerHtml += tag_ol.ToString();
							}

							var tag_summary = new TagBuilder("div");
							tag_summary.AddCssClass("question-summary");

							{
								var tag_summary_item = new TagBuilder("div");
								tag_summary_item.AddCssClass("question-summary-left");

								tag_summary_item.InnerHtml = string.Format("编号：{0}", q.GetUniqueId());
								tag_summary.InnerHtml += tag_summary_item.ToString();

								tag_summary_item.InnerHtml = string.Format("分类：{0}", q.GetCatalog());
								tag_summary.InnerHtml += tag_summary_item.ToString();

								tag_summary_item.InnerHtml = string.Format("难度：{0}", q.GetDifficulty());
								tag_summary.InnerHtml += tag_summary_item.ToString();

								tag_summary_item.InnerHtml = string.Format("更新时间：{0}", q.GetUpdateTime().ToShortDateString());
								tag_summary.InnerHtml += tag_summary_item.ToString();

								var answer = ConvertHelper.GetAnswerFromQuestion(q.GetQuestionType(), q.GetAnswer());

								var tag_ask_for_answer = new TagBuilder("div");
								tag_ask_for_answer.InnerHtml = "查看答案";
								tag_ask_for_answer.AddCssClass("question-summary-right");
								tag_ask_for_answer.MergeAttribute("data-toggle", "tooltip");
								tag_ask_for_answer.MergeAttribute("title", string.Format("答案：{0}",
									string.IsNullOrEmpty(answer) ? "略" : answer)); 

								tag_summary.InnerHtml += tag_ask_for_answer.ToString();
							}

							tag_group.InnerHtml += tag_summary.ToString();
							tag_group.InnerHtml += tag_content.ToString();
						}

						question_div.InnerHtml += tag_group.ToString();
						sb.AppendLine(question_div.ToString());
					}
				}

				document.InnerHtml = sb.ToString();
				return MvcHtmlString.Create(document.ToString());
			}
		}
	}
}
