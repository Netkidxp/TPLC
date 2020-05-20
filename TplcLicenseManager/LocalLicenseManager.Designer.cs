namespace TplcLicenseManager
{
    partial class LocalLicenseManager
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
            this.tbHardwareId = new System.Windows.Forms.TextBox();
            this.bu_new = new System.Windows.Forms.Button();
            this.bu_write = new System.Windows.Forms.Button();
            this.bu_read = new System.Windows.Forms.Button();
            this.pgMain = new System.Windows.Forms.PropertyGrid();
            this.localHardwareIdTextBox1 = new Fqh.CommonLib.LocalHardwareIdTextBox();
            this.SuspendLayout();
            // 
            // tbHardwareId
            // 
            this.tbHardwareId.Location = new System.Drawing.Point(12, 10);
            this.tbHardwareId.Name = "tbHardwareId";
            this.tbHardwareId.Size = new System.Drawing.Size(246, 21);
            this.tbHardwareId.TabIndex = 5;
            // 
            // bu_new
            // 
            this.bu_new.Location = new System.Drawing.Point(288, 7);
            this.bu_new.Name = "bu_new";
            this.bu_new.Size = new System.Drawing.Size(75, 23);
            this.bu_new.TabIndex = 7;
            this.bu_new.Text = "new";
            this.bu_new.UseVisualStyleBackColor = true;
            this.bu_new.Click += new System.EventHandler(this.bu_new_Click);
            // 
            // bu_write
            // 
            this.bu_write.Location = new System.Drawing.Point(526, 8);
            this.bu_write.Name = "bu_write";
            this.bu_write.Size = new System.Drawing.Size(75, 23);
            this.bu_write.TabIndex = 8;
            this.bu_write.Text = "write";
            this.bu_write.UseVisualStyleBackColor = true;
            this.bu_write.Click += new System.EventHandler(this.bu_write_Click);
            // 
            // bu_read
            // 
            this.bu_read.Location = new System.Drawing.Point(410, 7);
            this.bu_read.Name = "bu_read";
            this.bu_read.Size = new System.Drawing.Size(75, 23);
            this.bu_read.TabIndex = 9;
            this.bu_read.Text = "read";
            this.bu_read.UseVisualStyleBackColor = true;
            this.bu_read.Click += new System.EventHandler(this.bu_read_Click);
            // 
            // pgMain
            // 
            this.pgMain.Location = new System.Drawing.Point(12, 49);
            this.pgMain.Name = "pgMain";
            this.pgMain.Size = new System.Drawing.Size(717, 399);
            this.pgMain.TabIndex = 10;
            // 
            // localHardwareIdTextBox1
            // 
            this.localHardwareIdTextBox1.Location = new System.Drawing.Point(519, 461);
            this.localHardwareIdTextBox1.Name = "localHardwareIdTextBox1";
            this.localHardwareIdTextBox1.Size = new System.Drawing.Size(210, 20);
            this.localHardwareIdTextBox1.TabIndex = 11;
            // 
            // LocalLicenseManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 493);
            this.Controls.Add(this.localHardwareIdTextBox1);
            this.Controls.Add(this.pgMain);
            this.Controls.Add(this.bu_read);
            this.Controls.Add(this.bu_write);
            this.Controls.Add(this.bu_new);
            this.Controls.Add(this.tbHardwareId);
            this.Name = "LocalLicenseManager";
            this.Text = "LocalLicenseManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbHardwareId;
        private System.Windows.Forms.Button bu_new;
        private System.Windows.Forms.Button bu_write;
        private System.Windows.Forms.Button bu_read;
        private System.Windows.Forms.PropertyGrid pgMain;
        private Fqh.CommonLib.LocalHardwareIdTextBox localHardwareIdTextBox1;
    }
}