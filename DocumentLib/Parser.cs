using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace DocumentLib
{
	public class Parser
	{
		public void SetDocument(string document, string basePath)
		{
			// Ensure newline at end of file. Parser/exporter expects it
			if (document.Length > 0 && document[document.Length - 1] != '\n')
				document += "\n";

			this.document = document;
			this.basePath = basePath;
			success = false;
		}

		public string GetDocument()
		{
			return document;
		}

		public Dictionary<string, Chapter> GetChapters()
		{
			return chapters;
		}

		public Dictionary<string, Figure> GetFigures()
		{
			return figures;
		}

		public Dictionary<string, Table> GetTables()
		{
			return tables;
		}

		public List<Reference> GetReferences()
		{
			return references;
		}

		public bool GetSuccess()
		{
			return success;
		}

		public bool Parse(bool processTemplates = false)
		{
			chapters = new Dictionary<string, Chapter>();
			figures = new Dictionary<string, Figure>();
			tables = new Dictionary<string, Table>();
			references = new List<Reference>();
			log = new List<LogLine>();

			if (document == "")
				return true;

			if (processTemplates)
				document = ProcessTemplates(document);

			ExtractChapters();
			ExtractFigures();
			ExtractTables();
			ExtractReferences();

			VerifyReferences();

			success = log.Count == 0;

			return success;
		}

		public List<LogLine> GetLog()
		{
			return log;
		}

		string ProcessTemplates(string document)
		{
			// Find and insert all external files
			MatchCollection fileMatches = new Regex("^@file\\((.+?)\\)$", RegexOptions.Multiline).Matches(document);

			foreach (Match m in fileMatches)
			{
				string match = m.Groups[0].ToString();
				string fileName = m.Groups[1].ToString().Trim();
				string fullPath = Path.Combine(basePath, fileName);

				if (File.Exists(fullPath))
					document = document.Replace(match, File.ReadAllText(fullPath).Replace("\r", ""));
			}

			// Find and replace all variables defined
			MatchCollection variableMatches = new Regex(@"^\$(.+?)=(.+)$", RegexOptions.Multiline).Matches(document);

			foreach (Match m in variableMatches)
			{
				document = document.Replace(m.Groups[0].ToString(), "");
				string key = m.Groups[1].ToString().Trim();
				string value = m.Groups[2].ToString().Trim();

				document = document.Replace("$" + key, value);
			}

			return document;
		}

		void ExtractChapters()
		{
			Regex re = new Regex("^([#$]+) ([^\\||.]+?)( \\| (.+?))?$", RegexOptions.Multiline);
			MatchCollection mc = re.Matches(document);

			Chapter previousChapter = null;

			foreach (Match m in mc)
			{
				string id = m.Groups[4].ToString().Trim();
				string text = m.Groups[2].ToString().Trim();
				int level = m.Groups[1].Length;
				bool showInToc = m.Groups[1].ToString()[0] == '#';

				Chapter parent = previousChapter;

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
					log.Add(new LogLine(LogLine.Level.WARN, "Chapter with no text", m.ToString().Trim(), m.Index));
					continue;
				}

				Chapter h = new Chapter(id, m.Index, m.ToString(), parent, text, level, showInToc);

				if (chapters.ContainsKey(h.id))
				{
					log.Add(new LogLine(LogLine.Level.ERR, m.ToString(), "Skipping duplicate chapter id '" + id + "'", m.Index));
					continue;
				}

				chapters[h.id] = h;
				previousChapter = h;
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

				if (!File.Exists(Path.Combine(basePath, path)))
					log.Add(new LogLine(LogLine.Level.WARN, m.ToString().Trim(), "Unable to find figure file '" + path + "'", m.Index));

				Figure f = new Figure(id, m.Index, m.ToString().Trim(), text, path);
				figures[f.id] = f;
			}
		}

		void ExtractTables()
		{
			Regex re = new Regex("^@table\\((.+?),(.+?),(.+?)(,(.+?))?\\)$", RegexOptions.Multiline);
			MatchCollection mc = re.Matches(document);

			foreach (Match m in mc)
			{
				string id = m.Groups[1].ToString().Trim();
				string path = Path.Combine(basePath, m.Groups[2].ToString().Trim());
				string text = m.Groups[3].ToString().Trim();
				string headers = m.Groups[5].ToString().Trim();

				if (tables.ContainsKey(id))
				{
					log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Skipping duplicate table id '" + id + "'", m.Index));
					continue;
				}

				if (!File.Exists(path))
					log.Add(new LogLine(LogLine.Level.WARN, m.ToString().Trim(), "Unable to find table file '" + path + "'", m.Index));

				Table t = new Table(id, m.Index, m.ToString().Trim(), text, path, headers);
				tables[t.id] = t;
			}
		}

		void ExtractReferences()
		{
			Regex re = new Regex("@\\(\"(.+?)\"\\)", RegexOptions.Multiline);
			MatchCollection mc = re.Matches(document);

			int figNum = 1;

			foreach (Match m in mc)
			{
				string content = m.Groups[1].ToString().Trim();

				bool match = false;

				for (int i = 0; i < references.Count; i++)
				{
					if (references[i].content == content)
					{
						match = true;
						break;
					}
				}

				if (!match)
					references.Add(new Reference(m.ToString().Trim(), figNum++, content));
			}
		}

		void VerifyReferences()
		{
			Regex re = new Regex("@([^\\(|^ |.]+?)\\(([^,|.]+?)\\)");

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
					case "figref":
						if (!figures.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown figure '" + id + "'", m.Index));
						break;

					case "tableref":
						if (!tables.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown table '" + id + "'", m.Index));
						break;

					case "chapref":
						if (!chapters.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown chapter '" + id + "'", m.Index));
						break;

					case "pageref":
						if (!figures.ContainsKey(id) && !tables.ContainsKey(id) && !chapters.ContainsKey(id))
							log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown id '" + id + "'", m.Index));
						break;

					default:
						log.Add(new LogLine(LogLine.Level.ERR, m.ToString().Trim(), "Unknown command '" + type + "'", m.Index));
						break;
				}
			}
		}

		string document;
		string basePath;
		bool success;

		Dictionary<string, Chapter> chapters = null;
		Dictionary<string, Figure> figures = null;
		Dictionary<string, Table> tables = null;
		List<Reference> references = null;

		List<LogLine> log;
	}
}
