using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Common
{
	public class QuestionModelException : Exception
	{
		private static string Reason = "选项或答案不符合要求";

		public QuestionModelException()
			: base(Reason)
		{ }
	}
}
