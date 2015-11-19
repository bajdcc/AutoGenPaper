using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Common
{
	public static class AGPDefine
	{
		public enum RoleType
		{
			[AGPEnum(Name = "管理员")]
			Admin,

			[AGPEnum(Name = "教师")]
			Teacher,

			[AGPEnum(Name = "审核员")]
			Verifier,

			[AGPEnum(Name = "教研员")]
			Researcher,
		}

		public enum LogLevelType
		{
			[AGPEnum(Name = "严重")]
			Fatal = 1,

			[AGPEnum(Name = "错误")]
			Error,

			[AGPEnum(Name = "警告")]
			Warning,

			[AGPEnum(Name = "信息")]
			Info,

			[AGPEnum(Name = "调试")]
			Debug
		}

		public enum LogEventType
		{
			[AGPEnum(Name = "登录")]
			Login = 1,

			[AGPEnum(Name = "注销")]
			Logout,

			[AGPEnum(Name = "注册")]
			Register,

			[AGPEnum(Name = "越权")]
			NoAccess,

			[AGPEnum(Name = "锁定")]
			Lock,

			[AGPEnum(Name = "添加")]
			Insert,

			[AGPEnum(Name = "更改")]
			Update,

			[AGPEnum(Name = "删除")]
			Delete,

			[AGPEnum(Name = "其他")]
			Other,
		}
		public enum LogObjectType
		{
			[AGPEnum(Name = "用户")]
			User = 1,

			[AGPEnum(Name = "角色")]
			Role,

			[AGPEnum(Name = "组")]
			Group,

			[AGPEnum(Name = "试题")]
			Question,

			[AGPEnum(Name = "试卷")]
			Paper,

			[AGPEnum(Name = "课程")]
			Course,

			[AGPEnum(Name = "类别")]
			Catalog,
		}

		public enum QuestionType
		{
			[AGPEnum(Name = "未定义")]
			None = 0,

			[AGPEnum(Name = "单选题")]
			SingleSelect = 10,

			[AGPEnum(Name = "多选题")]
			MultiSelect = 11,

			[AGPEnum(Name = "判断题")]
			Check = 12,

			[AGPEnum(Name = "填空题")]
			Blank = 8,

			[AGPEnum(Name = "简答题")]
			ShortAnswer = 21,
		}

		public enum PQsType
		{
			[AGPEnum(Name = "正常")]
			Normal = 0,

			[AGPEnum(Name = "锁定")]
			Locked = 1,

			[AGPEnum(Name = "重复")]
			Repeat = 2,
		}

		public enum CommitType
		{
			[AGPEnum(Name = "正常")]
			Normal = 0,

			[AGPEnum(Name = "锁定")]
			Lock = 1,

			[AGPEnum(Name = "添加")]
			Insert = 10,

			[AGPEnum(Name = "更改")]
			Update = 20,

			[AGPEnum(Name = "删除")]
			Delete = 30,
		}
	}
}
