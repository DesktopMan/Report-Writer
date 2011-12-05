﻿using System;
using System.Collections.Generic;
using System.Linq;
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

			// Convert @page links to actual page numbers
			document = new Regex("@page\\((.+?)\\)").Replace(document, "<a href='#$1' class='pageref'>this link</a>");

			document = ProcessHeadings(document, parser.GetHeadings());
			document = ProcessFigures(document, parser.GetFigures());
			document = ProcessTables(document, parser.GetTables());
			document = ProcessReferences(document, parser.GetReferences());

			// Convert lines to paragraphs
			document = new Regex("^([^<^\n].+)$", RegexOptions.Multiline).Replace(document, "<p>$1</p>");

			html.Append(document);
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

		private static string ProcessHeadings(string document, Dictionary<string, Heading> headings)
		{
			StringBuilder toc = new StringBuilder();

			toc.Append("<div>\r\n");

			foreach (KeyValuePair<string, Heading> p in headings)
			{
				document = document.Replace("@heading(" + p.Value.id + ")", "<a href='#" + p.Value.id + "'>" + p.Value.text + "</a>");
				document = document.Replace(p.Value.match, "<h" + p.Value.level + " id='" + p.Value.id + "'>" + p.Value.text + "</h" + p.Value.level + ">");
				toc.Append("<a href='#" + p.Value.id + "' class='toc_" + p.Value.level + "'>" + p.Value.text + "</a><br>\r\n");
			}

			toc.Append("</div>\r\n");

			return document.Replace("@toc", toc.ToString());
		}

		private static string ProcessFigures(string document, Dictionary<string, Figure> figures)
		{

			StringBuilder figtoc = new StringBuilder();

			figtoc.Append("<div>\r\n");

			foreach (KeyValuePair<string, Figure> p in figures)
			{
				document = document.Replace("@figure(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='figref'>Figure x</a>");
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
				document = document.Replace("@table(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='tableref'>Table x</a>");

				StringBuilder table = new StringBuilder();

				table.Append("<div id='" + p.Value.id + "' class='table'>\r\n");
				table.Append("<table>\r\n");

				for (int row = 0; row < p.Value.table.Count; row++)
				{
					table.Append("<tr>");
					for (int col = 0; col < p.Value.table[row].Count; col++)
					{
						if (row == 0)
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
				document = document.Replace(r.match, "<a href='" + r.url + "'>[" + r.figNum + "]</a>");
				reflist.Append("<a href='" + r.url + "'>" + r.ToString() + " - " + GetWebPageTitle(r.url) + "</a><br>\r\n");
			}

			reflist.Append("</div>\r\n");

			return document.Replace("@references", reflist.ToString());
		}
	}
}
