namespace TplcLicenseManager
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbHardwareId = new System.Windows.Forms.TextBox();
            this.lvMain = new System.Windows.Forms.ListView();
            this.buCheck = new System.Windows.Forms.Button();
            this.ComponentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Guid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DayCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExpiredDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.localHardwareIdTextBox1 = new Fqh.CommonLib.LocalHardwareIdTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setComponentDayCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAllComponentDayCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbHardwareId
            // 
            this.tbHardwareId.Location = new System.Drawing.Point(22, 28);
            this.tbHardwareId.Name = "tbHardwareId";
            this.tbHardwareId.Size = new System.Drawing.Size(246, 21);
            this.tbHardwareId.TabIndex = 0;
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ComponentName,
            this.Guid,
            this.State,
            this.StartDate,
            this.DayCount,
            this.ExpiredDate});
            this.lvMain.ContextMenuStrip = this.contextMenuStrip1;
            this.lvMain.Location = new System.Drawing.Point(22, 70);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(1178, 401);
            this.lvMain.TabIndex = 1;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            // 
            // buCheck
            // 
            this.buCheck.Location = new System.Drawing.Point(285, 25);
            this.buCheck.Name = "buCheck";
            this.buCheck.Size = new System.Drawing.Size(75, 23);
            this.buCheck.TabIndex = 2;
            this.buCheck.Text = "check";
            this.buCheck.UseVisualStyleBackColor = true;
            this.buCheck.Click += new System.EventHandler(this.buCheck_Click);
            // 
            // ComponentName
            // 
            this.ComponentName.Text = "Component Name";
            this.ComponentName.Width = 438;
            // 
            // Guid
            // 
            this.Guid.Text = "Guid";
            this.Guid.Width = 308;
            // 
            // State
            // 
            this.State.Text = "State";
            // 
            // StartDate
            // 
            this.StartDate.Text = "Start Date";
            this.StartDate.Width = 132;
            // 
            // DayCount
            // 
            this.DayCount.Text = "Day Count";
            this.DayCount.Width = 82;
            // 
            // ExpiredDate
            // 
            this.ExpiredDate.Text = "Expired Date";
            this.ExpiredDate.Width = 137;
            // 
            // localHardwareIdTextBox1
            // 
            this.localHardwareIdTextBox1.Location = new System.Drawing.Point(450, 29);
            this.localHardwareIdTextBox1.Name = "localHardwareIdTextBox1";
            this.localHardwareIdTextBox1.Size = new System.Drawing.Size(210, 20);
            this.localHardwareIdTextBox1.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setComponentDayCountToolStripMenuItem,
            this.setAllComponentDayCountToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(284, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuOpening);
            // 
            // setComponentDayCountToolStripMenuItem
            // 
            this.setComponentDayCountToolStripMenuItem.Name = "setComponentDayCountToolStripMenuItem";
            this.setComponentDayCountToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.setComponentDayCountToolStripMenuItem.Text = "Set selected components day count";
            this.setComponentDayCountToolStripMenuItem.Click += new System.EventHandler(this.setComponentDayCountToolStripMenuItem_Click);
            // 
            // setAllComponentDayCountToolStripMenuItem
            // 
            this.setAllComponentDayCountToolStripMenuItem.Name = "setAllComponentDayCountToolStripMenuItem";
            this.setAllComponentDayCountToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.setAllComponentDayCountToolStripMenuItem.Text = "Set all component day count";
            this.setAllComponentDayCountToolStripMenuItem.Click += new System.EventHandler(this.setAllComponentDayCountToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 512);
            this.Controls.Add(this.localHardwareIdTextBox1);
            this.Controls.Add(this.buCheck);
            this.Controls.Add(this.lvMain);
            this.Controls.Add(this.tbHardwareId);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbHardwareId;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.Button buCheck;
        private System.Windows.Forms.ColumnHeader ComponentName;
        private System.Windows.Forms.ColumnHeader Guid;
        private System.Windows.Forms.ColumnHeader State;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader DayCount;
        private System.Windows.Forms.ColumnHeader ExpiredDate;
        private Fqh.CommonLib.LocalHardwareIdTextBox localHardwareIdTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setComponentDayCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAllComponentDayCountToolStripMenuItem;
    }
}

