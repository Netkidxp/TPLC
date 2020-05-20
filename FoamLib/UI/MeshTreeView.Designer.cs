namespace FoamLib.UI
{
    partial class MeshTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeshTreeView));
            this.tv_main = new System.Windows.Forms.TreeView();
            this.iml_main = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tv_main
            // 
            this.tv_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_main.ImageIndex = 0;
            this.tv_main.ImageList = this.iml_main;
            this.tv_main.LabelEdit = true;
            this.tv_main.Location = new System.Drawing.Point(0, 0);
            this.tv_main.Name = "tv_main";
            this.tv_main.SelectedImageIndex = 2;
            this.tv_main.Size = new System.Drawing.Size(258, 347);
            this.tv_main.TabIndex = 0;
            this.tv_main.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.OnAfterLabelEdit);
            this.tv_main.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            this.tv_main.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeDoubleClick);
            // 
            // iml_main
            // 
            this.iml_main.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_main.ImageStream")));
            this.iml_main.TransparentColor = System.Drawing.Color.Transparent;
            this.iml_main.Images.SetKeyName(0, "DocumentMap_16x16.png");
            this.iml_main.Images.SetKeyName(1, "MultiplePages_16x16.png");
            this.iml_main.Images.SetKeyName(2, "Pointer_16x16.png");
            // 
            // MeshTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tv_main);
            this.Name = "MeshTreeView";
            this.Size = new System.Drawing.Size(258, 347);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv_main;
        private System.Windows.Forms.ImageList iml_main;
    }
}
