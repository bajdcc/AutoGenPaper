using AutoGenPaper.Common;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AutoGenPaper.Mvc
{
	public class QuestionHelper
	{
		public static bool AddQuestion(AGPDataContext db, string user, QuestionProxyModel model, ModelStateDictionary ModelState, out int id)
		{
			bool success = true;
			id = 0;
			try
			{
				var c = db.Catalogs.Find(model.CatalogId);
				if (c == null)
				{
					ModelState.AddModelError("CatalogId", "类别不正确");
					return false;
				}
				int TypeId = model.TypeId;
				if (TypeId == 0)
				{
					ModelState.AddModelError("TypeId", "题型不正确");
					return false;
				}
				var QType = (AGPDefine.QuestionType)Enum.ToObject(typeof(AGPDefine.QuestionType), TypeId);
				var u = db.Users.Single(a => a.UserName == user);
				Question q = new Question();
				switch ( QType )
				{
				case AGPDefine.QuestionType.SingleSelect:
					{
						var o = model.Option.Split( '$' ).ToList();
						var ans = model.Answer.ToList().ConvertAll<int>(a => a - 'A' + 1).Distinct().OrderBy(a => a).ToList();
						if (o.Count != 1)
							throw new QuestionModelException();
						if (!(ans.First() > 0 && ans.First() <= o.Count))
							throw new QuestionModelException();
						q = new AGPQuestionModel_SingleSelect()
						{
							Caption		= model.Name, Option = o, Answer = ans.First(),
							User		= u, Catalog = c, Points = model.Points, Label = model.Label,
							Description	= model.Description, Difficulty = model.Difficulty
						}.ConvertToQuestion();
					}
					break;
				case AGPDefine.QuestionType.MultiSelect:
					{
						var o = model.Option.Split( '$' ).ToList();
						var ans = model.Answer.ToList().ConvertAll<int>(a => a - 'A' + 1).Distinct().OrderBy(a => a).ToList();
						if (o.Count <= 1 || o.Count > 26)
							throw new QuestionModelException();
						if (!(ans.All(a => a > 0 && a <= o.Count)))
							throw new QuestionModelException();
						q = new AGPQuestionModel_MultiSelect()
						{
							Caption		= model.Name, Option = o, Answer = ans,
							User		= u, Catalog = c, Points = model.Points, Label = model.Label,
							Description	= model.Description, Difficulty = model.Difficulty
						}.ConvertToQuestion();
					}
					break;
				case AGPDefine.QuestionType.Check:
					{
						q = new AGPQuestionModel_Check()
						{
							Caption		= model.Name, Answer = string.Compare(model.Answer, "true", true) == 0,
							User		= u, Catalog = c, Points = model.Points, Label = model.Label,
							Description	= model.Description, Difficulty = model.Difficulty
						}.ConvertToQuestion();
					}
					break;
				case AGPDefine.QuestionType.Blank:
					{
						q = new AGPQuestionModel_Blank()
						{
							Caption		= model.Name, Answer = model.Answer,
							User		= u, Catalog = c, Points = model.Points, Label = model.Label,
							Description	= model.Description, Difficulty = model.Difficulty
						}.ConvertToQuestion();
					}
					break;
				case AGPDefine.QuestionType.ShortAnswer:
					{
						q = new AGPQuestionModel_ShortAnswer()
						{
							Caption		= model.Name, Answer = model.Answer,
							User		= u, Catalog = c, Points = model.Points, Label = model.Label,
							Description	= model.Description, Difficulty = model.Difficulty
						}.ConvertToQuestion();
					}
					break;
				default:
					break;
				}

				q.State = (int)AGPDefine.CommitType.Insert;
				db.Questions.Add(q);
				db.SaveChanges();
				id = q.QuestionId;
			}
			catch (Exception ex)
			{
				success = false;
				ModelState.AddModelError("", ex.Message);
			}
			return success;
		}

		public static void DeleteQuestion(AGPDataContext db, int id)
		{
			var paper = db.Questions.Find(id);
			if (paper == null)
			{
				return;
			}

			//删除试题
			db.Questions.Remove(paper);
			db.SaveChanges();
		}
	}
}
