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
            if (e.KeyCode != Keys.Enter)
                return;

            parser.SetText(txtDocument.Text);
            parser.Parse();


        }
    }
}
