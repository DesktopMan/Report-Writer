using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
	public class Reference : Content
	{
		public Reference(string id, int position, string match, string text, string url) : base(id, position, match, text)
		{
			this.url = url;
		}

		public override string ToString()
		{
			return id + " - " + text;
		}

		public string url;
	}
}
