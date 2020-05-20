namespace FoamLib.UI
{
    partial class SetupDlg
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
            this.pg_main = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pg_main
            // 
            this.pg_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg_main.Location = new System.Drawing.Point(0, 0);
            this.pg_main.Name = "pg_main";
            this.pg_main.Size = new System.Drawing.Size(678, 511);
            this.pg_main.TabIndex = 0;
            // 
            // SetupDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 511);
            this.Controls.Add(this.pg_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetupDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pg_main;
    }
}