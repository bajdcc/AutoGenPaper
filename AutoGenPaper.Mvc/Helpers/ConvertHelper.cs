using AutoGenPaper.Common;
using AutoGenPaper.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.IO;

namespace AutoGenPaper.Mvc
{
	public class ConvertHelper
	{
		public static readonly string DefaultSubTitle = "学号________  姓名________ 教师________ 成绩________";
		private static readonly string CheckTrue = "正确";
		private static readonly string CheckFalse = "错误";

		public static AGPPrintMainModel ConvertPaperForPrint(string Title, string SubTitle, IEnumerable<Paper_Question_Relationship> PQs)
		{
			return new AGPPrintMainModel()
			{
				Title = Title,
				SubTitle = SubTitle,
				Items = ConvertQuestionsForPrint(PQs)
			};
		}

		private static IEnumerable<IAGPPrintProxy> ConvertQuestionsForPrint(IEnumerable<Paper_Question_Relationship> PQs)
		{
			List<IAGPPrintProxy> Results = new List<IAGPPrintProxy>();
			{
				var i = 1;
				foreach (var PQ in PQs.OrderBy(a => a.QuestionId).OrderBy(a => a.Question.TypeId))
				{
					AGPPrintItemModel model = null;
					var Q = PQ.Question;
					var QType = (AGPDefine.QuestionType)Enum.ToObject(typeof(AGPDefine.QuestionType), Q.TypeId);
					switch (QType)
					{
						case AGPDefine.QuestionType.SingleSelect:
							model = new AGPPrintItemModel(JsonConvert.DeserializeObject<IEnumerable<string>>(Q.Option));
							break;
						case AGPDefine.QuestionType.MultiSelect:
							model = new AGPPrintItemModel(JsonConvert.DeserializeObject<IEnumerable<string>>(Q.Option));
							break;
						case AGPDefine.QuestionType.Check:
							model = new AGPPrintItemModel(null);
							break;
						case AGPDefine.QuestionType.Blank:
							model = new AGPPrintItemModel(null);
							break;
						case AGPDefine.QuestionType.ShortAnswer:
							model = new AGPPrintItemModel(null);
							break;
					}
					model.SetModelInfo(Q.QuestionId, Q.Time.EditTime.Value, Q.Catalog.Info.Name, Q.Answer, i, PQ.Points, Q.Difficulty, (AGPDefine.QuestionType)Q.TypeId, Q.Info.Name);
					Results.Add(model);
					i++;
				}
			}
			var sum = from x in Results
					  group x by x.GetQuestionType() into g
					  select new { type = g.Key, sum = g.Sum(x => x.GetPoints()) };
			var group = from y in
							(from x in Results
							 group x by x.GetQuestionType() into g
							 select new { type = g.Key, firstId = g.Min(x => x.GetId()) })
						join s in sum on y.type equals s.type
						orderby y.type descending
						select new { y.type, y.firstId, s.sum };
			{
				var i = group.Count();
				var converter = new NumberConvertHelper();
				foreach (var k in group)
				{
					Results.Insert(k.firstId - 1, new AGPPrintItemModel(true,
						string.Format("{0}、{1}（{2}'）", converter.Convert(i.ToString(), bToRMB: false),
							EnumHelper.GetEnumDescription(
								EnumHelper.GetFields(typeof(AGPDefine.QuestionType))
								.Where(a => (AGPDefine.QuestionType)a.GetRawConstantValue() == k.type).Single())
								, k.sum)));
					i--;
				}
			}
			return Results;
		}

		public static string GetAnswerFromQuestion(AGPDefine.QuestionType QType, string Answer)
		{
			string result = string.Empty;

			switch (QType)
			{
				case AGPDefine.QuestionType.MultiSelect:
					{
						var answers = from x in JsonConvert.DeserializeObject<IEnumerable<string>>(Answer)
									  select (char)((char)x.First() + 16);
						answers = answers.Take(26); // A-Z，最多支持26答案
						foreach (var o in answers)
						{
							result += o.ToString();
						}
					}
					break;
				case AGPDefine.QuestionType.SingleSelect:
					{
						result = string.IsNullOrEmpty(Answer) ? string.Empty : ((char)(Answer.First() + 16)).ToString();
					}
					break;
				case AGPDefine.QuestionType.Check:
					{
						result = string.IsNullOrEmpty(Answer) ? string.Empty :
							string.Compare(Answer, "true", true) == 0 ? CheckTrue : CheckFalse;
					}
					break;
			}
			return result;
		}

		public static HtmlHelper<T> ConvertFromControllerToHelper<T>(Controller controller)
		{
			var viewContext = new ViewContext(controller.ControllerContext,
				new FakeView(), controller.ViewData, controller.TempData, TextWriter.Null);
			return new HtmlHelper<T>(viewContext, new ViewPage());
		}
	}
}
