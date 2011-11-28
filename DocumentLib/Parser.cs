using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DocumentLib
{
	public class Parser
	{
		public void SetDocument(string document)
		{
			this.document = document;
		}

		public Dictionary<string, Heading> GetHeadings()
		{
			return headings;
		}

		public Dictionary<string, Figure> GetFigures()
		{
			return figures;
		}

		public bool Parse()
		{
			headings = new Dictionary<string, Heading>();
			figures = new Dictionary<string, Figure>();
			tables = new Dictionary<string, Table>();
			log = new List<string>();

			if (document == "")
				return true;

			ExtractHeadings();
			ExtractFigures();
			ExtractTables();

			VerifyReferences();

			if (log.Count == 0)
				log.Add("Document parsed successfully");

			return true;
		}

		public string GetLog()
		{
			StringBuilder sb = new StringBuilder();

			foreach (string s in log)
			{
				sb.Append(s + Environment.NewLine);
			}

			return sb.ToString();
		}

		void ExtractHeadings()
        {
            Regex re = new Regex("^([#$]+) ([^\\||.]+?)( \\| (.+?))?\r$", RegexOptions.Multiline);
            MatchCollection mc = re.Matches(document);

			Heading currentHeading = null;

            foreach (Match m in mc)
            {
				string id = m.Groups[4].ToString().Trim();
                string text = m.Groups[2].ToString().Trim();
                int level = m.Groups[1].Length;
                bool showInToc = m.Groups[1].ToString()[0] == '#';

				Heading parent = currentHeading;

				while (parent != null && level <= parent.level)
					parent = parent.parent;

				if (id == "")
				{
					if (parent != null)
						id = parent.GetFullTitle() + " - " + text;
					else
						id = text;
				}

                if (text == "")
                {
                    log.Add("Warning: Heading with no text");
                    continue;
                }

                if (headings.ContainsKey(id))
                {
                    log.Add("Error: Skipping duplicate heading id '" + id + "'");
					continue;
                }

                headings[id] = new Heading(id, m.Index, parent, text, level, showInToc);

				currentHeading = headings[id];

            }
        }

		void ExtractFigures()
		{
			Regex re = new Regex("^@figure\\((.+?),(.+?),(.+?)\\)\r$", RegexOptions.Multiline);
			MatchCollection mc = re.Matches(document);

			foreach (Match m in mc)
			{
				string id = m.Groups[1].ToString().Trim();
				string path = m.Groups[2].ToString().Trim();
				string text = m.Groups[3].ToString().Trim();

				if (figures.ContainsKey(id))
				{
					log.Add("Error: Skipping duplicate figure '" + id + "'");
					continue;
				}

				figures[id] = new Figure(id, m.Index, text, path);
			}
		}

		void ExtractTables()
		{

		}

		void VerifyReferences()
		{
			Regex re = new Regex("@(heading|page|figure|table)\\(([^,|.]+?)\\)");

			MatchCollection mc = re.Matches(document);

			foreach (Match m in mc)
			{
				string type = m.Groups[1].ToString();
				string id = m.Groups[2].ToString();

				if (!headings.ContainsKey(id) && !figures.ContainsKey(id) && !tables.ContainsKey(id))
					log.Add("Error: Unknown reference '" + id + "'");
			}
		}

		string document;

		Dictionary<string, Heading> headings = null;
		Dictionary<string, Figure> figures = null;
		Dictionary<string, Table> tables = null;

		List<string> log;
	}
}
