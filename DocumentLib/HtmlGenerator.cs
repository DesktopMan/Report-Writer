using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DocumentLib
{
	public class HtmlGenerator
	{
		public static string GetHtml(string document)
		{
			Parser parser = new Parser();
			
			parser.SetDocument(document);

			if (!parser.Parse())
				return "There were errors parsing the document.";

			StringBuilder html = new StringBuilder();

			html.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">\r\n");
			html.Append("<html>\r\n<head><link href=\"style.css\" rel=\"stylesheet\" type=\"text/css\"></head>\r\n<body>\r\n");

			// Convert @page links to actual page numbers
			document = new Regex("@page\\((.+?)\\)").Replace(document, "<a href='#$1' class='pageref'>here</a>");

			StringBuilder toc = new StringBuilder();

			foreach (KeyValuePair<string, Heading> p in parser.GetHeadings())
			{
				document = document.Replace("@heading(" + p.Value.id + ")", "<a href='#" + p.Value.id + "'>" + p.Value.text + "</a>");
				document = document.Replace(p.Value.match, "<h" + p.Value.level + " id='" + p.Value.id + "'>" + p.Value.text + "</h" + p.Value.level + ">");
				toc.Append("<a href='#" + p.Value.id + "' class='toc_" + p.Value.level + "'>" + p.Value.text + "</a><br/>\r\n");
			}

			document = document.Replace("@toc", toc.ToString());

			StringBuilder figures = new StringBuilder();

			foreach (KeyValuePair<string, Figure> p in parser.GetFigures())
			{
				document = document.Replace("@figure(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='figref'>Figure x</a>");
				document = document.Replace(p.Value.match, "<div id='" + p.Value.id + "' class='figure'><img src='" + p.Value.imagePath + "' alt='" + p.Value.text + "' title='" + p.Value.text + "' /><div class='caption'>" + p.Value.text + "</div></div>");
				figures.Append("<a href='#" + p.Value.id + "' class='figtoc'>" + p.Value.text + "</a><br/>\r\n");
			}

			document = document.Replace("@figures", figures.ToString());

			foreach (KeyValuePair<string, Reference> p in parser.GetReferences())
			{
				document = document.Replace("@reference(" + p.Value.id + ")", "<a href='#" + p.Value.id + "' class='refref'>[" + p.Value.id + "]</a>");
				document = document.Replace(p.Value.match, "<a href='" + p.Value.url + "' id='" + p.Value.id + "' class='reftoc'>" + p.Value.url + " - " + p.Value.text + "</a><br/>");
			}

			// Convert lines to paragraphs
			document = new Regex("^([^<].+)\r$", RegexOptions.Multiline).Replace(document, "<p>$1</p>");

			html.Append(document);
			html.Append("</body></html>");

			return html.ToString();
		}
	}
}
