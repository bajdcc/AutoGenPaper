using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Common.Models
{
	/// <summary>
	/// 试卷打印代理接口
	/// </summary>
	public interface IAGPPrintProxy
	{
		/// <summary>
		/// 数据库ID
		/// </summary>
		/// <returns></returns>
		int GetUniqueId();
		/// <summary>
		/// 更新时间
		/// </summary>
		/// <returns></returns>
		DateTime GetUpdateTime();
		/// <summary>
		/// 分类
		/// </summary>
		/// <returns></returns>
		string GetCatalog();
		/// <summary>
		/// 答案
		/// </summary>
		/// <returns></returns>
		string GetAnswer();

		/// <summary>
		/// 题号
		/// </summary>
		/// <returns></returns>
		int GetId();
		/// <summary>
		/// 难度
		/// </summary>
		/// <returns></returns>
		int GetDifficulty();
		/// <summary>
		/// 分值
		/// </summary>
		/// <returns></returns>
		decimal GetPoints();
		/// <summary>
		/// 类型
		/// </summary>
		/// <returns></returns>
		AGPDefine.QuestionType GetQuestionType();
		/// <summary>
		/// 选项
		/// </summary>
		/// <returns></returns>
		IEnumerable<string> GetOptions();
		/// <summary>
		/// 标题
		/// </summary>
		/// <returns></returns>
		string GetTitle();
		/// <summary>
		/// 是否有选项
		/// </summary>
		/// <returns></returns>
		bool HasOptions();
		/// <summary>
		/// 是否是大标题（非题目）
		/// </summary>
		/// <returns></returns>
		bool IsGroup();
	}

	/// <summary>
	/// 试卷
	/// </summary>
	public class AGPPrintMainModel
	{
		/// <summary>
		/// 主标题（XXX期末考试）
		/// </summary>
		public string Title { set; get; }
		/// <summary>
		/// 学号___  姓名___ 任课教师_____ 成绩______
		/// </summary>
		public string SubTitle { set; get; }
		/// <summary>
		/// 题目+编号
		/// </summary>
		public IEnumerable<IAGPPrintProxy> Items { set; get; }
	}

	/// <summary>
	/// 试题
	/// </summary>
	public class AGPPrintItemModel : IAGPPrintProxy
	{
		private int UniqueId = 0;
		private DateTime UpdateTime = DateTime.Now;
		private string Catalog = string.Empty;
		private string Answer = string.Empty;

		private int Id = 0;
		private int Difficulty = 0;
		private decimal Points = 0;
		private string Title = string.Empty;
		private IEnumerable<string> Options;
		private bool bGroup = false;
		private AGPDefine.QuestionType QuestionType = AGPDefine.QuestionType.None;

		public AGPPrintItemModel(bool group, string title)
		{
			bGroup = group;
			Title = title;
		}

		public AGPPrintItemModel(IEnumerable<string> options)
		{
			Options = options;
		}

		public void SetModelInfo(int uniqueId, DateTime updateTime, string catalog, string answer, int id, decimal points, int difficulty, AGPDefine.QuestionType qtype, string title)
		{
			UniqueId = uniqueId;
			UpdateTime = updateTime;
			Catalog = catalog;
			Answer = answer;
			Id = id;
			Difficulty = difficulty;
			Points = points;
			Title = title;
			QuestionType = qtype;
		}

		/// <summary>
		/// 获取题目编号
		/// </summary>
		/// <returns></returns>
		public int GetId()
		{
			return Id;
		}

		/// <summary>
		/// 获取难度
		/// </summary>
		/// <returns></returns>
		public int GetDifficulty()
		{
			return Difficulty;
		}

		/// <summary>
		/// 获取题目分数
		/// </summary>
		/// <returns></returns>
		public decimal GetPoints()
		{
			return Points;
		}

		/// <summary>
		/// 得到题目类型
		/// </summary>
		/// <returns></returns>
		public AGPDefine.QuestionType GetQuestionType()
		{
			return QuestionType;
		}

		/// <summary>
		/// 获取题干（XXXX）或题目类型（如“一、选择题（10'）”）
		/// </summary>
		/// <returns></returns>
		public string GetTitle()
		{
			return Title;
		}

		/// <summary>
		/// 获取所有选项，如果没有，返回空
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetOptions()
		{
			return HasOptions() ? Options : null;
		}

		/// <summary>
		/// 是否有选项
		/// </summary>
		/// <returns></returns>
		public bool HasOptions()
		{
			return !bGroup && Options != null && Options.Count() > 0;
		}

		/// <summary>
		/// 是否是题型编号（一、选择题）
		/// </summary>
		/// <returns></returns>
		public bool IsGroup()
		{
			return bGroup;
		}

		/// <summary>
		/// 数据库ID
		/// </summary>
		/// <returns></returns>
		public int GetUniqueId()
		{
			return UniqueId;
		}

		/// <summary>
		/// 更新时间
		/// </summary>
		/// <returns></returns>
		public DateTime GetUpdateTime()
		{
			return UpdateTime;
		}

		/// <summary>
		/// 所属分类
		/// </summary>
		/// <returns></returns>
		public string GetCatalog()
		{
			return Catalog;
		}

		/// <summary>
		/// 答案
		/// </summary>
		/// <returns></returns>
		public string GetAnswer()
		{
			return Answer;
		}
	}
}
