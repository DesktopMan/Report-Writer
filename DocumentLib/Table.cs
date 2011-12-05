using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DocumentLib
{
	public class Table : Content
	{
		public Table(string id, int position, string match, string text, string csvPath)
			: base(id, position, match, text)
		{
			int rowCount = 0;

			foreach (string row in File.ReadAllLines(csvPath))
			{
				int celCount = 0;
				table[rowCount] = new Dictionary<int, string>();

				foreach (string cell in row.Split(','))
					table[rowCount][celCount++] = cell;

				rowCount++;
			}
		}

		public override string ToString()
		{
			return id + " - " + text;
		}

		public Dictionary<int, Dictionary<int, string>> table = new Dictionary<int, Dictionary<int, string>>();
	}
}
