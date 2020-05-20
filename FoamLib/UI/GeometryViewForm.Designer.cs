namespace FoamLib.UI
{
    partial class GeometryViewForm
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
            this.geometryViewer1 = new GeometryViewer();
            this.SuspendLayout();
            // 
            // geometryViewer1
            // 
            this.geometryViewer1.AddTestActors = false;
            this.geometryViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geometryViewer1.GeometryColor = System.Drawing.Color.Gray;
            this.geometryViewer1.Location = new System.Drawing.Point(0, 0);
            this.geometryViewer1.Name = "geometryViewer1";
            this.geometryViewer1.Size = new System.Drawing.Size(564, 533);
            this.geometryViewer1.TabIndex = 0;
            this.geometryViewer1.TestText = null;
            // 
            // GeometryViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 533);
            this.Controls.Add(this.geometryViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GeometryViewForm";
            this.Text = "钢包结构预览";
            this.ResumeLayout(false);

        }

        #endregion

        private GeometryViewer geometryViewer1;
    }
}