using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Common
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AGPQuestionAttribute : Attribute
	{
		public AGPDefine.QuestionType type { set; get; }
	}
}
