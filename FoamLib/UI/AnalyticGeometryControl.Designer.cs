namespace FoamLib.UI
{
    partial class AnalyticGeometryControl
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
            this.cb_main = new System.Windows.Forms.ComboBox();
            this.pg_main = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // cb_main
            // 
            this.cb_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_main.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_main.FormattingEnabled = true;
            this.cb_main.Location = new System.Drawing.Point(0, 0);
            this.cb_main.Name = "cb_main";
            this.cb_main.Size = new System.Drawing.Size(223, 20);
            this.cb_main.TabIndex = 0;
            this.cb_main.SelectedValueChanged += new System.EventHandler(this.OnSelectedValueChanged);
            // 
            // pg_main
            // 
            this.pg_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg_main.Location = new System.Drawing.Point(0, 20);
            this.pg_main.Name = "pg_main";
            this.pg_main.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pg_main.Size = new System.Drawing.Size(223, 283);
            this.pg_main.TabIndex = 1;
            this.pg_main.ToolbarVisible = false;
            // 
            // AnalyticGeometryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pg_main);
            this.Controls.Add(this.cb_main);
            this.Name = "AnalyticGeometryControl";
            this.Size = new System.Drawing.Size(223, 303);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_main;
        private System.Windows.Forms.PropertyGrid pg_main;
    }
}
