using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLib
{
	public class Todo
	{
		public Todo(int location, string description)
		{
			this.location = location;
			this.description = description;
		}

		public override string ToString()
		{
			return description;
		}

		public int location;
		public string description;
	}
}
