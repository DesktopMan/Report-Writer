using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLib
{
	public class Reference
	{
		public Reference(string match, int figNum, string content)
		{
			this.match = match;
			this.figNum = figNum;
			this.content = content;
		}

		public override string ToString()
		{
			return "[" + figNum + "] " + content;
		}

		public string match;
		public int figNum;
		public string content;
	}
}
