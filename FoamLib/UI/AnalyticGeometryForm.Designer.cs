namespace FoamLib.UI
{
    partial class AnalyticGeometryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyticGeometryForm));
            this.agf_main = new FoamLib.UI.AnalyticGeometryControl();
            this.SuspendLayout();
            // 
            // agf_main
            // 
            this.agf_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.agf_main.Geometry = new FoamLib.Model.CfMesh.Box();
            this.agf_main.Location = new System.Drawing.Point(0, 0);
            this.agf_main.Name = "agf_main";
            this.agf_main.Size = new System.Drawing.Size(254, 232);
            this.agf_main.TabIndex = 0;
            // 
            // AnalyticGeometryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 232);
            this.Controls.Add(this.agf_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AnalyticGeometryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analytic geometry";
            this.ResumeLayout(false);

        }

        #endregion

        private AnalyticGeometryControl agf_main;
    }
}