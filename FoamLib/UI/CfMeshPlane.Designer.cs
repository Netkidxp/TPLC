namespace FoamLib.UI
{
    partial class CfMeshPlane
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
            this.bu_generate = new System.Windows.Forms.Button();
            this.cm_geometry = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmi_geometry_openstl = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_value = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pg_main = new System.Windows.Forms.PropertyGrid();
            this.bu_surface = new System.Windows.Forms.Button();
            this.cm_geometry.SuspendLayout();
            this.cm_value.SuspendLayout();
            this.SuspendLayout();
            // 
            // bu_generate
            // 
            this.bu_generate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bu_generate.Location = new System.Drawing.Point(0, 420);
            this.bu_generate.Name = "bu_generate";
            this.bu_generate.Size = new System.Drawing.Size(268, 23);
            this.bu_generate.TabIndex = 1;
            this.bu_generate.Text = "Generate";
            this.bu_generate.UseVisualStyleBackColor = true;
            this.bu_generate.Click += new System.EventHandler(this.bu_generate_Click);
            // 
            // cm_geometry
            // 
            this.cm_geometry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmi_geometry_openstl});
            this.cm_geometry.Name = "cm_geometry";
            this.cm_geometry.Size = new System.Drawing.Size(133, 26);
            // 
            // cmi_geometry_openstl
            // 
            this.cmi_geometry_openstl.Name = "cmi_geometry_openstl";
            this.cmi_geometry_openstl.Size = new System.Drawing.Size(132, 22);
            this.cmi_geometry_openstl.Text = "Open STL";
            // 
            // cm_value
            // 
            this.cm_value.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.cm_value.Name = "cm_generate";
            this.cm_value.Size = new System.Drawing.Size(107, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem1.Text = "Set";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem2.Text = "Clear";
            // 
            // pg_main
            // 
            this.pg_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg_main.LineColor = System.Drawing.SystemColors.Control;
            this.pg_main.Location = new System.Drawing.Point(0, 0);
            this.pg_main.Name = "pg_main";
            this.pg_main.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.pg_main.Size = new System.Drawing.Size(268, 420);
            this.pg_main.TabIndex = 2;
            this.pg_main.ToolbarVisible = false;
            this.pg_main.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnPropertyValueChanged);
            // 
            // bu_surface
            // 
            this.bu_surface.Dock = System.Windows.Forms.DockStyle.Top;
            this.bu_surface.Location = new System.Drawing.Point(0, 0);
            this.bu_surface.Name = "bu_surface";
            this.bu_surface.Size = new System.Drawing.Size(268, 23);
            this.bu_surface.TabIndex = 3;
            this.bu_surface.Text = "button1";
            this.bu_surface.UseVisualStyleBackColor = true;
            // 
            // CfMeshPlane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bu_surface);
            this.Controls.Add(this.pg_main);
            this.Controls.Add(this.bu_generate);
            this.Name = "CfMeshPlane";
            this.Size = new System.Drawing.Size(268, 443);
            this.cm_geometry.ResumeLayout(false);
            this.cm_value.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bu_generate;
        private System.Windows.Forms.ContextMenuStrip cm_geometry;
        private System.Windows.Forms.ToolStripMenuItem cmi_geometry_openstl;
        private System.Windows.Forms.ContextMenuStrip cm_value;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.PropertyGrid pg_main;
        private System.Windows.Forms.Button bu_surface;
    }
}
