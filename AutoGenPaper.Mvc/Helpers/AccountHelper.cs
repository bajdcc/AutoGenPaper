using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using WebMatrix.WebData;

namespace AutoGenPaper.Mvc
{
	public class AccountHelper
	{
		public static void CreateUserAndAccount(string userName, string password)
		{
			WebSecurity.CreateUserAndAccount(userName, password, propertyValues: new { State = 0 });
		}
	}
}
