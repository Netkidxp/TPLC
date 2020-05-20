namespace Tplc.UI
{
    partial class TplcBoundaryEditForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv_patch = new System.Windows.Forms.TreeView();
            this.pg_patch = new System.Windows.Forms.PropertyGrid();
            this.cms_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_toInlet = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_toOutlet = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_toWall = new System.Windows.Forms.ToolStripMenuItem();
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
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv_patch);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pg_patch);
            this.splitContainer1.Size = new System.Drawing.Size(469, 416);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.TabIndex = 0;
            // 
            // tv_patch
            // 
            this.tv_patch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_patch.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tv_patch.Location = new System.Drawing.Point(0, 0);
            this.tv_patch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tv_patch.Name = "tv_patch";
            this.tv_patch.Size = new System.Drawing.Size(156, 416);
            this.tv_patch.TabIndex = 0;
            this.tv_patch.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeMouseClick);
            // 
            // pg_patch
            // 
            this.pg_patch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg_patch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pg_patch.Location = new System.Drawing.Point(0, 0);
            this.pg_patch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pg_patch.Name = "pg_patch";
            this.pg_patch.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pg_patch.Size = new System.Drawing.Size(309, 416);
            this.pg_patch.TabIndex = 0;
            // 
            // cms_main
            // 
            this.cms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_toInlet,
            this.mi_toOutlet,
            this.mi_toWall});
            this.cms_main.Name = "cms_main";
            this.cms_main.Size = new System.Drawing.Size(154, 70);
            // 
            // mi_toInlet
            // 
            this.mi_toInlet.Name = "mi_toInlet";
            this.mi_toInlet.Size = new System.Drawing.Size(153, 22);
            this.mi_toInlet.Text = "Assign Inlet";
            this.mi_toInlet.Click += new System.EventHandler(this.mi_toInlet_Click);
            // 
            // mi_toOutlet
            // 
            this.mi_toOutlet.Name = "mi_toOutlet";
            this.mi_toOutlet.Size = new System.Drawing.Size(153, 22);
            this.mi_toOutlet.Text = "Assign Outlet";
            this.mi_toOutlet.Click += new System.EventHandler(this.mi_toOutlet_Click);
            // 
            // mi_toWall
            // 
            this.mi_toWall.Name = "mi_toWall";
            this.mi_toWall.Size = new System.Drawing.Size(153, 22);
            this.mi_toWall.Text = "Assign Wall";
            this.mi_toWall.Click += new System.EventHandler(this.mi_toWall_Click);
            // 
            // TplcBoundaryEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 416);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TplcBoundaryEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Boundary conditon";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cms_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv_patch;
        private System.Windows.Forms.PropertyGrid pg_patch;
        private System.Windows.Forms.ContextMenuStrip cms_main;
        private System.Windows.Forms.ToolStripMenuItem mi_toInlet;
        private System.Windows.Forms.ToolStripMenuItem mi_toOutlet;
        private System.Windows.Forms.ToolStripMenuItem mi_toWall;
    }
}