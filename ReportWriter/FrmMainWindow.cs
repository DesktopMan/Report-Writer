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
        }

        private void txtDocument_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || changed == false)
                return;

            parser.SetDocument(txtDocument.Text);
            parser.Parse();

            lbNavigation.Items.Clear();

            foreach (KeyValuePair<string, DocumentLib.Heading> pair in parser.GetHeadings())
            {
                lbNavigation.Items.Add(pair.Value.text);
            }
        }

        private void txtDocument_TextChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private bool changed = false;
    }
}
