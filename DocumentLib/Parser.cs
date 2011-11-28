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

            return true;
        }

        void ExtractHeadings()
        {
            Regex re = new Regex("^([#$])+ (.*)$");
            string[] lines = document.Replace("\r", "").Split('\n');

            for (int i = 0; i < lines.Count(); i++)
            {
                Match m = re.Match(lines[i]);

                if (!m.Success)
                    continue;

                string id = m.Groups[2].ToString().ToLower().Replace(' ', '_');
                string text = m.Groups[2].ToString();
                int level = m.Groups[2].Length;
                bool showInToc = m.Groups[1].ToString()[0] == '#';

                if (text == "")
                {
                    log.Add("Warning: Heading with no text");
                    continue;
                }

                if (headings.ContainsKey(id))
                {
                    log.Add("Error: Duplicate heading id '" + id + "'");
                }

                headings[id] = new Heading(id, i, text, level, showInToc);
            }
        }

        void ExtractFigures()
        {

        }

        void ExtractTables()
        {

        }

        string document;

        Dictionary<string, Heading> headings = null;
        Dictionary<string, Figure> figures = null;
        Dictionary<string, Table> tables = null;

        List<string> log;
    }
}
