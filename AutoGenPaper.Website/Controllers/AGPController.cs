using AutoGenPaper.Common;
using AutoGenPaper.Mvc;
using AutoGenPaper.Mvc.Attributes;
using EntityFramework;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace AutoGenPaper.Website.Controllers
{
	[Authorize]
	[InitializeSimpleMembership]
	public class AGPController : Controller
    {
		[NonAction]
		private void SetResponseStatusAndEndRequest(HttpStatusCode statusCode)
		{
			Response.StatusCode = (int)statusCode;
			Response.End();
		}

		[NonAction]
		private void RequireCorrentAction()
		{
			WebSecurity.RequireAuthenticatedUser();

			if (!Request.IsAjaxRequest())
			{
				SetResponseStatusAndEndRequest(HttpStatusCode.Forbidden);
			}
		}

		// POST /AGP/Navigation
		[AllowAnonymous]
		[HttpPost]
		public JsonDotNetResult Navigation()
		{
			RequireCorrentAction();

			var RoleType = RoleHelper.GetRole(User);

			switch (RoleType)
			{
				case AutoGenPaper.Common.AGPDefine.RoleType.Admin:
					return new JsonDotNetResult()
					{
						Data = new List<AGPNavigationTreeNode>()
						{
#region 用户管理
							new AGPNavigationTreeNode(){
								id = 100,
								text = "用户管理",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 101,
										text = "查看用户",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "UserMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 102,
										text = "删除用户",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Delete", "UserMgr")
										},
									},
								},
							},
#endregion
#region 用户组管理
							new AGPNavigationTreeNode(){
								id = 110,
								text = "用户组管理",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 111,
										text = "添加用户组",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Create", "UserGroupMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 112,
										text = "删除用户组",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Delete", "UserGroupMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 113,
										text = "查看用户组",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "UserGroupMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 114,
										text = "编辑用户组",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Edit", "UserGroupMgr")
										},
									},
								},
							},
#endregion
#region 日志
							new AGPNavigationTreeNode(){
								id = 120,
								text = "日志管理",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 121,
										text = "查看日志",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "Log")
										},
									},
								},
							},
#endregion
						}
					};
				case AutoGenPaper.Common.AGPDefine.RoleType.Teacher:
					return new JsonDotNetResult()
					{
						Data = new List<AGPNavigationTreeNode>()
						{
#region 试题管理
							new AGPNavigationTreeNode(){
								id = 200,
								text = "试题管理",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 201,
										text = "添加试题",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Create", "QuestionMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 202,
										text = "删除试题",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Delete", "QuestionMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 203,
										text = "查看试题",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "QuestionMgr")
										},
									},
// 									new AGPNavigationTreeNode(){
// 										id = 204,
// 										text = "编辑试题",
// 										attributes = new AGPNavigationTreeNodeAttibute(){
// 											url = Url.Action("Edit", "QuestionMgr")
// 										},
// 									},
								},
							},
#endregion
#region 试卷管理
							new AGPNavigationTreeNode(){
								id = 210,
								text = "试卷管理",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 211,
										text = "添加试卷",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Create", "PaperMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 212,
										text = "删除试卷",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Delete", "PaperMgr")
										},
									},
									new AGPNavigationTreeNode(){
										id = 213,
										text = "查看试卷",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "PaperMgr")
										},
									},
// 									new AGPNavigationTreeNode(){
// 										id = 214,
// 										text = "编辑试卷",
// 										attributes = new AGPNavigationTreeNodeAttibute(){
// 											url = Url.Action("Edit", "PaperMgr")
// 										},
// 									},
								},
							},
#endregion
#region 试卷反馈
							new AGPNavigationTreeNode(){
								id = 220,
								text = "试卷反馈",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 221,
										text = "试卷反馈",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Details", "PaperMgr")
										},
									},
								},
							},
#endregion
						}
					};
				case AutoGenPaper.Common.AGPDefine.RoleType.Verifier:
					return new JsonDotNetResult()
					{
						Data = new List<AGPNavigationTreeNode>()
						{
#region 试题审核
							new AGPNavigationTreeNode(){
								id = 300,
								text = "试题审核",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 301,
										text = "未审试题",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "QuestionVerify")
										},
									},
								},
							},
#endregion
						}
					};
				case AutoGenPaper.Common.AGPDefine.RoleType.Researcher:
					return new JsonDotNetResult()
					{
						Data = new List<AGPNavigationTreeNode>()
						{
#region 试卷审核
							new AGPNavigationTreeNode(){
								id = 300,
								text = "试卷审核",
								attributes = new AGPNavigationTreeNodeAttibute()
								{
									url = ""
								},
								children = new List<AGPNavigationTreeNode>(){
									new AGPNavigationTreeNode(){
										id = 301,
										text = "未审试卷",
										attributes = new AGPNavigationTreeNodeAttibute(){
											url = Url.Action("Index", "PaperVerify")
										},
									},
								},
							},
#endregion
						}
					};
			}
			SetResponseStatusAndEndRequest(HttpStatusCode.Forbidden);
			return null;
		}

		// POST /AGP/QuestionDetails
		[AllowAnonymous]
		[ActionName("QuestionDetails")]
		[HttpPost]
		public JsonDotNetResult AGP_Question_Details(int id = 0)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("教师");

			using (var db = new AGPDataContext())
			{
				var question = db.Questions
					.Include(a => a.Owner)
					.Include(a => a.Verifier)
					.Include(a => a.Catalog)
					.Where(a => a.QuestionId == id)
					.Where(a => a.State == (int)AGPDefine.CommitType.Normal);

				if (question.Count() == 0)
				{
					SetResponseStatusAndEndRequest(HttpStatusCode.NotFound);
				}

				return new JsonDotNetResult() { Data = JsonQuestionModel.FromModel(this, question.First()) };
			}
		}

		// POST /AGP/QuestionDetails_Verify
		[AllowAnonymous]
		[ActionName("QuestionDetails_Verify")]
		[HttpPost]
		public JsonDotNetResult AGP_Question_Details_Verify(int id = 0)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("审核员");

			using (var db = new AGPDataContext())
			{
				var question = db.Questions
					.Include(a => a.Owner)
					.Include(a => a.Verifier)
					.Include(a => a.Catalog)
					.Where(a => a.QuestionId == id);

				if (question.Count() == 0)
				{
					SetResponseStatusAndEndRequest(HttpStatusCode.NotFound);
				}

				return new JsonDotNetResult() { Data = JsonQuestionModel.FromModel(this, question.First()) };
			}
		}

		// POST /AGP/PaperAll
		[AllowAnonymous]
		[ActionName("PaperAll")]
		[HttpPost]
		public JsonDotNetResult AGP_Paper_All(int rows = 10, int page = 1)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("教师");

			using (var db = new AGPDataContext())
			{
				var papers = db.Papers
					.Include(a => a.Owner)
					.Include(a => a.Verifier)
					.OrderBy(a => a.PaperId)
					.Where(a => a.State == (int)AGPDefine.CommitType.Normal);
				var total = papers.FutureCount();
				var result = papers
					.Skip(rows * (page - 1))
					.Take(rows).Future();

				var count = total.Value;
				if (count == 0)
				{
					return new JsonDotNetResult() { Data = JsonPaperListModel.Empty() };
				}

				return new JsonDotNetResult() { Data = JsonPaperListModel.FromModel(count, result) };
			}
		}

		// POST /AGP/PaperAll_Verify
		[AllowAnonymous]
		[ActionName("PaperAll_Verify")]
		[HttpPost]
		public JsonDotNetResult AGP_Paper_All_Verify(int rows = 10, int page = 1)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("教研员");

			using (var db = new AGPDataContext())
			{
				var papers = db.Papers
					.Include(a => a.Owner)
					.Include(a => a.Verifier)
					.Where(a => a.State != (int)AGPDefine.CommitType.Normal)
					.OrderBy(a => a.PaperId);
				var total = papers.FutureCount();
				var result = papers
					.Skip(rows * (page - 1))
					.Take(rows).Future();

				var count = total.Value;
				if (count == 0)
				{
					return new JsonDotNetResult() { Data = JsonPaperListModel.Empty() };
				}

				return new JsonDotNetResult() { Data = JsonPaperListModel.FromModel(count, result) };
			}
		}

		// POST /AGP/QuestionAll
		[AllowAnonymous]
		[ActionName("QuestionAll")]
		[HttpPost]
		public JsonDotNetResult AGP_Question_All(int rows = 10, int page = 1)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("教师");

			using (var db = new AGPDataContext())
			{
				var questions = db.Questions
					.Include(a => a.Owner)
					.Include(a => a.Verifier)
					.Include(a => a.Catalog)
					.Where(a => a.State == (int)AGPDefine.CommitType.Normal)
					.OrderBy(a => a.QuestionId);
				var total = questions.FutureCount();
				var result = questions
					.Skip(rows * (page - 1))
					.Take(rows).Future();

				var count = total.Value;
				if (count == 0)
				{
					return new JsonDotNetResult() { Data = JsonQuestionListModel.Empty() };
				}

				return new JsonDotNetResult() { Data = JsonQuestionListModel.FromModel(count, result) };
			}
		}

		// POST /AGP/QuestionAll_Verify
		[AllowAnonymous]
		[ActionName("QuestionAll_Verify")]
		[HttpPost]
		public JsonDotNetResult AGP_Question_All_Verify(int rows = 10, int page = 1)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("审核员");

			using (var db = new AGPDataContext())
			{
				var questions = db.Questions
					.Include(a => a.Owner)
					.Include(a => a.Verifier)
					.Include(a => a.Catalog)
					.Where(a => a.State != (int)AGPDefine.CommitType.Normal)
					.OrderBy(a => a.QuestionId);
				var total = questions.FutureCount();
				var result = questions
					.Skip(rows * (page - 1))
					.Take(rows).Future();

				var count = total.Value;
				if (count == 0)
				{
					return new JsonDotNetResult() { Data = JsonQuestionListModel.Empty() };
				}

				return new JsonDotNetResult() { Data = JsonQuestionListModel.FromModel(count, result) };
			}
		}

		// POST /AGP/Log
		[AllowAnonymous]
		[ActionName("Log")]
		[HttpPost]
		public JsonDotNetResult AGP_Log(int rows = 10, int page = 1)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("管理员");

			using (var db = new AGPDataContext())
			{
				var logs = db.Logs
					.Include(a => a.User)
					.Include(a => a.Level)
					.Include(a => a.Event)
					.Include(a => a.Object)
					.OrderBy(a => a.Time);
				var total = logs.FutureCount();
				var result = logs
					.Skip(rows * (page - 1))
					.Take(rows).Future();

				var count = total.Value;
				if (count == 0)
				{
					return new JsonDotNetResult() { Data = JsonLogListModel.Empty() };
				}

				return new JsonDotNetResult() { Data = JsonLogListModel.FromModel(count, result) };
			}
		}

		// POST /AGP/GetCourseList
		[ActionName("GetCourseList")]
		[HttpPost]
		public JsonDotNetResult AGP_Get_Course_List()
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("教师");

			using (var db = new AGPDataContext())
			{
				var courses = from x in db.Courses
							  orderby x.CourseId
							  select new
							  {
								  id = x.CourseId,
								  val = x.Info.Name
							  };

				return new JsonDotNetResult() { Data = courses.ToList() };
			}
		}

		// POST /AGP/GetCatalogList
		[ActionName("GetCatalogList")]
		[HttpPost]
		public JsonDotNetResult AGP_Get_Catalog_List(int id)
		{
			RequireCorrentAction();

			WebSecurity.RequireRoles("教师");

			using (var db = new AGPDataContext())
			{
				var courses = from x in db.Catalogs.Include(a => a.Course)
							  where x.Course.CourseId == id
							  orderby x.CatalogId
							  select new
							  {
								  id = x.CatalogId,
								  val = x.Info.Name
							  };

				return new JsonDotNetResult() { Data = courses.ToList() };
			}
		}

		// GET /AGP/WordDl
		[ActionName("WordDl")]
		[HttpGet]
		public ActionResult AGP_Download_Paper_To_Word(int id)
		{
			WebSecurity.RequireRoles("教师");

			var doc = WordHelper.TransferPaperToWord(User.Identity.Name, id);
			if (doc == null)
			{
				return HttpNotFound();
			}

			using (var m = new MemoryStream())
			{
				doc.Save(m, Aspose.Words.SaveFormat.Doc);
				return File(m.ToArray(), "application/msword", string.Format("{0}.doc", id));
			}
		}
	}
}
