using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoGenPaper.Common;
using System.Security.Principal;
using System.Web.Security;

namespace AutoGenPaper.Mvc
{
	public class RoleHelper
	{
		public static AGPDefine.RoleType GetRole(IPrincipal user)
		{
			var roleName = Roles.GetRolesForUser().First();
			return (AGPDefine.RoleType)Enum.ToObject(typeof(AGPDefine.RoleType),
				EnumHelper.GetFields(typeof(AGPDefine.RoleType)).Where(
				a => EnumHelper.GetEnumDescription(a) == roleName).Single().GetRawConstantValue());
		}
	}
}
