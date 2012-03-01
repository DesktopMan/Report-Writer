using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace DocumentLib
{
	public class HtmlGenerator
	{
		public static string GetHtml(Parser parser)
		{
			string document = parser.GetDocument();

			StringBuilder html = new StringBuilder();

			html.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">\r\n");
			html.Append("<html>\r\n<head>\r\n<title>Title</title>\r\n<link href=\"style.css\" rel=\"stylesheet\" type=\"text/css\">\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n</head>\r\n<body>\r\n");

			if (!parser.GetSuccess())
			{
				html.Append("<p>Errors occurred while parsing the document. Please correct these before trying to export.</p>\r\n");
			}
			else
			{
				// Convert @pageref links to actual page numbers
				document = new Regex("@pageref\\((.+?)\\)").Replace(document, "<a href='#$1' class='pageref'>this link</a>");

				// Center lines
				document = new Regex("^--\\s*(.*?)\\s*$", RegexOptions.Multiline).Replace(document, "<p style='text-align: center'>$1</p>");

				// Convert lines to paragraphs (ignoring any figures, tables, or raw html)
				MatchCollection paragraphs = new Regex("(^([^@figure\\(|^@table\\(|^\n|^#|^\\$|^\\<].+?)\n)+", RegexOptions.Multiline).Matches(document);

				for (int i = paragraphs.Count - 1; i >= 0; i--)
				{
					Match m = paragraphs[i];

					document = document.Remove(m.Index, m.Length);
					document = document.Insert(m.Index, "<p>\n" + m.Groups[0].Value.ToString().Substring(0, m.Groups[0].Value.Length - 1).Replace("\n", "<br>\n") + "\n</p>\n");
				}

				// Convert lines only containing # to vertical padding paragraphs
				document = new Regex("^#\n", RegexOptions.Multiline).Replace(document, "<p><br></p>\n");

				document = ProcessChapters(document, parser.GetChapters());
				document = ProcessFigures(document, parser.GetFigures());
				document = ProcessTables(document, parser.GetTables());
				document = ProcessReferences(document, parser.GetReferences());

				html.Append(document);
			}

			html.Append("</body></html>");

			return html.ToString();
		}

		public static string GetWebPageTitle(string url)
		{
			// Create a request to the url
			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

			// If the request wasn't an HTTP request (like a file), ignore it
			if (request == null) return null;

			// Use the user's credentials
			request.UseDefaultCredentials = true;

			// Obtain a response from the server, if there was an error, return nothing
			HttpWebResponse response = null;

			try { response = request.GetResponse() as HttpWebResponse; }

			catch (WebException) { return null; }

			// Regular expression for an HTML title
			string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";

			// If the correct HTML header exists for HTML text, continue
			if (new List<string>(response.Headers.AllKeys).Contains("Content-Type"))

				if (response.Headers["Content-Type"].StartsWith("text/html"))
				{
					// Download the page
					WebClient web = new WebClient();

					string page = web.DownloadString(url);

					// Extract the title
					Regex ex = new Regex(regex, RegexOptions.IgnoreCase);

					return ex.Match(page).Value.Trim();
				}

			// Not a valid HTML page
			return null;
		}

		private static string ProcessChapters(string document, Dictionary<string, Chapter> chapters)
		{
			string toInsert = "";
			// Each item has a built in position, but that's not accurate after
			// the document has been changed. Search for it instead.
			// This is guaranteed to work as the chapters are found in
			// sequential order.
			int position;

			StringBuilder toc = new StringBuilder();
			StringBuilder output = new StringBuilder(document);

			toc.Append("<div>\r\n");

			foreach (KeyValuePair<string, Chapter> p in chapters)
			{
				// First update all chapter references
				output.Replace("@chapref(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='chapref'>" + p.Value.text + "</a>");

				// Get the first match. There might be more in case sub headings
				// have the same title.
				position = output.ToString().IndexOf(p.Value.match + "\n");

				// Get rid of the header line
				output.Remove(position, p.Value.match.Length);

				if (p.Value.showInToc)
				{
					toInsert = "<h" + p.Value.level + " id='" + p.Value.id + "'>" + p.Value.text + "</h" + p.Value.level + ">";
					toc.Append("<a href='#" + p.Value.id + "' class='toc_" + p.Value.level + "'>" + p.Value.text + "</a><br>\r\n");
				}
				else
					toInsert = "<h" + p.Value.level + " id='" + p.Value.id + "' class='notoc'>" + p.Value.text + "</h" + p.Value.level + ">";

				output.Insert(position, toInsert);
			}

			toc.Append("</div>\r\n");

			return output.Replace("@toc", toc.ToString()).ToString();
		}

		private static string ProcessFigures(string document, Dictionary<string, Figure> figures)
		{

			StringBuilder figtoc = new StringBuilder();

			figtoc.Append("<div>\r\n");

			foreach (KeyValuePair<string, Figure> p in figures)
			{
				document = document.Replace("@figref(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='figref'>this figure</a>");
				document = document.Replace(p.Value.match, "<div id='" + p.Value.id + "' class='figure'><img src='" + p.Value.imagePath + "' alt='" + p.Value.text + "' title='" + p.Value.text + "'><div class='caption'>" + p.Value.text + "</div></div>");
				figtoc.Append("<a href='#" + p.Value.id + "' class='figtoc'>" + p.Value.text + "</a><br>\r\n");
			}

			figtoc.Append("</div>\r\n");

			return document.Replace("@figures", figtoc.ToString());
		}

		private static string ProcessTables(string document, Dictionary<string, Table> tables)
		{
			StringBuilder tabletoc = new StringBuilder();

			tabletoc.Append("<div>\r\n");

			foreach (KeyValuePair<string, Table> p in tables)
			{
				document = document.Replace("@tableref(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='tableref'>this table</a>");

				StringBuilder table = new StringBuilder();

				table.Append("<div id='" + p.Value.id + "' class='table'>\r\n");
				table.Append("<table>\r\n");

				for (int row = 0; row < p.Value.table.Count; row++)
				{
					table.Append("<tr>");
					for (int col = 0; col < p.Value.table[row].Count; col++)
					{
						if (row == 0 && p.Value.headerTop || col == 0 && p.Value.headerLeft ||
							row == p.Value.table.Count - 1 && p.Value.headerBottom ||
							col == p.Value.table[row].Count - 1 && p.Value.headerRight)
							table.Append("<th>" + p.Value.table[row][col] + "</th>");
						else
							table.Append("<td>" + p.Value.table[row][col] + "</td>");
					}
					table.Append("</tr>\r\n");
				}
				
				table.Append("</table>\r\n");
				table.Append("<div class='caption'>" + p.Value.text + "</div>\r\n");
				table.Append("</div>");

				document = document.Replace(p.Value.match, table.ToString());
				tabletoc.Append("<a href='#" + p.Value.id + "' class='tabletoc'>" + p.Value.text + "</a><br>\r\n");
			}

			tabletoc.Append("</div>\r\n");

			return document.Replace("@tables", tabletoc.ToString());
		}

		private static string ProcessReferences(string document, List<Reference> references)
		{

			StringBuilder reflist = new StringBuilder();

			reflist.Append("<div>\r\n");

			foreach (Reference r in references)
			{
				if (r.content.StartsWith("http"))
				{
					reflist.AppendFormat("<a id='reference_{0}' href='{1}'>{2}</a><br>\r\n", r.figNum, r.content, r.ToString() + " - " + GetWebPageTitle(r.content));
					document = document.Replace(r.match, "<a href='" + r.content + "'>[" + r.figNum + "]</a>");
				}
				else
				{
					reflist.AppendFormat("<span id='reference_{0}'>[{0}] {1}</span><br>\r\n", r.figNum, r.content);
					document = document.Replace(r.match, "<a href='#reference_" + r.figNum + "' title='" + r.content + "'>[" + r.figNum + "]</a>");
				}
			}

			reflist.Append("</div>\r\n");

			return document.Replace("@references", reflist.ToString());
		}
	}
}
