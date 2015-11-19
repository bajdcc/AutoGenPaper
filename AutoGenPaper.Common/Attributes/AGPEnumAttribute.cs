using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Common
{
	[AttributeUsage(AttributeTargets.Field)]
	public class AGPEnumAttribute : Attribute
	{
		public string Name { set; get; }
	}
}
