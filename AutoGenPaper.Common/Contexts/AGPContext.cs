using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace AutoGenPaper.Common
{
	public class AGPDataContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<UserGroup> Groups { get; set; }

		public DbSet<Course> Courses { get; set; }

		public DbSet<Catalog> Catalogs { get; set; }

		public DbSet<Question> Questions { get; set; }

		public DbSet<Paper> Papers { get; set; }

		public DbSet<Record> Records { get; set; }

		public DbSet<SystemLog> Logs { get; set; }

		public DbSet<SystemLogLevel> LogLevels { get; set; }

		public DbSet<SystemLogEvent> LogEvents { get; set; }

		public DbSet<SystemLogObject> LogObjects { get; set; }

		public DbSet<Paper_Question_Relationship> PQs { get; set; }

		public AGPDataContext()
			: base("DefaultConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			/* 一对一关系 */

			/* 一对多关系 */

			// User : Paper = 1 : n
			modelBuilder.Entity<User>()
				.HasMany(a => a.Papers)
				.WithRequired(a => a.Owner)
				.Map(a => a.MapKey());

			modelBuilder.Entity<User>()
				.HasMany(a => a.Papers)
				.WithOptional(a => a.Verifier)
				.Map(a => a.MapKey());

			// User : Question = 1 : n
			modelBuilder.Entity<User>()
				.HasMany(a => a.Questions)
				.WithOptional(a => a.Owner)
				.Map(a => a.MapKey());

			modelBuilder.Entity<User>()
				.HasMany(a => a.Questions)
				.WithOptional(a => a.Verifier)
				.Map(a => a.MapKey());

			// User : Record = 1 : n
			modelBuilder.Entity<User>()
				.HasMany(a => a.Records)
				.WithOptional(a => a.User)
				.Map(a => a.MapKey());

			// Paper : Record = 1 : n
			modelBuilder.Entity<Paper>()
				.HasMany(a => a.Records)
				.WithRequired(a => a.Paper)
				.Map(a => a.MapKey());

			// SystemLog : User = n : 1
			modelBuilder.Entity<SystemLog>()
				.HasRequired(a => a.User)
				.WithMany()
				.Map(a => a.MapKey());

			// SystemLog : SystemLogLevel = n : 1
			modelBuilder.Entity<SystemLog>()
				.HasRequired(a => a.Level)
				.WithMany()
				.Map(a => a.MapKey());

			// SystemLog : SystemLogEvent = n : 1
			modelBuilder.Entity<SystemLog>()
				.HasRequired(a => a.Event)
				.WithMany()
				.Map(a => a.MapKey());

			// SystemLog : SystemLogObject = n : 1
			modelBuilder.Entity<SystemLog>()
				.HasRequired(a => a.Object)
				.WithMany()
				.Map(a => a.MapKey());

			// Course : Catalog = 1 : n
			modelBuilder.Entity<Course>()
				.HasMany(a => a.Catalogs)
				.WithRequired(a => a.Course)
				.Map(a => a.MapKey());

			// Catalog : Question = 1 : n
			modelBuilder.Entity<Catalog>()
				.HasMany(a => a.Questions)
				.WithRequired(a => a.Catalog)
				.Map(a => a.MapKey());

			// Paper_Question_Relationship : Question = 1 : n
			modelBuilder.Entity<Paper_Question_Relationship>()
				.HasRequired(a => a.Question)
				.WithMany(a => a.PQs)
				.HasForeignKey(a => a.QuestionId);

			modelBuilder.Entity<Paper_Question_Relationship>()
				.HasRequired(a => a.Paper)
				.WithMany(a => a.PQs)
				.HasForeignKey(a => a.PaperId);

			/* 多对多关系 */

			// UserGroup : User = m : n
			modelBuilder.Entity<UserGroup>()
				.HasMany(a => a.Users)
				.WithMany(a => a.UserGroups)
				.Map(a => a.MapLeftKey().MapRightKey().ToTable("FK_Group_User"));
		}
	}
}
