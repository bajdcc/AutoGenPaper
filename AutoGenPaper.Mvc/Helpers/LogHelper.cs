using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoGenPaper.Common;

namespace AutoGenPaper.Mvc
{
	public class LogHelper
	{
		public static bool Log(AGPDataContext db, string userName, AGPDefine.LogLevelType logLevel,
			AGPDefine.LogEventType logEvent, AGPDefine.LogObjectType logObject, string message)
		{
			try
			{
				var u = db.Users.Single(a => a.UserName == userName);
				var l = db.LogLevels.Find((int)logLevel);
				var e = db.LogEvents.Find((int)logEvent);
				var o = db.LogObjects.Find((int)logObject);
				db.Logs.Add(new SystemLog()
				{
					User = u,
					Time = DateTime.Now,
					Level = l,
					Event = e,
					Object = o,
					Text = message
				});
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
