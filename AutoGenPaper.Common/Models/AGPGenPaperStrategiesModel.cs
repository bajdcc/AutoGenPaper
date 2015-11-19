using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace AutoGenPaper.Common
{
	public class AGPGenPaperStrategiesCountModel
	{
		public int Count { set; get; }
		public int Point { set; get; }
	}

	/// <summary>
	/// 出卷策略
	/// </summary>
	public class AGPGenPaperStrategiesModel
	{
		public AGPGenPaperStrategiesModel()
		{
			SingleSelectCount = 10;
			SingleSelectPoint = 5;
			MultiSelectCount = 10;
			MultiSelectPoint = 20;
			CheckCount = 5;
			CheckPoint = 5;
			BlankCount = 10;
			BlankPoint = 20;
			ShortAnswerCount = 5;
			ShortAnswerPoint = 50;
		}

		[Display(Name = "试卷名称")]
		[StringLength(100, MinimumLength = 4)]
		[Required]
		public string Name { set; get; }

		[Display(Name = "课程")]
		[Required]
		public int CourseId { set; get; }

		[Display(Name = "类别")]
		[Required]
		public List<int> CatalogList { set; get; }

		[Display(Name = "难度等级")]
		[Range(0, 2)]
		[Required]
		public int DifficultyType { set; get; }

		[Display(Name = "单选题个数")]
		[Range(0, 50)]
		[Required]
		public int SingleSelectCount { set; get; }

		[Display(Name = "单选题总分")]
		[Range(0, 1000)]
		[Required]
		public int SingleSelectPoint { set; get; }

		[Display(Name = "多选题个数")]
		[Range(0, 50)]
		[Required]
		public int MultiSelectCount { set; get; }

		[Display(Name = "多选题总分")]
		[Range(0, 1000)]
		[Required]
		public int MultiSelectPoint { set; get; }

		[Display(Name = "判断题个数")]
		[Range(0, 50)]
		[Required]
		public int CheckCount { set; get; }

		[Display(Name = "判断题总分")]
		[Range(0, 1000)]
		[Required]
		public int CheckPoint { set; get; }

		[Display(Name = "填空题个数")]
		[Range(0, 50)]
		[Required]
		public int BlankCount { set; get; }

		[Display(Name = "填空题总分")]
		[Range(0, 1000)]
		[Required]
		public int BlankPoint { set; get; }

		[Display(Name = "简答题个数")]
		[Range(0, 50)]
		[Required]
		public int ShortAnswerCount { set; get; }

		[Display(Name = "简答题总分")]
		[Range(0, 1000)]
		[Required]
		public int ShortAnswerPoint { set; get; }
	}
}
