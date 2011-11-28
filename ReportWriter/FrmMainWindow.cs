using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Report_Writer
{
	public partial class FrmMainWindow : Form
	{
		DocumentLib.Parser parser = new DocumentLib.Parser();

		public FrmMainWindow()
		{
			InitializeComponent();
		}

		private void FrmMainWindow_Load(object sender, EventArgs e)
		{
			txtDocument.Text = File.ReadAllText("Docs/Specification.txt");
			UpdateInterface();
		}

		private void txtDocument_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter)
				return;

			UpdateInterface();
		}

		private void txtDocument_TextChanged(object sender, EventArgs e)
		{
			changed = true;
		}

		private void lbNavigation_SelectedIndexChanged(object sender, EventArgs e)
		{
			DocumentLib.Heading heading = (DocumentLib.Heading)lbNavigation.SelectedItem;

			if (heading == null)
				return;

			Navigate(heading.position);
		}

		private void UpdateInterface()
		{
			if (changed == false)
				return;

			parser.SetDocument(txtDocument.Text);
			parser.Parse();
			txtLog.Text = parser.GetLog();

			lbNavigation.Items.Clear();
			lbFigures.Items.Clear();

			foreach (KeyValuePair<string, DocumentLib.Heading> pair in parser.GetHeadings())
			{
				lbNavigation.Items.Add(pair.Value);
			}

			foreach (KeyValuePair<string, DocumentLib.Figure> pair in parser.GetFigures())
			{
				lbFigures.Items.Add(pair.Value);
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
	}
}
