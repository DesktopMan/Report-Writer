using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DocumentLib
{
	public class Content
	{
		public Content(string id, int position, string match, string text)
		{
			Regex re = new Regex("[^a-z^0-9^_^-]");

			this.id = re.Replace(id.ToLower().Trim(), "_");
			this.position = position;
			this.match = match;
			this.text = text;

			colored = false;
		}

		public string id;
		public string text;
		public int position;
		public string match;
		public bool colored;
	}
}
