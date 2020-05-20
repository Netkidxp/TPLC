namespace TplcModelLib.Ui
{
    partial class AboutForm
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
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.lbAppicationName = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.tcLicenses = new System.Windows.Forms.TabControl();
            this.buOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLicenseUntil = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.localHardwareIdTextBox1 = new Fqh.CommonLib.LocalHardwareIdTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(676, 88);
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            // 
            // lbAppicationName
            // 
            this.lbAppicationName.AutoSize = true;
            this.lbAppicationName.Location = new System.Drawing.Point(22, 106);
            this.lbAppicationName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAppicationName.Name = "lbAppicationName";
            this.lbAppicationName.Size = new System.Drawing.Size(73, 17);
            this.lbAppicationName.TabIndex = 1;
            this.lbAppicationName.Text = "TPLC-Solver";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(22, 125);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(37, 17);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "1.0.0";
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Location = new System.Drawing.Point(22, 144);
            this.lbCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(140, 17);
            this.lbCopyright.TabIndex = 3;
            this.lbCopyright.Text = "Copyright NETKIDXP.CN";
            // 
            // tcLicenses
            // 
            this.tcLicenses.Location = new System.Drawing.Point(10, 205);
            this.tcLicenses.Name = "tcLicenses";
            this.tcLicenses.SelectedIndex = 0;
            this.tcLicenses.Size = new System.Drawing.Size(655, 222);
            this.tcLicenses.TabIndex = 4;
            // 
            // buOK
            // 
            this.buOK.Location = new System.Drawing.Point(597, 432);
            this.buOK.Name = "buOK";
            this.buOK.Size = new System.Drawing.Size(66, 26);
            this.buOK.TabIndex = 5;
            this.buOK.Text = "OK";
            this.buOK.UseVisualStyleBackColor = true;
            this.buOK.Click += new System.EventHandler(this.buOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "The license is valid until : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hardware ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLicenseUntil
            // 
            this.lbLicenseUntil.AutoSize = true;
            this.lbLicenseUntil.Location = new System.Drawing.Point(459, 125);
            this.lbLicenseUntil.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLicenseUntil.Name = "lbLicenseUntil";
            this.lbLicenseUntil.Size = new System.Drawing.Size(42, 17);
            this.lbLicenseUntil.TabIndex = 10;
            this.lbLicenseUntil.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 185);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Component licenses:";
            // 
            // localHardwareIdTextBox1
            // 
            this.localHardwareIdTextBox1.Location = new System.Drawing.Point(396, 106);
            this.localHardwareIdTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.localHardwareIdTextBox1.Name = "localHardwareIdTextBox1";
            this.localHardwareIdTextBox1.Size = new System.Drawing.Size(246, 18);
            this.localHardwareIdTextBox1.TabIndex = 9;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 469);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbLicenseUntil);
            this.Controls.Add(this.localHardwareIdTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buOK);
            this.Controls.Add(this.tcLicenses);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbAppicationName);
            this.Controls.Add(this.pbMain);
            this.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "AboutForm";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Label lbAppicationName;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.TabControl tcLicenses;
        private System.Windows.Forms.Button buOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Fqh.CommonLib.LocalHardwareIdTextBox localHardwareIdTextBox1;
        private System.Windows.Forms.Label lbLicenseUntil;
        private System.Windows.Forms.Label label1;
    }
}