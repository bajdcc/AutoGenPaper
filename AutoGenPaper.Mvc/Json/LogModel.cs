using AutoGenPaper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Mvc
{
	public class JsonLogAllModel
	{
		public IEnumerable<JsonLogListModel> rows { get; set; }
		public int total { get; set; }
	}

	public class JsonLogListModel
	{
		public int id { get; set; }
		public string time { get; set; }
		public string user { get; set; }
		public string level { get; set; }
		public string @event { get; set; }
		public string @object { get; set; }
		public string text { get; set; }
		public static JsonLogAllModel Empty()
		{
			return new JsonLogAllModel()
			{
				total = 0,
				rows = new List<JsonLogListModel>()
			};
		}
		public static JsonLogAllModel FromModel(int total, IEnumerable<SystemLog> logs)
		{
			var rows = new List<JsonLogListModel>();
			foreach (var log in logs)
			{
				rows.Add(new JsonLogListModel()
				{
					id = log.LogId,
					time = log.Time.ToLongTimeString(),
					user = log.User.UserName,
					level = log.Level.LevelName,
					@event = log.Event.EventName,
					@object = log.Object.ObjectName,
					text = log.Text,
				});
			}
			return new JsonLogAllModel()
			{
				rows = rows,
				total = total
			};
		}
	}
}
