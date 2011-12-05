using System;
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

			StringBuilder toc = new StringBuilder();

			toc.Append("<div>\r\n");

			foreach (KeyValuePair<string, Heading> p in parser.GetHeadings())
			{
				document = document.Replace("@heading(" + p.Value.id + ")", "<a href='#" + p.Value.id + "'>" + p.Value.text + "</a>");
				document = document.Replace(p.Value.match, "<h" + p.Value.level + " id='" + p.Value.id + "'>" + p.Value.text + "</h" + p.Value.level + ">");
				toc.Append("<a href='#" + p.Value.id + "' class='toc_" + p.Value.level + "'>" + p.Value.text + "</a><br>\r\n");
			}

			toc.Append("</div>\r\n");

			document = document.Replace("@toc", toc.ToString());

			StringBuilder figures = new StringBuilder();

			figures.Append("<div>\r\n");

			foreach (KeyValuePair<string, Figure> p in parser.GetFigures())
			{
				document = document.Replace("@figure(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='figref'>Figure x</a>");
				document = document.Replace(p.Value.match, "<div id='" + p.Value.id + "' class='figure'><img src='" + p.Value.imagePath + "' alt='" + p.Value.text + "' title='" + p.Value.text + "'><div class='caption'>" + p.Value.text + "</div></div>");
				figures.Append("<a href='#" + p.Value.id + "' class='figtoc'>" + p.Value.text + "</a><br>\r\n");
			}

			figures.Append("</div>\r\n");

			document = document.Replace("@figures", figures.ToString());

			StringBuilder references = new StringBuilder();

			references.Append("<div>\r\n");

			foreach (Reference r in parser.GetReferences())
			{
				document = document.Replace(r.match, "<a href='" + r.url + "'>[" + r.figNum + "]</a>");
				references.Append("<a href='" + r.url + "'>" + r.ToString() + " - " + GetWebPageTitle(r.url) + "</a><br>\r\n");
			}

			references.Append("</div>\r\n");

			document = document.Replace("@references", references.ToString());

			// Convert lines to paragraphs
			document = new Regex("^([^<].+)\r$", RegexOptions.Multiline).Replace(document, "<p>$1</p>");

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
	}
}
