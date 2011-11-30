using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
	public class Heading : Content
	{
		public Heading(string id, int position, string match, Heading parent, string text, int level, bool showInToc)
			: base(id, position, match, text)
		{
			this.level = level;
			this.showInToc = showInToc;
		}

		public string GetFullTitle()
		{
			if (parent != null)
				return parent.GetFullTitle() + " - " + text;
			else
				return text;
		}

		public override string ToString()
		{
			return text.PadLeft((level - 1) * 4 + text.Length);
		}

		public int level;
		public bool showInToc;
		public Heading parent;
	}
}
