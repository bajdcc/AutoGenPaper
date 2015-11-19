using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace AutoGenPaper.Common
{
	public class ModelState
	{
		private static readonly int state_ = 0;
		private int state = state_;

		[ScaffoldColumn(false)]
		public int? State { get { return state; } set { state = value ?? state_; } }
	}

	[ComplexType]
	public class ModelTime
	{
		private DateTime createTime;
		private DateTime editTime;

		public ModelTime()
		{
			createTime = editTime = DateTime.Now;
		}

		[DataType(DataType.DateTime), Display(Name = "创建时间")]
		public DateTime? CreateTime { get { return createTime; } set { createTime = value ?? DateTime.Now; } }

		[DataType(DataType.DateTime), Display(Name = "修改时间")]
		public DateTime? EditTime { get { return createTime; } set { createTime = value ?? DateTime.Now; } }
	}

	[ComplexType]
	public class ModelDescription
	{
		[Display(Name = "名称")]
		public string Name { get; set; }

		[Display(Name = "描述")]
		public string Description { get; set; }
	}

	[Table("User")]
	public class User : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int UserId { get; set; }

		[Display(Name = "帐号")]
		public string UserName { get; set; }

		[Display(Name = "真实姓名")]
		public string RealName { get; set; }

		public virtual List<UserGroup> UserGroups { get; set; }

		public virtual List<Question> Questions { get; set; }

		public virtual List<Paper> Papers { get; set; }

		public virtual List<Record> Records { get; set; }
	}

	[Table("Group")]
	public class UserGroup : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int GroupId { get; set; }

		public virtual ModelDescription Info { get; set; }

		public virtual List<User> Users { get; set; }

		[NotMapped]
		public virtual List<string> Items { set; get; }
	}

	[Table("Course")]
	public class Course : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int CourseId { get; set; }

		public virtual ModelDescription Info { get; set; }

		public virtual List<Catalog> Catalogs { get; set; }
	}

	[Table("Catalog")]
	public class Catalog : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int CatalogId { get; set; }

		public virtual ModelDescription Info { get; set; }

		public virtual List<Question> Questions { get; set; }

		public virtual Course Course { get; set; }
	}

	[Table("Question")]
	public class Question : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int QuestionId { get; set; }

		[ScaffoldColumn(false)]
		public int TypeId { get; set; }

		public virtual ModelDescription Info { get; set; }

		public virtual ModelTime Time { get; set; }

		[Display(Name = "选项")]
		public string Option { get; set; }

		[Display(Name = "答案")]
		public string Answer { get; set; }

		[Display(Name = "分值")]
		public decimal Points { get; set; }

		[Display(Name = "难度")]
		public int Difficulty { get; set; }

		[Display(Name = "标签")]
		public string Label { get; set; }

		public int? TargetId { get; set; } 

		public virtual User Owner { get; set; }

		public virtual User Verifier { get; set; }

		public virtual Catalog Catalog { get; set; }

		public virtual List<Paper_Question_Relationship> PQs { get; set; }
	}

	[Table("Paper")]
	public class Paper : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int PaperId { get; set; }

		public virtual ModelDescription Info { get; set; }

		public virtual ModelTime Time { get; set; }

		[Display(Name = "分值")]
		public decimal Points { get; set; }

		[Display(Name = "难度")]
		public int Difficulty { get; set; }

		[Display(Name = "标签")]
		public string Label { get; set; }

		[Display(Name = "出卷人")]
		public virtual User Owner { get; set; }

		[Display(Name = "审核员")]
		public virtual User Verifier { get; set; }

		public virtual List<Record> Records { get; set; }

		public virtual List<Paper_Question_Relationship> PQs { get; set; }
	}


	[Table("Record")]
	public class Record : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int RecordId { get; set; }

		public virtual ModelTime Time { get; set; }

		[Required]
		[Display(Name = "评论")]
		public string Comment { get; set; }

		public virtual User User { get; set; }

		public virtual Paper Paper { get; set; }
	}

	[Table("LogLevel")]
	public class SystemLogLevel
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int LevelId { get; set; }

		[Required]
		public string LevelName { get; set; }
	}

	[Table("LogEvent")]
	public class SystemLogEvent
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int EventId { get; set; }

		[Required]
		public string EventName { get; set; }
	}

	[Table("LogObject")]
	public class SystemLogObject
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int ObjectId { get; set; }

		[Required]
		public string ObjectName { get; set; }
	}

	[Table("Log")]
	public class SystemLog : ModelState
	{
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
		public int LogId { get; set; }

		[Display(Name = "时间")]
		public DateTime Time { get; set; }

		[Display(Name = "用户")]
		public virtual User User { get; set; }

		[Display(Name = "级别")]
		public virtual SystemLogLevel Level { get; set; }

		[Display(Name = "事件")]
		public virtual SystemLogEvent Event { get; set; }

		[Display(Name = "对象")]
		public virtual SystemLogObject Object { get; set; }

		[Display(Name = "信息")]
		public string Text { get; set; }
	}

	[Table("FK_Paper_Question")]
	public class Paper_Question_Relationship : ModelState
	{
		[Key, Column(Order = 0)]
		public int PaperId { get; set; }

		[Key, Column(Order = 1)]
		public int QuestionId { get; set; }

		public virtual Paper Paper { get; set; }

		public virtual Question Question { get; set; }

		[Display(Name = "分值")]
		public decimal Points { get; set; }
	}
}
