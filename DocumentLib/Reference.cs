using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
	public class Reference
	{
		public Reference(string match, int figNum, string url)
		{
			this.match = match;
			this.figNum = figNum;
			this.url = url;
		}

		public override string ToString()
		{
			return "[" + figNum + "] " + url;
		}

		public string match;
		public int figNum;
		public string url;
	}
}
