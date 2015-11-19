using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AutoGenPaper.Common
{
	public interface IAGPQuestionModel
	{
		Question ConvertToQuestion();
	}

	public class AGPQuestionModel_Base
	{
		private static Random rand = new Random();

		public AGPQuestionModel_Base()
		{
			Caption = "[题干]";
			Description = "[解析]";
			Points = 10;
			Difficulty = rand.Next(10) + 1;
		}

		private int GetId(Type type)
		{
			var attr = Attribute.GetCustomAttribute(type, typeof(AGPQuestionAttribute)) as AGPQuestionAttribute;
			return attr == null ? 0 : (int)attr.type;
		}

		protected Question ConvertToQuestion(Type type, string Option, string Answer)
		{
			return new Question()
			{
				TypeId = GetId(type),
				Info = new ModelDescription() { Name = Caption, Description = Description },
				Time = new ModelTime(),
				Option = Option,
				Answer = Answer,
				Owner = User,
				Catalog = Catalog,
				Points = Points,
				Difficulty = Difficulty,
				Label = Label
			};
		}

		public string Caption { get; set; }

		public User User { get; set; }

		public User Verifier { get; set; }

		public Catalog Catalog { get; set; }

		public string Description { get; set; }

		public decimal Points { get; set; }

		public int Difficulty { get; set; }

		public string Label { get; set; }
	}

	[AGPQuestion(type = AGPDefine.QuestionType.SingleSelect)]
	public class AGPQuestionModel_SingleSelect : AGPQuestionModel_Base, IAGPQuestionModel
	{
		public List<string> Option = new List<string>();
		public int Answer = 1;

		public AGPQuestionModel_SingleSelect()
		{ }

		private void ValidInfo()
		{
			if (Option.Count == 0 || Answer < 1 || Option.Count < Answer)
			{
				throw new QuestionModelException();
			}
		}

		public Question ConvertToQuestion()
		{
			ValidInfo();

			return ConvertToQuestion(GetType(), JsonConvert.SerializeObject(Option), JsonConvert.SerializeObject(Answer));
		}
	}

	/// <summary>
	/// 多选题
	/// </summary>
	[AGPQuestion(type = AGPDefine.QuestionType.MultiSelect)]
	public class AGPQuestionModel_MultiSelect : AGPQuestionModel_Base, IAGPQuestionModel
	{
		public List<string> Option = new List<string>();
		public List<int> Answer = new List<int>();

		public AGPQuestionModel_MultiSelect()
		{ }

		private void ValidInfo()
		{
			Answer = Answer.Distinct().ToList();
			if (Option.Count == 0 || Answer.Count == 0 || !Answer.All(a => a > 0 && a <= Option.Count))
			{
				throw new QuestionModelException();
			}
		}

		public Question ConvertToQuestion()
		{
			ValidInfo();

			return ConvertToQuestion(GetType(), JsonConvert.SerializeObject(Option), JsonConvert.SerializeObject(Answer));
		}
	}

	/// <summary>
	/// 判断题
	/// </summary>
	[AGPQuestion(type = AGPDefine.QuestionType.Check)]
	public class AGPQuestionModel_Check : AGPQuestionModel_Base, IAGPQuestionModel
	{
		public bool Answer = false;

		public AGPQuestionModel_Check()
		{ }

		private void ValidInfo()
		{
		}

		public Question ConvertToQuestion()
		{
			return ConvertToQuestion(GetType(), null, JsonConvert.SerializeObject(Answer));
		}
	}

	/// <summary>
	/// 填空题
	/// </summary>
	[AGPQuestion(type = AGPDefine.QuestionType.Blank)]
	public class AGPQuestionModel_Blank : AGPQuestionModel_Base, IAGPQuestionModel
	{
		public string Answer = "[填空]";

		public AGPQuestionModel_Blank()
		{ }

		private void ValidInfo()
		{
		}

		public Question ConvertToQuestion()
		{
			return ConvertToQuestion(GetType(), null, Answer);
		}
	}

	/// <summary>
	/// 简答题
	/// </summary>
	[AGPQuestion(type = AGPDefine.QuestionType.ShortAnswer)]
	public class AGPQuestionModel_ShortAnswer : AGPQuestionModel_Base, IAGPQuestionModel
	{
		public string Answer = "[简答]";

		public AGPQuestionModel_ShortAnswer()
		{ }

		private void ValidInfo()
		{
		}

		public Question ConvertToQuestion()
		{
			return ConvertToQuestion(GetType(), null, JsonConvert.SerializeObject(Answer));
		}
	}

	public class QuestionProxyModel
	{
		[Required]
		[Display(Name = "题型")]
		public int TypeId { get; set; }

		[Required]
		[Display(Name = "题干")]
		public string Name { get; set; }

		[Display(Name = "解析")]
		public string Description { get; set; }

		[Display(Name = "类别")]
		public int CatalogId { get; set; }

		[Display(Name = "选项（如没有则留空，分隔符为“$”）")]
		public string Option { get; set; }

		[Display(Name = "答案（多选例ABCD）")]
		public string Answer { get; set; }

		[Display(Name = "分值")]
		[Required]
		public decimal Points { get; set; }

		[Display(Name = "难度")]
		[Range(1, 10)]
		[Required]
		public int Difficulty { get; set; }

		[Display(Name = "标签")]
		public string Label { get; set; }
	}
}
