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
			Open("Example/document.txt");
		}

		private void txtDocument_KeyUp(object sender, KeyEventArgs e)
		{
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

			lbNavigation.Items.Clear();
			lbFigures.Items.Clear();
			lbTables.Items.Clear();
			lbReferences.Items.Clear();

			foreach (KeyValuePair<string, DocumentLib.Chapter> pair in parser.GetChapters())
			{
				// BackColorText(pair.Value.position, pair.Value.match.Length, Color.LightGreen);
				lbNavigation.Items.Add(pair.Value);
			}

			foreach (KeyValuePair<string, DocumentLib.Figure> pair in parser.GetFigures())
			{
				// BackColorText(pair.Value.position, pair.Value.match.Length, Color.Yellow);
				lbFigures.Items.Add(pair.Value);
			}

			foreach (KeyValuePair<string, DocumentLib.Table> pair in parser.GetTables())
			{
				// BackColorText(pair.Value.position, pair.Value.match.Length, Color.Yellow);
				lbTables.Items.Add(pair.Value);
			}

			foreach (DocumentLib.Reference r in parser.GetReferences())
			{
				lbReferences.Items.Add(r);
			}

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

		private void Open(string path)
		{
			if (!File.Exists(path))
				return;

			filePath = path;

			txtDocument.Text = "";
			txtDocument.Text = File.ReadAllText(path);
			tsslblTip.Text = "Opened document '" + filePath + "'";

			UpdateInterface();
		}

		private void Save()
		{
			UpdateInterface();

			File.WriteAllText(filePath, txtDocument.Text.Replace("\n", Environment.NewLine));
			tsslblTip.Text = "Saved document '" + filePath + "'";
		}

		private void Export()
		{
			Save();
			UpdateInterface();

			DocumentLib.Parser fullParser = new DocumentLib.Parser();
			fullParser.SetDocument(txtDocument.Text, Path.GetDirectoryName(filePath));
			fullParser.Parse(true);

			string exportName = Path.Combine(Path.GetDirectoryName(filePath), "output.html");
			string html = DocumentLib.HtmlGenerator.GetHtml(fullParser);

			File.WriteAllText(exportName, html);

			tsslblTip.Text = "Exported document to '" + exportName + "'";
		}

		private void tsbOpen_Click(object sender, EventArgs e)
		{
			if (ofdOpen.ShowDialog() != DialogResult.OK)
				return;

			Open(ofdOpen.FileName);
		}

		private void tsbSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void tsbExport_Click(object sender, EventArgs e)
		{
			Export();
		}

		private void txtDocument_KeyDown(object sender, KeyEventArgs e)
		{
			bool handled = false;

			if (e.KeyCode == Keys.O && e.Control)
			{
				if (ofdOpen.ShowDialog() != DialogResult.OK)
					return;

				Open(ofdOpen.FileName);

				handled = true;
			}

			if (e.KeyCode == Keys.S && e.Control)
			{
				Save();
				handled = true;
			}

			if (e.KeyCode == Keys.E && e.Control)
			{
				Export();
				handled = true;
			}

			if (e.KeyCode == Keys.C && e.Control)
			{
				if (txtDocument.SelectedText != "")
				{
					Clipboard.Clear();
					Clipboard.SetText(txtDocument.SelectedText);
				}

				handled = true;
			}

			if (e.KeyCode == Keys.V && e.Control)
			{
				txtDocument.SelectedText = Clipboard.GetText();
				handled = true;
			}

			if (handled)
				e.SuppressKeyPress = true;
		}
	}
}
