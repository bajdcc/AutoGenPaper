﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AutoGenPaper.Mvc
{
	public class FakeView : IView
	{
		public void Render(ViewContext viewContext, TextWriter writer)
		{
			throw new InvalidOperationException();
		}
	}
}
