using AutoGenPaper.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AutoGenPaper.Mvc
{
	public class JsonQuestionModel
	{
		public string name { set; get; }
		public string value { set; get; }
		public string group { set; get; }

		public static IEnumerable<JsonQuestionModel> FromModel(Controller controller, Question question)
		{
			var helper = ConvertHelper.ConvertFromControllerToHelper<Question>(controller);
			helper.ViewData.Model = question;
			var QType = (AGPDefine.QuestionType)Enum.ToObject(typeof(AGPDefine.QuestionType), question.TypeId);
			var StrParam = "主要信息";
			var result = new List<JsonQuestionModel>()
			{
				new JsonQuestionModel()
				{
					name = "类型",
					value = EnumHelper.GetEnumDescription(
							EnumHelper.GetFields(typeof(AGPDefine.QuestionType))
							.Where(a => (AGPDefine.QuestionType)a.GetRawConstantValue() == QType).Single()),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Info.Name).ToString(),
					value = helper.DisplayFor(model => model.Info.Name).ToString(),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Info.Description).ToString(),
					value = helper.DisplayFor(model => model.Info.Description).ToString(),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Time.CreateTime).ToString(),
					value = helper.DisplayFor(model => model.Time.CreateTime).ToString(),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Time.EditTime).ToString(),
					value = helper.DisplayFor(model => model.Time.EditTime).ToString(),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Points).ToString(),
					value = helper.DisplayFor(model => model.Points).ToString(),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Difficulty).ToString(),
					value = helper.DisplayFor(model => model.Difficulty).ToString(),
					group = StrParam
				},
				new JsonQuestionModel()
				{
					name = helper.DisplayNameFor(model => model.Label).ToString(),
					value = helper.DisplayFor(model => model.Label).ToString(),
					group = StrParam
				},
			};
			var Option = helper.DisplayNameFor(model => model.Option).ToString();
			var Answer = helper.DisplayNameFor(model => model.Answer).ToString();
			//选项
			switch (QType)
			{
				case AGPDefine.QuestionType.SingleSelect:
				case AGPDefine.QuestionType.MultiSelect:
					{
						var options = JsonConvert.DeserializeObject<IEnumerable<string>>(question.Option);
						options = options.Take(26); // A-Z，最多支持26选项
						var c = 'A';
						foreach (var o in options)
						{
							result.Add(new JsonQuestionModel()
							{
								name = c.ToString(),
								value = o,
								group = "选项"
							});
							c++;
						}
					}
					break;
				default:
					break;
			}
			//答案
			switch (QType)
			{
				case AGPDefine.QuestionType.MultiSelect:
					{
						var answers = from x in JsonConvert.DeserializeObject<IEnumerable<string>>(question.Answer)
									  select (char)((char)x.First() + 16);
						answers = answers.Take(26); // A-Z，最多支持26答案
						foreach (var o in answers)
						{
							result.Add(new JsonQuestionModel()
							{
								name = "",
								value = o.ToString(),
								group = Answer
							});
						}
					}
					break;
				case AGPDefine.QuestionType.SingleSelect:
					{
						result.Add(new JsonQuestionModel()
						{
							name = "",
							value = ((char)(helper.DisplayFor(model => model.Answer).ToString().First() + 16)).ToString(),
							group = Answer
						});
					}
					break;
				case AGPDefine.QuestionType.Check:
					{
						result.Add(new JsonQuestionModel()
						{
							name = "",
							value = string.Compare(helper.DisplayFor(model => model.Answer).ToString(), "true", true) == 0 ? "正确" : "错误",
							group = Answer
						});
					}
					break;
				default:
					{
						result.Add(new JsonQuestionModel()
						{
							name = "",
							value = helper.DisplayFor(model => model.Answer).ToString(),
							group = Answer
						});
					}
					break;
			}
			return result;
		}
	}

	public class JsonQuestionAllModel
	{
		public IEnumerable<JsonQuestionListModel> rows { get; set; }
		public int total { get; set; }
	}

	public class JsonQuestionListModel
	{
		public int id { get; set; }
		public string type { get; set; }
		public string name { get; set; }
		public string create { get; set; }
		public string edit { get; set; }
		public string pts { get; set; }
		public string diff { get; set; }
		public string label { get; set; }
		public string opt { get; set; }
		public string ans { get; set; }
		public static JsonQuestionAllModel Empty()
		{
			return new JsonQuestionAllModel()
			{
				total = 0,
				rows = new List<JsonQuestionListModel>()
			};
		}

		public static JsonQuestionAllModel FromModel(int total, IEnumerable<Question> questions)
		{
			var rows = new List<JsonQuestionListModel>();
			foreach (var q in questions)
			{
				var QType = (AGPDefine.QuestionType)Enum.ToObject(typeof(AGPDefine.QuestionType), q.TypeId);
				rows.Add(new JsonQuestionListModel()
				{
					id = q.QuestionId,
					type = EnumHelper.GetEnumDescription(
							EnumHelper.GetFields(typeof(AGPDefine.QuestionType))
							.Where(a => (AGPDefine.QuestionType)a.GetRawConstantValue() == QType).Single()),
					name = q.Info.Name,
					create = q.Time.CreateTime.ToString(),
					edit = q.Time.EditTime.ToString(),
					pts = q.Points.ToString(),
					diff = q.Difficulty.ToString(),
					label = q.Label,
					opt = q.Option,
					ans = q.Answer,
				});
			}
			return new JsonQuestionAllModel()
			{
				rows = rows,
				total = total
			};
		}
	}
}
