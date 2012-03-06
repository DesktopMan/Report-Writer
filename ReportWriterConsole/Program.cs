using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ReportWriterConsole
{
	class Program
	{
		static int GetLineNumber(string s, int pos)
		{
			int line = 1;
			int linePos = 0;

			while ((linePos = s.IndexOf('\n', linePos + 1)) != -1)
			{
				line++;

				if (linePos > pos)
					return line - 1;
			}

			return line;
		}

		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("usage: ReportWriterConsole.exe <input> <output>");
				return;
			}
			if (!File.Exists(args[0]))
			{
				Console.WriteLine("Input file does not exist: " + args[0]);
				return;
			}

			DocumentLib.Parser fullParser = new DocumentLib.Parser();
			string document = File.ReadAllText(args[0]);

			fullParser.SetDocument(document, Path.GetDirectoryName(args[0]));
			fullParser.Parse(true);

			string html = DocumentLib.HtmlGenerator.GetHtml(fullParser);

			File.WriteAllText(args[1], html);

			fullParser.GetLog().Reverse();

			foreach (DocumentLib.LogLine line in fullParser.GetLog())
			{
				int lineNum = GetLineNumber(document, line.position);
				string logLine = line.GetLevel() + ":" + lineNum + " " + line.text;
				Console.WriteLine(logLine);
			}

			Console.WriteLine("Exported document to '" + args[1] + "'");
		}
	}
}
