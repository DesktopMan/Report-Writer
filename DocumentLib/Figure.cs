using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLib
{
	public class Figure : Content
	{
		public Figure(string id, int position, string match, string text, string imagePath)
			: base(id, position, match, text)
		{
			this.imagePath = imagePath;
		}

		public override string ToString()
		{
			return id + " - " + text;
		}

		public string imagePath;
	}
}
