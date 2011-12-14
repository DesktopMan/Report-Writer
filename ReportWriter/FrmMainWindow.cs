using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Report_Writer
{
	public partial class FrmMainWindow : Form
	{
		DocumentLib.Parser parser = new DocumentLib.Parser();
		string filePath;

		public FrmMainWindow()
		{
			InitializeComponent();
		}

		private void FrmMainWindow_Load(object sender, EventArgs e)
		{
			LoadDocument("Example/document.txt");
		}

		private void txtDocument_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.V && e.Control)
			{
				e.SuppressKeyPress = true;
			}

			if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
				return;

			UpdateInterface();
		}

		private void txtDocument_TextChanged(object sender, EventArgs e)
		{
			changed = true;
		}

		private void lbNavigation_SelectedIndexChanged(object sender, EventArgs e)
		{
			DocumentLib.Chapter chapter = (DocumentLib.Chapter)lbNavigation.SelectedItem;

			if (chapter == null)
				return;

			Navigate(chapter.position);
		}

		private void UpdateInterface()
		{
			if (changed == false)
				return;

			parser.SetDocument(txtDocument.Text, Path.GetDirectoryName(filePath));
			parser.Parse();

			lbLog.Items.Clear();

			foreach (DocumentLib.LogLine l in parser.GetLog())
				lbLog.Items.Add(l);

			if (lbLog.Items.Count == 0)
				tsslblTip.Text = "Document parsed successfully";
			else
				tsslblTip.Text = "Some issues were found while parsing this document";

			lbNavigation.Items.Clear();
			lbFigures.Items.Clear();
			lbTables.Items.Clear();
			lbReferences.Items.Clear();

			foreach (KeyValuePair<string, DocumentLib.Chapter> pair in parser.GetChapters())
			{
				BackColorText(pair.Value.position, pair.Value.match.Length, Color.LightGreen);
				lbNavigation.Items.Add(pair.Value);
			}

			foreach (KeyValuePair<string, DocumentLib.Figure> pair in parser.GetFigures())
			{
				BackColorText(pair.Value.position, pair.Value.match.Length, Color.Yellow);
				lbFigures.Items.Add(pair.Value);
			}

			foreach (KeyValuePair<string, DocumentLib.Table> pair in parser.GetTables())
			{
				BackColorText(pair.Value.position, pair.Value.match.Length, Color.Yellow);
				lbTables.Items.Add(pair.Value);
			}

			foreach (DocumentLib.Reference r in parser.GetReferences())
			{
				lbReferences.Items.Add(r);
			}

			string html = DocumentLib.HtmlGenerator.GetHtml(parser);
			File.WriteAllText(Path.Combine(Path.GetDirectoryName(filePath), "output.html"), html);

			changed = false;
		}

		private void Navigate(int position)
		{
			txtDocument.Focus();

			txtDocument.Select(txtDocument.Text.Length, 0);
			txtDocument.ScrollToCaret();

			txtDocument.Select(position, 0);
			txtDocument.ScrollToCaret();
		}

		private bool changed = false;

		private void txtDocument_MouseUp(object sender, MouseEventArgs e)
		{
			UpdateInterface();
		}

		private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbLog.SelectedItem != null)
				Navigate(((DocumentLib.LogLine)lbLog.SelectedItem).position);

			lbLog.ClearSelected();
		}

		private void lbFigures_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbFigures.SelectedItem != null)
				Navigate(((DocumentLib.Figure)lbFigures.SelectedItem).position);

			lbFigures.ClearSelected();
		}

		private void lbTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbTables.SelectedItem != null)
				Navigate(((DocumentLib.Table)lbTables.SelectedItem).position);

			lbTables.ClearSelected();
		}

		private void lbReferences_SelectedIndexChanged(object sender, EventArgs e)
		{
			lbReferences.ClearSelected();
		}

		private void BackColorText(int start, int length, Color color)
		{
			this.ActiveControl = null;

			int currentPosition = txtDocument.SelectionStart;

			txtDocument.Select(start, length);
			txtDocument.SelectionBackColor = color;

			txtDocument.Select(currentPosition, 0);

			txtDocument.Focus();
		}

		private void LoadDocument(string path)
		{
			if (!File.Exists(path))
				return;

			filePath = path;

			txtDocument.Text = File.ReadAllText(path);
			UpdateInterface();
		}
	}
}
