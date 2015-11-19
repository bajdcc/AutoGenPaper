using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoGenPaper.Mvc
{
	public class AGPNavigationTreeNode
	{
		public AGPNavigationTreeNode()
		{
			state = "open";
			@checked = false;
		}
		public int id { set; get; }
		public string text { set; get; }
		public string state { set; get; }
		public bool @checked { set; get; }
		public AGPNavigationTreeNodeAttibute attributes { set; get; }
		public List<AGPNavigationTreeNode> children { set; get; }
	}

	public class AGPNavigationTreeNodeAttibute
	{
		public string url { set; get; }
	}
}
