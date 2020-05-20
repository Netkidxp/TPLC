namespace FoamLib.UI
{
    partial class RefineObjectListEditControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.cms_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_newbox = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_newcone = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_newsphere = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.pgMain = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cms_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pgMain);
            this.splitContainer1.Size = new System.Drawing.Size(182, 305);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvMain
            // 
            this.tvMain.ContextMenuStrip = this.cms_main;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Name = "tvMain";
            this.tvMain.Size = new System.Drawing.Size(182, 172);
            this.tvMain.TabIndex = 0;
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnItemSelected);
            // 
            // cms_main
            // 
            this.cms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.mi_delete});
            this.cms_main.Name = "cms_main";
            this.cms_main.Size = new System.Drawing.Size(153, 70);
            this.cms_main.Opening += new System.ComponentModel.CancelEventHandler(this.OnCmsMainOpening);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_newbox,
            this.mi_newcone,
            this.mi_newsphere});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // mi_newbox
            // 
            this.mi_newbox.Name = "mi_newbox";
            this.mi_newbox.Size = new System.Drawing.Size(117, 22);
            this.mi_newbox.Text = "Box";
            this.mi_newbox.Click += new System.EventHandler(this.mi_newbox_Click);
            // 
            // mi_newcone
            // 
            this.mi_newcone.Name = "mi_newcone";
            this.mi_newcone.Size = new System.Drawing.Size(152, 22);
            this.mi_newcone.Text = "Cone";
            this.mi_newcone.Visible = false;
            this.mi_newcone.Click += new System.EventHandler(this.mi_newcone_Click);
            // 
            // mi_newsphere
            // 
            this.mi_newsphere.Name = "mi_newsphere";
            this.mi_newsphere.Size = new System.Drawing.Size(117, 22);
            this.mi_newsphere.Text = "Sphere";
            this.mi_newsphere.Click += new System.EventHandler(this.mi_newsphere_Click);
            // 
            // mi_delete
            // 
            this.mi_delete.Name = "mi_delete";
            this.mi_delete.Size = new System.Drawing.Size(113, 22);
            this.mi_delete.Text = "Delete";
            this.mi_delete.Click += new System.EventHandler(this.mi_delete_Click);
            // 
            // pgMain
            // 
            this.pgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMain.HelpVisible = false;
            this.pgMain.Location = new System.Drawing.Point(0, 0);
            this.pgMain.Name = "pgMain";
            this.pgMain.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgMain.Size = new System.Drawing.Size(182, 129);
            this.pgMain.TabIndex = 0;
            this.pgMain.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnPropretyChanged);
            // 
            // RefineObjectListEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "RefineObjectListEditControl";
            this.Size = new System.Drawing.Size(182, 305);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cms_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.PropertyGrid pgMain;
        private System.Windows.Forms.ContextMenuStrip cms_main;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mi_newbox;
        private System.Windows.Forms.ToolStripMenuItem mi_newcone;
        private System.Windows.Forms.ToolStripMenuItem mi_newsphere;
        private System.Windows.Forms.ToolStripMenuItem mi_delete;
    }
}
