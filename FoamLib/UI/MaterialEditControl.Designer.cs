namespace FoamLib.UI
{
    partial class MaterialEditControl
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
            this.tp_const = new System.Windows.Forms.TabPage();
            this.bu_const_copy = new System.Windows.Forms.Button();
            this.bu_const_save = new System.Windows.Forms.Button();
            this.tb_const_viscosity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_const_density = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bu_delete = new System.Windows.Forms.Button();
            this.bu_add = new System.Windows.Forms.Button();
            this.cb_const_name = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tp_const.SuspendLayout();
            this.tc_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tp_const
            // 
            this.tp_const.Controls.Add(this.bu_const_copy);
            this.tp_const.Controls.Add(this.bu_const_save);
            this.tp_const.Controls.Add(this.tb_const_viscosity);
            this.tp_const.Controls.Add(this.label4);
            this.tp_const.Controls.Add(this.tb_const_density);
            this.tp_const.Controls.Add(this.label3);
            this.tp_const.Controls.Add(this.bu_delete);
            this.tp_const.Controls.Add(this.bu_add);
            this.tp_const.Controls.Add(this.cb_const_name);
            this.tp_const.Controls.Add(this.label2);
            this.tp_const.Location = new System.Drawing.Point(4, 22);
            this.tp_const.Name = "tp_const";
            this.tp_const.Padding = new System.Windows.Forms.Padding(3);
            this.tp_const.Size = new System.Drawing.Size(193, 181);
            this.tp_const.TabIndex = 1;
            this.tp_const.Text = "Const";
            this.tp_const.UseVisualStyleBackColor = true;
            // 
            // bu_const_copy
            // 
            this.bu_const_copy.Image = global::FoamLib.Properties.Resources.Copy_16x16;
            this.bu_const_copy.Location = new System.Drawing.Point(165, 53);
            this.bu_const_copy.Name = "bu_const_copy";
            this.bu_const_copy.Size = new System.Drawing.Size(25, 25);
            this.bu_const_copy.TabIndex = 10;
            this.bu_const_copy.UseVisualStyleBackColor = true;
            this.bu_const_copy.Click += new System.EventHandler(this.bu_const_copy_Click);
            // 
            // bu_const_save
            // 
            this.bu_const_save.Image = global::FoamLib.Properties.Resources.Save_16x16;
            this.bu_const_save.Location = new System.Drawing.Point(165, 78);
            this.bu_const_save.Name = "bu_const_save";
            this.bu_const_save.Size = new System.Drawing.Size(25, 25);
            this.bu_const_save.TabIndex = 9;
            this.bu_const_save.UseVisualStyleBackColor = true;
            this.bu_const_save.Click += new System.EventHandler(this.bu_const_save_Click);
            // 
            // tb_const_viscosity
            // 
            this.tb_const_viscosity.Location = new System.Drawing.Point(71, 80);
            this.tb_const_viscosity.Name = "tb_const_viscosity";
            this.tb_const_viscosity.Size = new System.Drawing.Size(88, 21);
            this.tb_const_viscosity.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Viscosity";
            // 
            // tb_const_density
            // 
            this.tb_const_density.Location = new System.Drawing.Point(72, 42);
            this.tb_const_density.Name = "tb_const_density";
            this.tb_const_density.Size = new System.Drawing.Size(87, 21);
            this.tb_const_density.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Density";
            // 
            // bu_delete
            // 
            this.bu_delete.Image = global::FoamLib.Properties.Resources.Delete_16x16;
            this.bu_delete.Location = new System.Drawing.Point(165, 28);
            this.bu_delete.Name = "bu_delete";
            this.bu_delete.Size = new System.Drawing.Size(25, 25);
            this.bu_delete.TabIndex = 4;
            this.bu_delete.UseVisualStyleBackColor = true;
            this.bu_delete.Click += new System.EventHandler(this.bu_delete_Click);
            // 
            // bu_add
            // 
            this.bu_add.Image = global::FoamLib.Properties.Resources.Add_16x16;
            this.bu_add.Location = new System.Drawing.Point(165, 3);
            this.bu_add.Name = "bu_add";
            this.bu_add.Size = new System.Drawing.Size(25, 25);
            this.bu_add.TabIndex = 3;
            this.bu_add.UseVisualStyleBackColor = true;
            this.bu_add.Click += new System.EventHandler(this.bu_add_Click);
            // 
            // cb_const_name
            // 
            this.cb_const_name.FormattingEnabled = true;
            this.cb_const_name.Location = new System.Drawing.Point(72, 4);
            this.cb_const_name.Name = "cb_const_name";
            this.cb_const_name.Size = new System.Drawing.Size(87, 20);
            this.cb_const_name.TabIndex = 2;
            this.cb_const_name.SelectedIndexChanged += new System.EventHandler(this.OnCb_ConstName_SelectIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // tc_main
            // 
            this.tc_main.Controls.Add(this.tp_const);
            this.tc_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_main.Location = new System.Drawing.Point(0, 0);
            this.tc_main.Name = "tc_main";
            this.tc_main.SelectedIndex = 0;
            this.tc_main.Size = new System.Drawing.Size(201, 207);
            this.tc_main.TabIndex = 0;
            // 
            // MaterialEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tc_main);
            this.Name = "MaterialEditControl";
            this.Size = new System.Drawing.Size(201, 207);
            this.tp_const.ResumeLayout(false);
            this.tp_const.PerformLayout();
            this.tc_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tp_const;
        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.Button bu_delete;
        private System.Windows.Forms.Button bu_add;
        private System.Windows.Forms.ComboBox cb_const_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_const_density;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_const_viscosity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bu_const_save;
        private System.Windows.Forms.Button bu_const_copy;
    }
}
