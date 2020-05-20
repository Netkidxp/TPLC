namespace TplcPost
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Patchs");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Cuts");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Pressure Drop");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_opencase = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_openstate = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_savestateas = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_export = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsb_stop = new System.Windows.Forms.ToolStripSplitButton();
            this.pb_main = new System.Windows.Forms.ToolStripProgressBar();
            this.tssl_main = new System.Windows.Forms.ToolStripStatusLabel();
            this.ts_main = new System.Windows.Forms.ToolStrip();
            this.tsb_opencase = new System.Windows.Forms.ToolStripButton();
            this.tsb_openstate = new System.Windows.Forms.ToolStripButton();
            this.tsb_savestateas = new System.Windows.Forms.ToolStripButton();
            this.tsb_export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_newcut = new System.Windows.Forms.ToolStripButton();
            this.tsb_removecut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tscb_field = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_resetrange = new System.Windows.Forms.ToolStripButton();
            this.tstb_min = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstb_max = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ptvMain = new FoamLib.UI.Post.Controls.PostTreeView();
            this.ms_cutunit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_showunit = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_hideunit = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_newcut = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_removecut = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_updatecut = new System.Windows.Forms.ToolStripMenuItem();
            this.im_treeicon = new System.Windows.Forms.ImageList(this.components);
            this.pgMain = new System.Windows.Forms.PropertyGrid();
            this.fdvMain = new FoamLib.UI.Post.Controls.FoamDataViewer();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ts_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ms_cutunit.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1031, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_opencase,
            this.mi_openstate,
            this.mi_savestateas,
            this.mi_export,
            this.mi_exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mi_opencase
            // 
            this.mi_opencase.Image = global::TPost.Properties.Resources.OpenHyperlink_16x16;
            this.mi_opencase.Name = "mi_opencase";
            this.mi_opencase.Size = new System.Drawing.Size(152, 22);
            this.mi_opencase.Text = "&Open case...";
            this.mi_opencase.Click += new System.EventHandler(this.mi_opencase_Click);
            // 
            // mi_openstate
            // 
            this.mi_openstate.Image = global::TPost.Properties.Resources.Open2_16x16;
            this.mi_openstate.Name = "mi_openstate";
            this.mi_openstate.Size = new System.Drawing.Size(152, 22);
            this.mi_openstate.Text = "Open &state...";
            this.mi_openstate.Click += new System.EventHandler(this.mi_openstate_Click);
            // 
            // mi_savestateas
            // 
            this.mi_savestateas.Image = global::TPost.Properties.Resources.SaveAndNew_16x16;
            this.mi_savestateas.Name = "mi_savestateas";
            this.mi_savestateas.Size = new System.Drawing.Size(152, 22);
            this.mi_savestateas.Text = "Save state &as";
            this.mi_savestateas.Click += new System.EventHandler(this.mi_savestateas_Click);
            // 
            // mi_export
            // 
            this.mi_export.Image = global::TPost.Properties.Resources.ExportToIMG_16x16;
            this.mi_export.Name = "mi_export";
            this.mi_export.Size = new System.Drawing.Size(152, 22);
            this.mi_export.Text = "&Export...";
            this.mi_export.Click += new System.EventHandler(this.mi_export_Click);
            // 
            // mi_exit
            // 
            this.mi_exit.Image = global::TPost.Properties.Resources.ReviewingPane_16x16;
            this.mi_exit.Name = "mi_exit";
            this.mi_exit.Size = new System.Drawing.Size(152, 22);
            this.mi_exit.Text = "E&xit";
            this.mi_exit.Click += new System.EventHandler(this.mi_exit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_stop,
            this.pb_main,
            this.tssl_main});
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1031, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsb_stop
            // 
            this.tsb_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_stop.Image = global::TPost.Properties.Resources.Stop_32x32;
            this.tsb_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_stop.Name = "tsb_stop";
            this.tsb_stop.Size = new System.Drawing.Size(32, 20);
            this.tsb_stop.Text = "toolStripSplitButton1";
            this.tsb_stop.Visible = false;
            // 
            // pb_main
            // 
            this.pb_main.Name = "pb_main";
            this.pb_main.Size = new System.Drawing.Size(300, 16);
            // 
            // tssl_main
            // 
            this.tssl_main.Name = "tssl_main";
            this.tssl_main.Size = new System.Drawing.Size(44, 17);
            this.tssl_main.Text = "Ready";
            // 
            // ts_main
            // 
            this.ts_main.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.ts_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_opencase,
            this.tsb_openstate,
            this.tsb_savestateas,
            this.tsb_export,
            this.toolStripSeparator1,
            this.tsb_newcut,
            this.tsb_removecut,
            this.toolStripSeparator3,
            this.tscb_field,
            this.toolStripSeparator2,
            this.tsb_resetrange,
            this.tstb_min,
            this.toolStripLabel1,
            this.tstb_max});
            this.ts_main.Location = new System.Drawing.Point(0, 25);
            this.ts_main.Name = "ts_main";
            this.ts_main.Size = new System.Drawing.Size(1031, 32);
            this.ts_main.TabIndex = 2;
            this.ts_main.Text = "Min value";
            this.ts_main.TextChanged += new System.EventHandler(this.OnRangeChanged);
            // 
            // tsb_opencase
            // 
            this.tsb_opencase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_opencase.Image = global::TPost.Properties.Resources.OpenHyperlink_32x32;
            this.tsb_opencase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_opencase.Name = "tsb_opencase";
            this.tsb_opencase.Size = new System.Drawing.Size(29, 29);
            this.tsb_opencase.Text = "Open case";
            this.tsb_opencase.Click += new System.EventHandler(this.mi_opencase_Click);
            // 
            // tsb_openstate
            // 
            this.tsb_openstate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_openstate.Image = global::TPost.Properties.Resources.Open2_32x32;
            this.tsb_openstate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_openstate.Name = "tsb_openstate";
            this.tsb_openstate.Size = new System.Drawing.Size(29, 29);
            this.tsb_openstate.Text = "Open state";
            this.tsb_openstate.Click += new System.EventHandler(this.mi_openstate_Click);
            // 
            // tsb_savestateas
            // 
            this.tsb_savestateas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_savestateas.Image = global::TPost.Properties.Resources.SaveAndNew_32x32;
            this.tsb_savestateas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_savestateas.Name = "tsb_savestateas";
            this.tsb_savestateas.Size = new System.Drawing.Size(29, 29);
            this.tsb_savestateas.Text = "Save state as";
            this.tsb_savestateas.Click += new System.EventHandler(this.mi_savestateas_Click);
            // 
            // tsb_export
            // 
            this.tsb_export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_export.Image = global::TPost.Properties.Resources.ExportToIMG_32x32;
            this.tsb_export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_export.Name = "tsb_export";
            this.tsb_export.Size = new System.Drawing.Size(29, 29);
            this.tsb_export.Text = "Export";
            this.tsb_export.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tsb_export.Click += new System.EventHandler(this.mi_export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsb_newcut
            // 
            this.tsb_newcut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_newcut.Enabled = false;
            this.tsb_newcut.Image = global::TPost.Properties.Resources._3DLine_32x32;
            this.tsb_newcut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_newcut.Name = "tsb_newcut";
            this.tsb_newcut.Size = new System.Drawing.Size(29, 29);
            this.tsb_newcut.Text = "New cut";
            this.tsb_newcut.Click += new System.EventHandler(this.mi_newcut_Click);
            // 
            // tsb_removecut
            // 
            this.tsb_removecut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_removecut.Enabled = false;
            this.tsb_removecut.Image = global::TPost.Properties.Resources.RemovePivotField_32x32;
            this.tsb_removecut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_removecut.Name = "tsb_removecut";
            this.tsb_removecut.Size = new System.Drawing.Size(29, 29);
            this.tsb_removecut.Text = "Remove cut";
            this.tsb_removecut.Click += new System.EventHandler(this.mi_removecut_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tscb_field
            // 
            this.tscb_field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_field.Name = "tscb_field";
            this.tscb_field.Size = new System.Drawing.Size(121, 32);
            this.tscb_field.SelectedIndexChanged += new System.EventHandler(this.OnFieldNameSelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsb_resetrange
            // 
            this.tsb_resetrange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_resetrange.Image = global::TPost.Properties.Resources.ReversSort_32x32;
            this.tsb_resetrange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_resetrange.Name = "tsb_resetrange";
            this.tsb_resetrange.Size = new System.Drawing.Size(29, 29);
            this.tsb_resetrange.Text = "Reset range";
            this.tsb_resetrange.Click += new System.EventHandler(this.tsb_resetrange_Click);
            // 
            // tstb_min
            // 
            this.tstb_min.Name = "tstb_min";
            this.tstb_min.Size = new System.Drawing.Size(100, 32);
            this.tstb_min.Text = "0.0";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 29);
            this.toolStripLabel1.Text = "to";
            // 
            // tstb_max
            // 
            this.tstb_max.Name = "tstb_max";
            this.tstb_max.Size = new System.Drawing.Size(100, 32);
            this.tstb_max.Text = "0.0";
            this.tstb_max.TextChanged += new System.EventHandler(this.OnRangeChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 57);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fdvMain);
            this.splitContainer1.Size = new System.Drawing.Size(1031, 588);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ptvMain);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pgMain);
            this.splitContainer2.Size = new System.Drawing.Size(236, 588);
            this.splitContainer2.SplitterDistance = 396;
            this.splitContainer2.TabIndex = 0;
            // 
            // ptvMain
            // 
            this.ptvMain.ContextMenuStrip = this.ms_cutunit;
            this.ptvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptvMain.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ptvMain.ImageIndex = 0;
            this.ptvMain.ImageList = this.im_treeicon;
            this.ptvMain.IndexCutList = 4;
            this.ptvMain.IndexCutter = 0;
            this.ptvMain.IndexPatch = 0;
            this.ptvMain.IndexPatchList = 3;
            this.ptvMain.Location = new System.Drawing.Point(0, 0);
            this.ptvMain.Name = "ptvMain";
            treeNode1.ImageIndex = 3;
            treeNode1.Name = "";
            treeNode1.Text = "Patchs";
            treeNode2.ImageIndex = 4;
            treeNode2.Name = "";
            treeNode2.Text = "Cuts";
            treeNode3.Name = "";
            treeNode3.Text = "Pressure Drop";
            this.ptvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.ptvMain.PostDesc = null;
            this.ptvMain.SelectedImageIndex = 0;
            this.ptvMain.Size = new System.Drawing.Size(234, 394);
            this.ptvMain.TabIndex = 0;
            this.ptvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnPostTreeViewItemSelected);
            this.ptvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTreeViewNodeMouseClick);
            // 
            // ms_cutunit
            // 
            this.ms_cutunit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_showunit,
            this.mi_hideunit,
            this.mi_newcut,
            this.mi_removecut,
            this.mi_updatecut});
            this.ms_cutunit.Name = "ms_cutunit";
            this.ms_cutunit.Size = new System.Drawing.Size(145, 114);
            // 
            // mi_showunit
            // 
            this.mi_showunit.Name = "mi_showunit";
            this.mi_showunit.Size = new System.Drawing.Size(144, 22);
            this.mi_showunit.Text = "&Show";
            this.mi_showunit.Click += new System.EventHandler(this.mi_showunit_Click);
            // 
            // mi_hideunit
            // 
            this.mi_hideunit.Name = "mi_hideunit";
            this.mi_hideunit.Size = new System.Drawing.Size(144, 22);
            this.mi_hideunit.Text = "&Hide";
            this.mi_hideunit.Click += new System.EventHandler(this.mi_hideunit_Click);
            // 
            // mi_newcut
            // 
            this.mi_newcut.Name = "mi_newcut";
            this.mi_newcut.Size = new System.Drawing.Size(144, 22);
            this.mi_newcut.Text = "&New cut";
            this.mi_newcut.Click += new System.EventHandler(this.mi_newcut_Click);
            // 
            // mi_removecut
            // 
            this.mi_removecut.Name = "mi_removecut";
            this.mi_removecut.Size = new System.Drawing.Size(144, 22);
            this.mi_removecut.Text = "&Remove cut";
            this.mi_removecut.Click += new System.EventHandler(this.mi_removecut_Click);
            // 
            // mi_updatecut
            // 
            this.mi_updatecut.Name = "mi_updatecut";
            this.mi_updatecut.Size = new System.Drawing.Size(144, 22);
            this.mi_updatecut.Text = "&Update cut";
            this.mi_updatecut.Click += new System.EventHandler(this.mi_updatecut_Click);
            // 
            // im_treeicon
            // 
            this.im_treeicon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("im_treeicon.ImageStream")));
            this.im_treeicon.TransparentColor = System.Drawing.Color.Transparent;
            this.im_treeicon.Images.SetKeyName(0, "Forward_16x16.png");
            this.im_treeicon.Images.SetKeyName(1, "3DLine_16x16.png");
            this.im_treeicon.Images.SetKeyName(2, "StepArea_16x16.png");
            this.im_treeicon.Images.SetKeyName(3, "LoadTheme_16x16.png");
            this.im_treeicon.Images.SetKeyName(4, "OpenHyperlink_16x16.png");
            // 
            // pgMain
            // 
            this.pgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMain.Location = new System.Drawing.Point(0, 0);
            this.pgMain.Name = "pgMain";
            this.pgMain.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgMain.Size = new System.Drawing.Size(234, 186);
            this.pgMain.TabIndex = 0;
            this.pgMain.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnPropertyValueChanged);
            // 
            // fdvMain
            // 
            this.fdvMain.AddTestActors = false;
            this.fdvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fdvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fdvMain.Location = new System.Drawing.Point(0, 0);
            this.fdvMain.Name = "fdvMain";
            this.fdvMain.Size = new System.Drawing.Size(789, 586);
            this.fdvMain.TabIndex = 0;
            this.fdvMain.TestText = null;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 667);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ts_main);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "TPLC-Post";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ts_main.ResumeLayout(false);
            this.ts_main.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ms_cutunit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mi_opencase;
        private System.Windows.Forms.ToolStripMenuItem mi_openstate;
        private System.Windows.Forms.ToolStripMenuItem mi_savestateas;
        private System.Windows.Forms.ToolStripMenuItem mi_exit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip ts_main;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar pb_main;
        private System.Windows.Forms.ToolStripStatusLabel tssl_main;
        private System.Windows.Forms.ToolStripButton tsb_openstate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid pgMain;
        private FoamLib.UI.Post.Controls.FoamDataViewer fdvMain;
        private FoamLib.UI.Post.Controls.PostTreeView ptvMain;
        private System.Windows.Forms.ToolStripSplitButton tsb_stop;
        private System.Windows.Forms.ImageList im_treeicon;
        private System.Windows.Forms.ToolStripComboBox tscb_field;
        private System.Windows.Forms.ToolStripMenuItem mi_export;
        private System.Windows.Forms.ToolStripButton tsb_opencase;
        private System.Windows.Forms.ToolStripButton tsb_savestateas;
        private System.Windows.Forms.ToolStripButton tsb_export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_resetrange;
        private System.Windows.Forms.ToolStripTextBox tstb_min;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstb_max;
        private System.Windows.Forms.ContextMenuStrip ms_cutunit;
        private System.Windows.Forms.ToolStripMenuItem mi_newcut;
        private System.Windows.Forms.ToolStripMenuItem mi_removecut;
        private System.Windows.Forms.ToolStripButton tsb_newcut;
        private System.Windows.Forms.ToolStripButton tsb_removecut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mi_showunit;
        private System.Windows.Forms.ToolStripMenuItem mi_hideunit;
        private System.Windows.Forms.ToolStripMenuItem mi_updatecut;
    }
}

