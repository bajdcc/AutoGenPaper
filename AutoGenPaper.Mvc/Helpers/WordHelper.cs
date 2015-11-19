using Aspose.Words;
using AutoGenPaper.Common;
using AutoGenPaper.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AutoGenPaper.Mvc
{
	public class WordHelper
	{
		public static Document TransferPaperToWord(string user, int id)
		{
			using (var db = new AGPDataContext())
			{
				var paper = db.Papers.Find(id);
				if (paper == null)
				{
					return null;
				}

				LogHelper.Log(db, user, AGPDefine.LogLevelType.Info, AGPDefine.LogEventType.Other, AGPDefine.LogObjectType.Paper, "下载试卷");

				var title = paper.Info.Name; // 试卷名称
				var PQs = from x in db.PQs.Include(a => a.Question).Include(a => a.Question.Catalog)
								where x.PaperId == paper.PaperId
								select x;

				var model = ConvertHelper.ConvertPaperForPrint(title, ConvertHelper.DefaultSubTitle, PQs);

				var doc = new Document();
				var builder = new DocumentBuilder(doc);

				//主标题和副标题
				{
					builder.Bold = true;
					builder.Font.Size = 20;
					builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
					builder.Writeln(model.Title);
					builder.InsertBreak(BreakType.LineBreak);

					builder.Bold = false;
					builder.Font.Size = 12;
					builder.Writeln(model.SubTitle);
					builder.InsertBreak(BreakType.LineBreak);
				}

				builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;

				//题目
				{
					foreach (var q in model.Items)
					{
						if (q.IsGroup())
						{
							builder.InsertBreak(BreakType.LineBreak);
							builder.Bold = true;
							builder.Font.Size = 12;
							builder.Writeln(q.GetTitle());
							builder.InsertBreak(BreakType.LineBreak);
						}
						else
						{
							builder.Bold = false;
							builder.Font.Size = 11;
							builder.Writeln(string.Format("{0}. {1}", q.GetId(), q.GetTitle()));

							if (q.HasOptions())
							{
								var c = 'A';
								foreach (var o in q.GetOptions())
								{
									builder.Write(string.Format("  {0}. {1}", c, o));
									c++;
								}
								builder.Writeln();
							}

							builder.InsertBreak(BreakType.LineBreak);
						}
					}
				}

				return doc;
			}
		}
	}
}
