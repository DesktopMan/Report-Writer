using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

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

		public List<Reference> GetReferences()
		{
			return references;
		}

		public bool Parse()
		{
			headings = new Dictionary<string, Heading>();
			figures = new Dictionary<string, Figure>();
			tables = new Dictionary<string, Table>();
			references = new List<Reference>();
			log = new List<LogLine>();

			if (document == "")
				return true;

			ExtractHeadings();
			ExtractFigures();
			ExtractTables();
			ExtractReferences();

			VerifyReferences();

			return true;
		}

		public List<LogLine> GetLog()
		{
			return log;
		}

		void ExtractHeadings()
		{
			Regex re = new Regex("^([#$]+) ([^\\||.]+?)( \\| (.+?))?$", RegexOptions.Multiline);
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
					log.Add(new LogLine(LogLine.Level.WARN, "Heading with no text", m.ToString().Trim(), m.Index));
					continue;
				}

				if (headings.ContainsKey(id))
				{
					log.Add(new LogLine(LogLine.Level.ERR, "Skipping duplicate heading id '" + id + "'", m.ToString().Trim(), m.Index));
					continue;
				}

				Heading h = new Heading(id, m.Index, m.ToString().Trim(), parent, text, level, showInToc);

				headings[h.id] = h;
				currentHeading = h;
			}
		}

		void ExtractFigures()
		{
			Regex re = new Regex("^@figure\\((.+?),(.+?),(.+?)\\)$", RegexOptions.Multiline);
			MatchCollection mc = re.Matches(document);

			foreach (Match m in mc)
			{
				string id = m.Groups[1].ToString().Trim();
				string path = m.Groups[2].ToString().Trim();
				string text = m.Groups[3].ToString().Trim();

				if (figures.ContainsKey(id))
				{
					log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Skipping duplicate figure id '" + id + "'", m.Index));
					continue;
				}

				if (!File.Exists(path))
					log.Add(new LogLine(LogLine.Level.WARN, m.ToString().Trim(), "Unable to find figure file '" + path + "'", m.Index));

				Figure f = new Figure(id, m.Index, m.ToString().Trim(), text, path);
				figures[f.id] = f;
			}
		}

		void ExtractTables()
		{

		}

		void ExtractReferences()
		{
			Regex re = new Regex("@\\(\"(.+?)\"\\)", RegexOptions.Multiline);
			MatchCollection mc = re.Matches(document);

			int figNum = 1;

			foreach (Match m in mc)
			{
				string url = m.Groups[1].ToString().Trim();

				bool match = false;

				for (int i = 0; i < references.Count; i++)
				{
					if (references[i].url == url)
					{
						match = true;
						break;
					}
				}

				if (!match)
					references.Add(new Reference(m.ToString().Trim(), figNum++, url));
			}
		}

		void VerifyReferences()
		{
			Regex re = new Regex("@(.+?)\\(([^,|.]+?)\\)");

			MatchCollection mc = re.Matches(document);

			foreach (Match m in mc)
			{
				string type = m.Groups[1].ToString();
				string id = m.Groups[2].ToString();

				// Ignore inline url references
				if (type.Length == 0 || type[0] == '(')
					continue;

				switch (type)
				{
					case "figure":
						if (!figures.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown figure '" + id + "'", m.Index));
						break;

					case "table":
						if (!tables.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown table '" + id + "'", m.Index));
						break;

					case "heading":
						if (!headings.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown heading '" + id + "'", m.Index));
						break;

					case "page":
						if (!figures.ContainsKey(id) && !tables.ContainsKey(id) && !headings.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown id '" + id + "'", m.Index));
						break;

					default:
						log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown command '" + type + "'", m.Index));
						break;
				}
			}
		}

		string document;

		Dictionary<string, Heading> headings = null;
		Dictionary<string, Figure> figures = null;
		Dictionary<string, Table> tables = null;
		List<Reference> references = null;

		List<LogLine> log;
	}
}
