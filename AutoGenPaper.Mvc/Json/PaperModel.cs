using AutoGenPaper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Mvc
{
	public class JsonPaperAllModel
	{
		public IEnumerable<JsonPaperListModel> rows { get; set; }
		public int total { get; set; }
	}

	public class JsonPaperListModel
	{
		public int id { get; set; }
		public string name { get; set; }
		public string create { get; set; }
		public string edit { get; set; }
		public string pts { get; set; }
		public string diff { get; set; }
		public string label { get; set; }
		public string owner { get; set; }
		public string verifier { get; set; }
		public static JsonPaperAllModel Empty()
		{
			return new JsonPaperAllModel()
			{
				total = 0,
				rows = new List<JsonPaperListModel>()
			};
		}
		public static JsonPaperAllModel FromModel(int total, IEnumerable<Paper> papers)
		{
			var rows = new List<JsonPaperListModel>();
			foreach (var p in papers)
			{
				rows.Add(new JsonPaperListModel()
				{
					id = p.PaperId,
					name = p.Info.Name,
					create = p.Time.CreateTime.Value.ToShortDateString(),
					edit = p.Time.EditTime.Value.ToShortDateString(),
					pts = p.Points.ToString(),
					diff = p.Difficulty.ToString(),
					label = p.Label,
					owner = p.Owner == null ? string.Empty : string.Format("[{0}]{1}", p.Owner.UserName, p.Owner.RealName),
					verifier = p.Verifier == null ? string.Empty : string.Format("[{0}]{1}", p.Verifier.UserName, p.Verifier.RealName),
				});
			}
			return new JsonPaperAllModel()
			{
				rows = rows,
				total = total
			};
		}
	}
}
