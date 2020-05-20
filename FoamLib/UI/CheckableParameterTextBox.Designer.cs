namespace FoamLib.UI
{
    partial class CheckableParameterTextBox
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_name = new System.Windows.Forms.CheckBox();
            this.tb_value = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cb_name);
            this.flowLayoutPanel1.Controls.Add(this.tb_value);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(164, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cb_name
            // 
            this.cb_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_name.AutoSize = true;
            this.cb_name.Location = new System.Drawing.Point(3, 5);
            this.cb_name.Name = "cb_name";
            this.cb_name.Size = new System.Drawing.Size(48, 16);
            this.cb_name.TabIndex = 0;
            this.cb_name.Text = "name";
            this.cb_name.UseVisualStyleBackColor = true;
            // 
            // tb_value
            // 
            this.tb_value.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tb_value.Enabled = false;
            this.tb_value.Location = new System.Drawing.Point(57, 3);
            this.tb_value.Name = "tb_value";
            this.tb_value.Size = new System.Drawing.Size(100, 21);
            this.tb_value.TabIndex = 1;
            this.tb_value.TextChanged += new System.EventHandler(this.OnValueChanged);
            // 
            // CheckableParameterTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CheckableParameterTextBox";
            this.Size = new System.Drawing.Size(164, 28);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox cb_name;
        private System.Windows.Forms.TextBox tb_value;
    }
}
