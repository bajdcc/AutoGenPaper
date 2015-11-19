using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoGenPaper.Common;
using AutoGenPaper.Mvc;
using System.Linq.Expressions;

namespace AutoGenPaper.Mvc
{
	public static class GeneratePaperStrategies
	{
		private static Expression<Func<Question, bool>> IsEasy = a => isEasy(a.Difficulty);
		private static Expression<Func<Question, bool>> IsMiddle = a => isMiddle(a.Difficulty);
		private static Expression<Func<Question, bool>> IsHard = a => isHard(a.Difficulty);
		private static bool isEasy(int difficulty)
		{
			return difficulty >= 7 && difficulty <= 10;
		}
		private static bool isMiddle(int difficulty)
		{
			return difficulty >= 3 && difficulty <= 6;
		}
		private static bool isHard(int difficulty)
		{
			return difficulty == 1 || difficulty == 2;
		}
		public static int GeneratePaper(AGPDataContext context, string owner, AGPGenPaperStrategiesModel model)
		{
			var user = context.Users.Single(a => a.UserName == owner);
			var sum = model.SingleSelectPoint + model.MultiSelectPoint + model.CheckPoint + model.BlankPoint + model.ShortAnswerPoint;
			var paper = new Paper()
			{
				Info = new ModelDescription() { Name = model.Name },
				Time = new ModelTime(),
				Points = sum,
				Difficulty = model.DifficultyType,
			};
			context.Papers.Add(paper);
			context.SaveChanges();

			var allAuestions = context.Questions.Include(a => a.Catalog)
				.Where(a => a.Catalog.CatalogId == model.CatalogId)
				.OrderBy(a => new Guid());
			var easyQuestion = allAuestions.Where(IsEasy.Compile());
			var middleQuestion = allAuestions.Where(IsMiddle.Compile());
			var hardQuestion = allAuestions.Where(IsHard.Compile());

			var selectedQuestion = new List<Paper_Question_Relationship>();
			if (model.DifficultyType == 0)
			{
				int easy = 0, middle = 0, hard = 0;

				easy = (int)(model.SingleSelectCount * 0.8);
				middle = (int)(model.SingleSelectCount * 0.1);
				hard = model.SingleSelectCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(model.SingleSelectCount - selectedQuestion.Count()));

				easy = (int)(model.MultiSelectCount * 0.8);
				middle = (int)(model.MultiSelectCount * 0.1);
				hard = model.MultiSelectCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(model.MultiSelectCount - selectedQuestion.Count()));

				easy = (int)(model.CheckCount * 0.8);
				middle = (int)(model.CheckCount * 0.1);
				hard = model.CheckCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(model.CheckCount - selectedQuestion.Count()));

				easy = (int)(model.BlankCount * 0.8);
				middle = (int)(model.BlankCount * 0.1);
				hard = model.BlankCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(model.BlankCount - selectedQuestion.Count()));

				easy = (int)(model.ShortAnswerCount * 0.8);
				middle = (int)(model.ShortAnswerCount * 0.1);
				hard = model.ShortAnswerCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(model.ShortAnswerCount - selectedQuestion.Count()));
			}
			else if (model.DifficultyType == 1)
			{
				int easy = 0, middle = 0, hard = 0;

				easy = (int)(model.SingleSelectCount * 0.7);
				middle = (int)(model.SingleSelectCount * 0.2);
				hard = model.SingleSelectCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(model.SingleSelectCount - selectedQuestion.Count()));

				easy = (int)(model.MultiSelectCount * 0.7);
				middle = (int)(model.MultiSelectCount * 0.2);
				hard = model.MultiSelectCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(model.MultiSelectCount - selectedQuestion.Count()));

				easy = (int)(model.CheckCount * 0.7);
				middle = (int)(model.CheckCount * 0.2);
				hard = model.CheckCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(model.CheckCount - selectedQuestion.Count()));

				easy = (int)(model.BlankCount * 0.7);
				middle = (int)(model.BlankCount * 0.2);
				hard = model.BlankCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(model.BlankCount - selectedQuestion.Count()));

				easy = (int)(model.ShortAnswerCount * 0.7);
				middle = (int)(model.ShortAnswerCount * 0.2);
				hard = model.ShortAnswerCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(model.ShortAnswerCount - selectedQuestion.Count()));
			}
			else if (model.DifficultyType == 2)
			{
				int easy = 0, middle = 0, hard = 0;

				easy = (int)(model.SingleSelectCount * 0.6);
				middle = (int)(model.SingleSelectCount * 0.1);
				hard = model.SingleSelectCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.SingleSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.SingleSelectPoint / model.SingleSelectCount
										   }).Take(model.SingleSelectCount - selectedQuestion.Count()));

				easy = (int)(model.MultiSelectCount * 0.6);
				middle = (int)(model.MultiSelectCount * 0.1);
				hard = model.MultiSelectCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect && x.Difficulty == 2
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.MultiSelect
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.MultiSelectPoint / model.MultiSelectCount
										   }).Take(model.MultiSelectCount - selectedQuestion.Count()));

				easy = (int)(model.CheckCount * 0.6);
				middle = (int)(model.CheckCount * 0.1);
				hard = model.CheckCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check && x.Difficulty == 2
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Check
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.CheckPoint / model.CheckCount
										   }).Take(model.CheckCount - selectedQuestion.Count()));

				easy = (int)(model.BlankCount * 0.6);
				middle = (int)(model.BlankCount * 0.1);
				hard = model.BlankCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank && x.Difficulty == 2
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.Blank
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.BlankPoint / model.BlankCount
										   }).Take(model.BlankCount - selectedQuestion.Count()));

				easy = (int)(model.ShortAnswerCount * 0.6);
				middle = (int)(model.ShortAnswerCount * 0.1);
				hard = model.ShortAnswerCount - easy - middle;
				selectedQuestion.AddRange((from x in middleQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer && x.Difficulty == 2
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(middle));
				selectedQuestion.AddRange((from x in hardQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(hard));
				selectedQuestion.AddRange((from x in easyQuestion
										   where x.TypeId == (int)AGPDefine.QuestionType.ShortAnswer
										   select new Paper_Question_Relationship()
										   {
											   PaperId = paper.PaperId,
											   QuestionId = x.QuestionId,
											   Points = (decimal)model.ShortAnswerPoint / model.ShortAnswerCount
										   }).Take(model.ShortAnswerCount - selectedQuestion.Count()));
			}
			foreach (var pq in selectedQuestion)
			{
				context.PQs.Add(pq);
			}
			context.SaveChanges();
			return paper.PaperId;
		}
	}
}
