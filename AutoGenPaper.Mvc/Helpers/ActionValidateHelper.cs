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
	public class ActionValidateHelper
	{
		public static bool ValidateGenPaperStrategies(AGPDataContext db, AGPGenPaperStrategiesModel model, ModelStateDictionary ModelState)
		{
			bool error = false;
			var strExceed = "数量超过预期";
			var questions = db.Questions.Include(a => a.Catalog);
			if (db.Courses.Count(a => a.CourseId == model.CourseId) == 0)
			{
				error = true;
				ModelState.AddModelError("CourseId", "未选择课程");
			}
			if (model.CatalogList.Count == 0 ||
				(from x in db.Catalogs
				 join y in model.CatalogList on x.CatalogId equals y
				 select x.CatalogId).Count() != model.CatalogList.Count)
			{
				error = true;
				ModelState.AddModelError("CatalogList", "类别不正确");
			}
			if (model.SingleSelectCount > questions.Count(a => a.Catalog.CatalogId == model.CourseId && a.TypeId == (int)AGPDefine.QuestionType.SingleSelect))
			{
				error = true;
				ModelState.AddModelError("SingleSelectCount", strExceed);
			}
			if (model.MultiSelectCount > questions.Count(a => a.Catalog.CatalogId == model.CourseId && a.TypeId == (int)AGPDefine.QuestionType.MultiSelect))
			{
				error = true;
				ModelState.AddModelError("MultiSelectCount", strExceed);
			}
			if (model.CheckCount > questions.Count(a => a.Catalog.CatalogId == model.CourseId && a.TypeId == (int)AGPDefine.QuestionType.Check))
			{
				error = true;
				ModelState.AddModelError("CheckCount", strExceed);
			}
			if (model.BlankCount > questions.Count(a => a.Catalog.CatalogId == model.CourseId && a.TypeId == (int)AGPDefine.QuestionType.Blank))
			{
				error = true;
				ModelState.AddModelError("BlankCount", strExceed);
			}
			if (model.ShortAnswerCount > questions.Count(a => a.Catalog.CatalogId == model.CourseId && a.TypeId == (int)AGPDefine.QuestionType.SingleSelect))
			{
				error = true;
				ModelState.AddModelError("ShortAnswerCount", strExceed);
			}
			return error;
		}
	}
}
