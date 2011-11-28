using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
	public class Figure : Content
	{
		public Figure(string id, int position, string text, string imagePath)
			: base(id, position, text)
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
