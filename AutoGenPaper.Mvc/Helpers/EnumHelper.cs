using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoGenPaper.Common;

namespace AutoGenPaper.Mvc
{
	public class EnumHelper
	{
		public static IEnumerable<FieldInfo> GetFields(Type type)
		{
			return type.GetFields().Where(a => a.FieldType == type);
		}

		public static string GetEnumDescription(FieldInfo info)
		{
			var attr = Attribute.GetCustomAttribute(info, typeof(AGPEnumAttribute)) as AGPEnumAttribute;
			return attr == null ? null : attr.Name;
		}

		public static string GetEnumDescriptionEx<T>(T value)
		{
			try
			{
				var fields = GetFields(typeof(T));
				var ctype = fields.Where(a =>
					{
						var t = a.GetRawConstantValue();
						return (int)a.GetRawConstantValue() == Convert.ToInt32(value);
					}
					).Single();
				return (Attribute.GetCustomAttribute(ctype, typeof(AGPEnumAttribute)) as AGPEnumAttribute).Name;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
