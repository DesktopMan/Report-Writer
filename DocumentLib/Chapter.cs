using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLib
{
	public class Chapter : Content
	{
		public Chapter(string id, int position, string match, Chapter parent, string text, int level, bool showInToc)
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
		public Chapter parent;
	}
}
