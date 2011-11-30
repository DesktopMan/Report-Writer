using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
	public class Content
	{
		public Content(string id, int position, string match, string text)
		{
			this.id = id;
			this.position = position;
			this.match = match;
			this.text = text;
		}

		public string id;
		public string text;
		public int position;
		public string match;
	}
}
