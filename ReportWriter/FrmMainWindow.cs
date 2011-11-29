﻿using System;
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

			lbLog.Items.Clear();

			foreach (DocumentLib.LogLine l in parser.GetLog())
				lbLog.Items.Add(l);

			if (lbLog.Items.Count == 0)
				tsslblTip.Text = "Document parsed successfully";
			else
				tsslblTip.Text = "Some issues were found while parsing this document";

			lbNavigation.Items.Clear();
			lbFigures.Items.Clear();
			lbReferences.Items.Clear();

			foreach (KeyValuePair<string, DocumentLib.Heading> pair in parser.GetHeadings())
				lbNavigation.Items.Add(pair.Value);

			foreach (KeyValuePair<string, DocumentLib.Figure> pair in parser.GetFigures())
				lbFigures.Items.Add(pair.Value);

			foreach (KeyValuePair<string, DocumentLib.Reference> pair in parser.GetReferences())
				lbReferences.Items.Add(pair.Value);

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

		private void lbReferences_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbReferences.SelectedItem != null)
				Navigate(((DocumentLib.Reference)lbReferences.SelectedItem).position);

			lbReferences.ClearSelected();
		}

		private void lbTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			lbTables.ClearSelected();
		}
	}
}
