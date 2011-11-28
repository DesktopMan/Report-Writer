using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
	class Table : Content
	{
		public Table(string id, int position, string text)
			: base(id, position, text)
		{
		}

		public override string ToString()
		{
			return id + " - " + text;
		}
	}
}
