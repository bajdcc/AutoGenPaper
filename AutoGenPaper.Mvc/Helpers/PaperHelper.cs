using AutoGenPaper.Common;
using AutoGenPaper.Mvc;
using EntityFramework;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace AutoGenPaper.Mvc
{
	public class PaperHelper
	{
		private static Expression<Func<Question, bool>> IsEasy = a => isEasy(a.Difficulty);
		private static Expression<Func<Question, bool>> IsMiddle = a => isMiddle(a.Difficulty);
		private static Expression<Func<Question, bool>> IsHard = a => isHard(a.Difficulty);
		/// <summary>
		/// 难度分配（简单，中等）
		/// </summary>
		private static List<Tuple<double, double>> DiffAlloc = new List<Tuple<double, double>>()
		{
			new Tuple<double, double>(0.8, 0.1), // 简单。diff = 0, easy = 0.8, middle = 0.1, hard = 0.1
			new Tuple<double, double>(0.7, 0.2), // 中等。diff = 1, easy = 0.7, middle = 0.2, hard = 0.1
			new Tuple<double, double>(0.6, 0.2), // 难  。diff = 2, easy = 0.6, middle = 0.2, hard = 0.2
		};
		private static bool isEasy(int difficulty)
		{
			return difficulty >= 6 && difficulty <= 10;
		}
		private static bool isMiddle(int difficulty)
		{
			return difficulty >= 3 && difficulty <= 5;
		}
		private static bool isHard(int difficulty)
		{
			return difficulty == 1 || difficulty == 2;
		}
		public static int GeneratePaper(AGPDataContext db, string owner, AGPGenPaperStrategiesModel model)
		{
			var dict = new Dictionary<AGPDefine.QuestionType, AGPGenPaperStrategiesCountModel>();
			dict.Add(AGPDefine.QuestionType.SingleSelect, new AGPGenPaperStrategiesCountModel() { Count = model.SingleSelectCount, Point = model.SingleSelectPoint });
			dict.Add(AGPDefine.QuestionType.MultiSelect, new AGPGenPaperStrategiesCountModel() { Count = model.MultiSelectCount, Point = model.MultiSelectPoint });
			dict.Add(AGPDefine.QuestionType.Check, new AGPGenPaperStrategiesCountModel() { Count = model.CheckCount, Point = model.CheckPoint });
			dict.Add(AGPDefine.QuestionType.Blank, new AGPGenPaperStrategiesCountModel() { Count = model.BlankCount, Point = model.BlankPoint });
			dict.Add(AGPDefine.QuestionType.ShortAnswer, new AGPGenPaperStrategiesCountModel() { Count = model.ShortAnswerCount, Point = model.ShortAnswerPoint });

			var user = db.Users.Single(a => a.UserName == owner);
			var paper = new Paper()
			{
				Info = new ModelDescription() { Name = model.Name },
				Time = new ModelTime(),
				Difficulty = model.DifficultyType,
				Owner = user,
				Points = 0
			};
			db.Papers.Add(paper);
			db.SaveChanges();

			var allQuestions = from x in db.Questions.Include(a => a.Catalog).Include(a => a.Catalog.Course)
							   where x.State == (int)AGPDefine.CommitType.Normal
							   where x.Catalog.Course.CourseId == model.CourseId
							   join y in model.CatalogList on x.Catalog.CatalogId equals y
							   orderby Guid.NewGuid().ToString()
							   select x;

			var easyQuestion = allQuestions.Where(IsEasy.Compile());
			var middleQuestion = allQuestions.Where(IsMiddle.Compile());
			var hardQuestion = allQuestions.Where(IsHard.Compile());

			var selectedQuestion = new List<Paper_Question_Relationship>();

			foreach (var kv in dict)
			{
				if (kv.Value.Count == 0 || kv.Value.Point == 0)
					continue;

				var easy = (int)Math.Ceiling(kv.Value.Count * DiffAlloc[model.DifficultyType].Item1);
				var middle = (int)Math.Ceiling(kv.Value.Count * DiffAlloc[model.DifficultyType].Item2);
				var hard = kv.Value.Count - easy - middle;
				decimal perPoint = (decimal)kv.Value.Point / (decimal)kv.Value.Count;

				var middleQ = (from x in middleQuestion
							   where x.TypeId == (int)kv.Key
							   select new Paper_Question_Relationship()
							   {
								   PaperId = paper.PaperId,
								   QuestionId = x.QuestionId,
								   Points = perPoint
							   }).Take(middle);
				var hardQ = (from x in hardQuestion
							 where x.TypeId == (int)kv.Key
							 select new Paper_Question_Relationship()
							 {
								 PaperId = paper.PaperId,
								 QuestionId = x.QuestionId,
								 Points = perPoint
							 }).Take(hard);
				var easyQ = (from x in easyQuestion
							 where x.TypeId == (int)kv.Key
							 select new Paper_Question_Relationship()
							 {
								 PaperId = paper.PaperId,
								 QuestionId = x.QuestionId,
								 Points = perPoint
							 }).Take(kv.Value.Count - middleQ.Count() - hardQ.Count());
				selectedQuestion.AddRange(easyQ);
				selectedQuestion.AddRange(middleQ);
				selectedQuestion.AddRange(hardQ);
			}

			foreach (var pq in selectedQuestion)
			{
				db.PQs.Add(pq);
			}

			db.Papers.Where(a => a.PaperId == paper.PaperId).Update(a => new Paper
			{
				Points = selectedQuestion.Sum(b => b.Points)
			});

			db.SaveChanges();

			return paper.PaperId;
		}

		public static void DeletePaper(AGPDataContext db, int id)
		{
			var paper = db.Papers.Find(id);
			if (paper == null)
			{
				return;
			}

			//删除相关项 - 外键关系表FK_Paper_Question
			db.PQs.Where(a => a.PaperId == id).Delete();

			//删除试卷
			db.Papers.Remove(paper);
			db.SaveChanges();
		}
	}
}
