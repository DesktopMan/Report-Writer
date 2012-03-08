namespace Report_Writer
{
	partial class FrmMainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainWindow));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer6 = new System.Windows.Forms.SplitContainer();
			this.lbNavigation = new System.Windows.Forms.ListBox();
			this.lbTodos = new System.Windows.Forms.ListBox();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.txtDocument = new System.Windows.Forms.RichTextBox();
			this.lbLog = new System.Windows.Forms.ListBox();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.lbFigures = new System.Windows.Forms.ListBox();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.lbTables = new System.Windows.Forms.ListBox();
			this.lbReferences = new System.Windows.Forms.ListBox();
			this.ssTip = new System.Windows.Forms.StatusStrip();
			this.tsslblTip = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpen = new System.Windows.Forms.ToolStripButton();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tsbExport = new System.Windows.Forms.ToolStripButton();
			this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
			this.fswDocument = new System.IO.FileSystemWatcher();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer6.Panel1.SuspendLayout();
			this.splitContainer6.Panel2.SuspendLayout();
			this.splitContainer6.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			this.ssTip.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fswDocument)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer6);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(792, 510);
			this.splitContainer1.SplitterDistance = 205;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer6
			// 
			this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer6.Location = new System.Drawing.Point(0, 0);
			this.splitContainer6.Name = "splitContainer6";
			this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer6.Panel1
			// 
			this.splitContainer6.Panel1.Controls.Add(this.lbNavigation);
			// 
			// splitContainer6.Panel2
			// 
			this.splitContainer6.Panel2.Controls.Add(this.lbTodos);
			this.splitContainer6.Size = new System.Drawing.Size(205, 510);
			this.splitContainer6.SplitterDistance = 412;
			this.splitContainer6.TabIndex = 0;
			// 
			// lbNavigation
			// 
			this.lbNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbNavigation.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbNavigation.FormattingEnabled = true;
			this.lbNavigation.ItemHeight = 15;
			this.lbNavigation.Location = new System.Drawing.Point(0, 0);
			this.lbNavigation.Name = "lbNavigation";
			this.lbNavigation.Size = new System.Drawing.Size(205, 412);
			this.lbNavigation.TabIndex = 2;
			this.lbNavigation.SelectedIndexChanged += new System.EventHandler(this.lbNavigation_SelectedIndexChanged);
			// 
			// lbTodos
			// 
			this.lbTodos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTodos.FormattingEnabled = true;
			this.lbTodos.Location = new System.Drawing.Point(0, 0);
			this.lbTodos.Name = "lbTodos";
			this.lbTodos.Size = new System.Drawing.Size(205, 94);
			this.lbTodos.TabIndex = 0;
			this.lbTodos.SelectedIndexChanged += new System.EventHandler(this.lbTodos_SelectedIndexChanged);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
			this.splitContainer2.Size = new System.Drawing.Size(583, 510);
			this.splitContainer2.SplitterDistance = 386;
			this.splitContainer2.TabIndex = 0;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.txtDocument);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.lbLog);
			this.splitContainer3.Size = new System.Drawing.Size(386, 510);
			this.splitContainer3.SplitterDistance = 413;
			this.splitContainer3.TabIndex = 0;
			// 
			// txtDocument
			// 
			this.txtDocument.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDocument.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDocument.Location = new System.Drawing.Point(0, 0);
			this.txtDocument.Name = "txtDocument";
			this.txtDocument.Size = new System.Drawing.Size(386, 413);
			this.txtDocument.TabIndex = 0;
			this.txtDocument.Text = "";
			this.txtDocument.TextChanged += new System.EventHandler(this.txtDocument_TextChanged);
			this.txtDocument.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocument_KeyDown);
			this.txtDocument.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDocument_KeyUp);
			this.txtDocument.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtDocument_MouseUp);
			// 
			// lbLog
			// 
			this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbLog.FormattingEnabled = true;
			this.lbLog.ItemHeight = 15;
			this.lbLog.Location = new System.Drawing.Point(0, 0);
			this.lbLog.Name = "lbLog";
			this.lbLog.Size = new System.Drawing.Size(386, 93);
			this.lbLog.TabIndex = 0;
			this.lbLog.SelectedIndexChanged += new System.EventHandler(this.lbLog_SelectedIndexChanged);
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.lbFigures);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
			this.splitContainer4.Size = new System.Drawing.Size(193, 510);
			this.splitContainer4.SplitterDistance = 158;
			this.splitContainer4.TabIndex = 0;
			// 
			// lbFigures
			// 
			this.lbFigures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbFigures.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbFigures.FormattingEnabled = true;
			this.lbFigures.ItemHeight = 15;
			this.lbFigures.Location = new System.Drawing.Point(0, 0);
			this.lbFigures.Name = "lbFigures";
			this.lbFigures.Size = new System.Drawing.Size(193, 158);
			this.lbFigures.TabIndex = 0;
			this.lbFigures.SelectedIndexChanged += new System.EventHandler(this.lbFigures_SelectedIndexChanged);
			// 
			// splitContainer5
			// 
			this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer5.Location = new System.Drawing.Point(0, 0);
			this.splitContainer5.Name = "splitContainer5";
			this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.Controls.Add(this.lbTables);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.lbReferences);
			this.splitContainer5.Size = new System.Drawing.Size(193, 348);
			this.splitContainer5.SplitterDistance = 163;
			this.splitContainer5.TabIndex = 0;
			// 
			// lbTables
			// 
			this.lbTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTables.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbTables.FormattingEnabled = true;
			this.lbTables.ItemHeight = 15;
			this.lbTables.Items.AddRange(new object[] {
            "Tables will be here"});
			this.lbTables.Location = new System.Drawing.Point(0, 0);
			this.lbTables.Name = "lbTables";
			this.lbTables.Size = new System.Drawing.Size(193, 163);
			this.lbTables.TabIndex = 1;
			this.lbTables.SelectedIndexChanged += new System.EventHandler(this.lbTables_SelectedIndexChanged);
			// 
			// lbReferences
			// 
			this.lbReferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbReferences.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbReferences.FormattingEnabled = true;
			this.lbReferences.ItemHeight = 15;
			this.lbReferences.Items.AddRange(new object[] {
            "References will be here"});
			this.lbReferences.Location = new System.Drawing.Point(0, 0);
			this.lbReferences.Name = "lbReferences";
			this.lbReferences.Size = new System.Drawing.Size(193, 181);
			this.lbReferences.TabIndex = 1;
			this.lbReferences.SelectedIndexChanged += new System.EventHandler(this.lbReferences_SelectedIndexChanged);
			// 
			// ssTip
			// 
			this.ssTip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblTip});
			this.ssTip.Location = new System.Drawing.Point(0, 535);
			this.ssTip.Name = "ssTip";
			this.ssTip.Size = new System.Drawing.Size(792, 22);
			this.ssTip.TabIndex = 1;
			this.ssTip.Text = "statusStrip1";
			// 
			// tsslblTip
			// 
			this.tsslblTip.Name = "tsslblTip";
			this.tsslblTip.Size = new System.Drawing.Size(118, 17);
			this.tsslblTip.Text = "toolStripStatusLabel1";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbSave,
            this.tsbExport});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(792, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbOpen
			// 
			this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
			this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpen.Name = "tsbOpen";
			this.tsbOpen.Size = new System.Drawing.Size(23, 22);
			this.tsbOpen.Text = "Open";
			this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "Save";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
			// 
			// tsbExport
			// 
			this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
			this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExport.Name = "tsbExport";
			this.tsbExport.Size = new System.Drawing.Size(23, 22);
			this.tsbExport.Text = "Export";
			this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
			// 
			// ofdOpen
			// 
			this.ofdOpen.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// fswDocument
			// 
			this.fswDocument.NotifyFilter = System.IO.NotifyFilters.LastWrite;
			this.fswDocument.SynchronizingObject = this;
			this.fswDocument.Changed += new System.IO.FileSystemEventHandler(this.fswDocument_Changed);
			// 
			// FrmMainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 557);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.ssTip);
			this.Controls.Add(this.toolStrip1);
			this.Name = "FrmMainWindow";
			this.Text = "Report Writer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMainWindow_FormClosing);
			this.Load += new System.EventHandler(this.FrmMainWindow_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer6.Panel1.ResumeLayout(false);
			this.splitContainer6.Panel2.ResumeLayout(false);
			this.splitContainer6.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			this.splitContainer4.ResumeLayout(false);
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel2.ResumeLayout(false);
			this.splitContainer5.ResumeLayout(false);
			this.ssTip.ResumeLayout(false);
			this.ssTip.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.fswDocument)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.StatusStrip ssTip;
		private System.Windows.Forms.ToolStripStatusLabel tsslblTip;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.SplitContainer splitContainer5;
		private System.Windows.Forms.ListBox lbFigures;
		private System.Windows.Forms.ListBox lbTables;
		private System.Windows.Forms.ListBox lbReferences;
		private System.Windows.Forms.ListBox lbLog;
		private System.Windows.Forms.RichTextBox txtDocument;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripButton tsbOpen;
		private System.Windows.Forms.ToolStripButton tsbExport;
		private System.Windows.Forms.OpenFileDialog ofdOpen;
		private System.Windows.Forms.SplitContainer splitContainer6;
		private System.Windows.Forms.ListBox lbNavigation;
		private System.Windows.Forms.ListBox lbTodos;
		private System.IO.FileSystemWatcher fswDocument;
	}
}

