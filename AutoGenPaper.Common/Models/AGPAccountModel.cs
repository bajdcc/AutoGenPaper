using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace AutoGenPaper.Common
{
	public class LocalPasswordModel
	{
		[DataType(DataType.Password)]
		[Display(Name = "当前密码")]
		[Required]
		public string OldPassword { get; set; }

		[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "新密码")]
		[Required]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "确认新密码")]
		[Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
		[Required]
		public string ConfirmPassword { get; set; }
	}

	public class LoginModel
	{
		[Display(Name = "用户名（学号/工号）")]
		[Required]
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "密码")]
		[Required]
		public string Password { get; set; }

		[Display(Name = "记住我?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterModel
	{
		[Display(Name = "用户角色")]
		[Range(1, 3, ErrorMessage = "非法角色")]
		[Required]
		public int UserRole { get; set; }
	
		[Display(Name = "用户名（学号/工号）")]
		[RegularExpression(@"\d{6,20}", ErrorMessage = "全为数字，长度6-20")]
		[Required]
		public string UserName { get; set; }

		[Display(Name = "真实姓名")]
		[StringLength(20, MinimumLength = 2)]
		[Required]
		public string RealName { get; set; }

		[DataType(DataType.Password)]
		[StringLength(20, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
		[Display(Name = "密码")]
		[Required]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "确认密码")]
		[StringLength(20, MinimumLength = 6)]
		[Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
		[Required]
		public string ConfirmPassword { get; set; }
	}
}
