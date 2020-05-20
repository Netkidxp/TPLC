namespace FoamLib.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("钢包结构");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("气体");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("钢液");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("相", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("相间作用");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("边界条件");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("初始化钢液位置1");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("初始化钢液位置2");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("钢液", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("气体");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("内部场", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("场", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("混匀效率计算");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("操作条件");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Pimple参数");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("求解控制");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("其他参数");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("工程", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode5,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            this.rbc_main = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.bu_case_new = new DevExpress.XtraBars.BarButtonItem();
            this.bu_case_open = new DevExpress.XtraBars.BarButtonItem();
            this.bu_save = new DevExpress.XtraBars.BarButtonItem();
            this.bu_saveas = new DevExpress.XtraBars.BarButtonItem();
            this.bu_case_save = new DevExpress.XtraBars.BarButtonItem();
            this.bu_case_saveas = new DevExpress.XtraBars.BarButtonItem();
            this.bu_desc_new = new DevExpress.XtraBars.BarButtonItem();
            this.bu_desc_open = new DevExpress.XtraBars.BarButtonItem();
            this.bu_desc_saveas = new DevExpress.XtraBars.BarButtonItem();
            this.bu_mesh_open = new DevExpress.XtraBars.BarButtonItem();
            this.bu_mesh_preview = new DevExpress.XtraBars.BarButtonItem();
            this.bci_mesh_surface = new DevExpress.XtraBars.BarCheckItem();
            this.bci_mesh_wireframe = new DevExpress.XtraBars.BarCheckItem();
            this.bci_mesh_point = new DevExpress.XtraBars.BarCheckItem();
            this.bu_tools_config = new DevExpress.XtraBars.BarButtonItem();
            this.bu_help_help = new DevExpress.XtraBars.BarButtonItem();
            this.bu_help_about = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bu_run_run = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bu_run_initlize = new DevExpress.XtraBars.BarButtonItem();
            this.bu_run_decomposepar = new DevExpress.XtraBars.BarButtonItem();
            this.bu_run_reconstruct = new DevExpress.XtraBars.BarButtonItem();
            this.bu_mesh_generate = new DevExpress.XtraBars.BarButtonItem();
            this.rp_file = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpg_case = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpg_mesh = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpg_mesh_view = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rp_solve = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpg_run = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rp_tools = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rp_help = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView_main = new System.Windows.Forms.TreeView();
            this.iml_main = new System.Windows.Forms.ImageList(this.components);
            this.propertyGrid_main = new System.Windows.Forms.PropertyGrid();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_model = new System.Windows.Forms.TabPage();
            this.progressPlane_main = new DevExpress.XtraWaitForm.ProgressPanel();
            this.foamViewer_main = new lflow_pre.FoamViewer();
            this.tabControl_Log = new System.Windows.Forms.TabControl();
            this.tablePage_common = new System.Windows.Forms.TabPage();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.tabPage_openfoam = new System.Windows.Forms.TabPage();
            this.tb_foamlog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.rbc_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tabPage_model.SuspendLayout();
            this.tabControl_Log.SuspendLayout();
            this.tablePage_common.SuspendLayout();
            this.tabPage_openfoam.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbc_main
            // 
            this.rbc_main.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.rbc_main.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.rbc_main.ExpandCollapseItem.Id = 0;
            this.rbc_main.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rbc_main.ExpandCollapseItem,
            this.bu_case_new,
            this.bu_case_open,
            this.bu_save,
            this.bu_saveas,
            this.bu_case_save,
            this.bu_case_saveas,
            this.bu_desc_new,
            this.bu_desc_open,
            this.bu_desc_saveas,
            this.bu_mesh_open,
            this.bu_mesh_preview,
            this.bci_mesh_surface,
            this.bci_mesh_wireframe,
            this.bci_mesh_point,
            this.bu_tools_config,
            this.bu_help_help,
            this.bu_help_about,
            this.barButtonItem1,
            this.bu_run_run,
            this.barButtonItem2,
            this.barButtonItem3,
            this.bu_run_initlize,
            this.bu_run_decomposepar,
            this.bu_run_reconstruct,
            this.bu_mesh_generate});
            this.rbc_main.Location = new System.Drawing.Point(0, 0);
            this.rbc_main.MaxItemId = 39;
            this.rbc_main.Name = "rbc_main";
            this.rbc_main.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rp_file,
            this.rp_solve,
            this.rp_tools,
            this.rp_help});
            this.rbc_main.Size = new System.Drawing.Size(1284, 163);
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.rbc_main;
            // 
            // bu_case_new
            // 
            this.bu_case_new.Caption = "新建";
            this.bu_case_new.Id = 3;
            this.bu_case_new.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_case_new.ImageOptions.Image")));
            this.bu_case_new.Name = "bu_case_new";
            this.bu_case_new.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewCase);
            // 
            // bu_case_open
            // 
            this.bu_case_open.Caption = "打开";
            this.bu_case_open.Id = 4;
            this.bu_case_open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_case_open.ImageOptions.Image")));
            this.bu_case_open.Name = "bu_case_open";
            this.bu_case_open.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnOpenCase);
            // 
            // bu_save
            // 
            this.bu_save.Caption = "Save";
            this.bu_save.Id = 5;
            this.bu_save.Name = "bu_save";
            // 
            // bu_saveas
            // 
            this.bu_saveas.Caption = "Save as";
            this.bu_saveas.Id = 6;
            this.bu_saveas.Name = "bu_saveas";
            // 
            // bu_case_save
            // 
            this.bu_case_save.Caption = "保存";
            this.bu_case_save.Id = 9;
            this.bu_case_save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_case_save.ImageOptions.Image")));
            this.bu_case_save.Name = "bu_case_save";
            this.bu_case_save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnSaveCase);
            // 
            // bu_case_saveas
            // 
            this.bu_case_saveas.Caption = "另存为";
            this.bu_case_saveas.Id = 10;
            this.bu_case_saveas.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_case_saveas.ImageOptions.Image")));
            this.bu_case_saveas.Name = "bu_case_saveas";
            this.bu_case_saveas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnSaveCaseAs);
            // 
            // bu_desc_new
            // 
            this.bu_desc_new.Caption = "New";
            this.bu_desc_new.Id = 11;
            this.bu_desc_new.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_desc_new.ImageOptions.Image")));
            this.bu_desc_new.Name = "bu_desc_new";
            // 
            // bu_desc_open
            // 
            this.bu_desc_open.Caption = "Open";
            this.bu_desc_open.Id = 12;
            this.bu_desc_open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_desc_open.ImageOptions.Image")));
            this.bu_desc_open.Name = "bu_desc_open";
            // 
            // bu_desc_saveas
            // 
            this.bu_desc_saveas.Caption = "Save As";
            this.bu_desc_saveas.Id = 13;
            this.bu_desc_saveas.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_desc_saveas.ImageOptions.Image")));
            this.bu_desc_saveas.Name = "bu_desc_saveas";
            // 
            // bu_mesh_open
            // 
            this.bu_mesh_open.Caption = "打开";
            this.bu_mesh_open.Id = 14;
            this.bu_mesh_open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_mesh_open.ImageOptions.Image")));
            this.bu_mesh_open.Name = "bu_mesh_open";
            this.bu_mesh_open.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnOpenMesh);
            // 
            // bu_mesh_preview
            // 
            this.bu_mesh_preview.Caption = "几何";
            this.bu_mesh_preview.Id = 15;
            this.bu_mesh_preview.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_mesh_preview.ImageOptions.Image")));
            this.bu_mesh_preview.Name = "bu_mesh_preview";
            this.bu_mesh_preview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bu_mesh_preview_ItemClick);
            // 
            // bci_mesh_surface
            // 
            this.bci_mesh_surface.BindableChecked = true;
            this.bci_mesh_surface.Caption = "面";
            this.bci_mesh_surface.Checked = true;
            this.bci_mesh_surface.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bci_mesh_surface.GroupIndex = 1;
            this.bci_mesh_surface.Id = 24;
            this.bci_mesh_surface.ImageOptions.Image = global::lflow_pre.Properties.Resources.bordersoutside_32x321;
            this.bci_mesh_surface.Name = "bci_mesh_surface";
            this.bci_mesh_surface.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnMeshViewSurface);
            // 
            // bci_mesh_wireframe
            // 
            this.bci_mesh_wireframe.Caption = "线框";
            this.bci_mesh_wireframe.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bci_mesh_wireframe.GroupIndex = 1;
            this.bci_mesh_wireframe.Id = 25;
            this.bci_mesh_wireframe.ImageOptions.Image = global::lflow_pre.Properties.Resources.bordersall_32x32;
            this.bci_mesh_wireframe.Name = "bci_mesh_wireframe";
            this.bci_mesh_wireframe.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnMeshViewWireframe);
            // 
            // bci_mesh_point
            // 
            this.bci_mesh_point.Caption = "点";
            this.bci_mesh_point.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bci_mesh_point.GroupIndex = 1;
            this.bci_mesh_point.Id = 26;
            this.bci_mesh_point.ImageOptions.Image = global::lflow_pre.Properties.Resources.borderlinestyle_32x32;
            this.bci_mesh_point.Name = "bci_mesh_point";
            this.bci_mesh_point.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnMeshViewPoint);
            // 
            // bu_tools_config
            // 
            this.bu_tools_config.Caption = "设置";
            this.bu_tools_config.Id = 28;
            this.bu_tools_config.ImageOptions.Image = global::lflow_pre.Properties.Resources.properties_32x32;
            this.bu_tools_config.Name = "bu_tools_config";
            this.bu_tools_config.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnToolConfig);
            // 
            // bu_help_help
            // 
            this.bu_help_help.Caption = "Help";
            this.bu_help_help.Id = 29;
            this.bu_help_help.ImageOptions.Image = global::lflow_pre.Properties.Resources.index_32x32;
            this.bu_help_help.Name = "bu_help_help";
            this.bu_help_help.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnHelpHelp);
            // 
            // bu_help_about
            // 
            this.bu_help_about.Caption = "About";
            this.bu_help_about.Id = 30;
            this.bu_help_about.ImageOptions.Image = global::lflow_pre.Properties.Resources.question_32x32;
            this.bu_help_about.Name = "bu_help_about";
            this.bu_help_about.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnHelpAbout);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 31;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bu_run_run
            // 
            this.bu_run_run.Caption = "计算";
            this.bu_run_run.Id = 32;
            this.bu_run_run.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_run_run.ImageOptions.Image")));
            this.bu_run_run.Name = "bu_run_run";
            this.bu_run_run.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bu_run_run_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 33;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 34;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // bu_run_initlize
            // 
            this.bu_run_initlize.Caption = "初始化";
            this.bu_run_initlize.Id = 35;
            this.bu_run_initlize.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_run_initlize.ImageOptions.Image")));
            this.bu_run_initlize.Name = "bu_run_initlize";
            this.bu_run_initlize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bu_run_initlize_ItemClick);
            // 
            // bu_run_decomposepar
            // 
            this.bu_run_decomposepar.Caption = "裂解";
            this.bu_run_decomposepar.Id = 36;
            this.bu_run_decomposepar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_run_decomposepar.ImageOptions.Image")));
            this.bu_run_decomposepar.Name = "bu_run_decomposepar";
            this.bu_run_decomposepar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bu_run_decomposepar_ItemClick);
            // 
            // bu_run_reconstruct
            // 
            this.bu_run_reconstruct.Caption = "合并";
            this.bu_run_reconstruct.Id = 37;
            this.bu_run_reconstruct.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bu_run_reconstruct.ImageOptions.Image")));
            this.bu_run_reconstruct.Name = "bu_run_reconstruct";
            this.bu_run_reconstruct.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bu_run_reconstruct_ItemClick);
            // 
            // bu_mesh_generate
            // 
            this.bu_mesh_generate.Caption = "生成";
            this.bu_mesh_generate.Id = 38;
            this.bu_mesh_generate.ImageOptions.Image = global::lflow_pre.Properties.Resources.apply_32x32;
            this.bu_mesh_generate.Name = "bu_mesh_generate";
            this.bu_mesh_generate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bu_mesh_generate_ItemClick);
            // 
            // rp_file
            // 
            this.rp_file.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpg_case,
            this.rpg_mesh,
            this.rpg_mesh_view});
            this.rp_file.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("rp_file.ImageOptions.Image")));
            this.rp_file.Name = "rp_file";
            this.rp_file.Text = "模型";
            // 
            // rpg_case
            // 
            this.rpg_case.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("rpg_case.ImageOptions.Image")));
            this.rpg_case.ItemLinks.Add(this.bu_case_new);
            this.rpg_case.ItemLinks.Add(this.bu_case_open);
            this.rpg_case.ItemLinks.Add(this.bu_case_save, true);
            this.rpg_case.ItemLinks.Add(this.bu_case_saveas);
            this.rpg_case.Name = "rpg_case";
            this.rpg_case.Text = "工程";
            // 
            // rpg_mesh
            // 
            this.rpg_mesh.ItemLinks.Add(this.bu_mesh_open);
            this.rpg_mesh.ItemLinks.Add(this.bu_mesh_preview);
            this.rpg_mesh.ItemLinks.Add(this.bu_mesh_generate);
            this.rpg_mesh.Name = "rpg_mesh";
            this.rpg_mesh.Text = "网格";
            // 
            // rpg_mesh_view
            // 
            this.rpg_mesh_view.ItemLinks.Add(this.bci_mesh_surface);
            this.rpg_mesh_view.ItemLinks.Add(this.bci_mesh_wireframe);
            this.rpg_mesh_view.ItemLinks.Add(this.bci_mesh_point);
            this.rpg_mesh_view.Name = "rpg_mesh_view";
            this.rpg_mesh_view.Text = "显示";
            // 
            // rp_solve
            // 
            this.rp_solve.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpg_run});
            this.rp_solve.ImageOptions.Image = global::lflow_pre.Properties.Resources.gaugestylefullcircular_32x32;
            this.rp_solve.Name = "rp_solve";
            this.rp_solve.Text = "求解";
            // 
            // rpg_run
            // 
            this.rpg_run.ItemLinks.Add(this.bu_run_initlize);
            this.rpg_run.ItemLinks.Add(this.bu_run_decomposepar);
            this.rpg_run.ItemLinks.Add(this.bu_run_run, true);
            this.rpg_run.ItemLinks.Add(this.bu_run_reconstruct);
            this.rpg_run.Name = "rpg_run";
            this.rpg_run.Text = "Run";
            // 
            // rp_tools
            // 
            this.rp_tools.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.rp_tools.ImageOptions.Image = global::lflow_pre.Properties.Resources.ide_32x32;
            this.rp_tools.Name = "rp_tools";
            this.rp_tools.Text = "工具";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.bu_tools_config);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Config";
            // 
            // rp_help
            // 
            this.rp_help.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.rp_help.ImageOptions.Image = global::lflow_pre.Properties.Resources.knowledgebasearticle_32x32;
            this.rp_help.Name = "rp_help";
            this.rp_help.Text = "帮助";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.bu_help_help);
            this.ribbonPageGroup2.ItemLinks.Add(this.bu_help_about);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Help";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 163);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1284, 715);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView_main);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid_main);
            this.splitContainer2.Size = new System.Drawing.Size(244, 715);
            this.splitContainer2.SplitterDistance = 413;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView_main
            // 
            this.treeView_main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_main.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_main.ImageIndex = 0;
            this.treeView_main.ImageList = this.iml_main;
            this.treeView_main.ItemHeight = 22;
            this.treeView_main.Location = new System.Drawing.Point(0, 0);
            this.treeView_main.Name = "treeView_main";
            treeNode1.ImageIndex = 123;
            treeNode1.Name = "Geometry";
            treeNode1.Text = "钢包结构";
            treeNode2.ImageIndex = 285;
            treeNode2.Name = "Gas";
            treeNode2.Text = "气体";
            treeNode3.ImageIndex = 140;
            treeNode3.Name = "Steel";
            treeNode3.Text = "钢液";
            treeNode4.ImageIndex = 0;
            treeNode4.Name = "Phase";
            treeNode4.Text = "相";
            treeNode5.ImageIndex = 31;
            treeNode5.Name = "Interfacial";
            treeNode5.Text = "相间作用";
            treeNode6.ImageIndex = 330;
            treeNode6.Name = "Boundary";
            treeNode6.Text = "边界条件";
            treeNode7.Name = "SteelInitlizePoint1";
            treeNode7.SelectedImageIndex = 191;
            treeNode7.Text = "初始化钢液位置1";
            treeNode8.Name = "SteelInitlizePoint2";
            treeNode8.SelectedImageIndex = 191;
            treeNode8.Text = "初始化钢液位置2";
            treeNode9.ImageIndex = 140;
            treeNode9.Name = "SteelInternalField";
            treeNode9.Text = "钢液";
            treeNode10.ImageIndex = 285;
            treeNode10.Name = "GasInternalField";
            treeNode10.Text = "气体";
            treeNode11.Name = "Internal Field";
            treeNode11.Text = "内部场";
            treeNode12.ImageIndex = 380;
            treeNode12.Name = "Field";
            treeNode12.Text = "场";
            treeNode13.ImageIndex = 148;
            treeNode13.Name = "Mixing";
            treeNode13.Text = "混匀效率计算";
            treeNode14.ImageIndex = 257;
            treeNode14.Name = "Operation Condition";
            treeNode14.Text = "操作条件";
            treeNode15.ImageIndex = 307;
            treeNode15.Name = "Pimple";
            treeNode15.Text = "Pimple参数";
            treeNode16.ImageIndex = 179;
            treeNode16.Name = "Control";
            treeNode16.Text = "求解控制";
            treeNode17.ImageIndex = 268;
            treeNode17.Name = "Other";
            treeNode17.Text = "其他参数";
            treeNode18.ImageIndex = 3;
            treeNode18.Name = "Case";
            treeNode18.Text = "工程";
            this.treeView_main.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode18});
            this.treeView_main.SelectedImageIndex = 8;
            this.treeView_main.Size = new System.Drawing.Size(242, 411);
            this.treeView_main.TabIndex = 0;
            this.treeView_main.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTreeViewItemClick);
            // 
            // iml_main
            // 
            this.iml_main.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_main.ImageStream")));
            this.iml_main.TransparentColor = System.Drawing.Color.Transparent;
            this.iml_main.Images.SetKeyName(0, "ColorMixer_16x16.png");
            this.iml_main.Images.SetKeyName(1, "GlobalColorScheme_16x16.png");
            this.iml_main.Images.SetKeyName(2, "LoadTheme_16x16.png");
            this.iml_main.Images.SetKeyName(3, "LocalColorScheme_16x16.png");
            this.iml_main.Images.SetKeyName(4, "SaveTheme_16x16.png");
            this.iml_main.Images.SetKeyName(5, "Add_16x16.png");
            this.iml_main.Images.SetKeyName(6, "AddFile_16x16.png");
            this.iml_main.Images.SetKeyName(7, "AddItem_16x16.png");
            this.iml_main.Images.SetKeyName(8, "Apply_16x16.png");
            this.iml_main.Images.SetKeyName(9, "Cancel_16x16.png");
            this.iml_main.Images.SetKeyName(10, "Clear_16x16.png");
            this.iml_main.Images.SetKeyName(11, "ClearFormatting_16x16.png");
            this.iml_main.Images.SetKeyName(12, "ClearTableStyle_16x16.png");
            this.iml_main.Images.SetKeyName(13, "Clip_16x16.png");
            this.iml_main.Images.SetKeyName(14, "Close_16x16.png");
            this.iml_main.Images.SetKeyName(15, "Convert_16x16.png");
            this.iml_main.Images.SetKeyName(16, "ConvertToRange_16x16.png");
            this.iml_main.Images.SetKeyName(17, "DeleteList_16x16.png");
            this.iml_main.Images.SetKeyName(18, "DeleteList2_16x16.png");
            this.iml_main.Images.SetKeyName(19, "Down_16x16.png");
            this.iml_main.Images.SetKeyName(20, "Download_16x16.png");
            this.iml_main.Images.SetKeyName(21, "EditName_16x16.png");
            this.iml_main.Images.SetKeyName(22, "Fill_16x16.png");
            this.iml_main.Images.SetKeyName(23, "FormatAsTable_16x16.png");
            this.iml_main.Images.SetKeyName(24, "Group_16x16.png");
            this.iml_main.Images.SetKeyName(25, "Group2_16x16.png");
            this.iml_main.Images.SetKeyName(26, "Hide_16x16.png");
            this.iml_main.Images.SetKeyName(27, "ImportImage_16x16.png");
            this.iml_main.Images.SetKeyName(28, "Insert_16x16.png");
            this.iml_main.Images.SetKeyName(29, "Left_16x16.png");
            this.iml_main.Images.SetKeyName(30, "LoadFrom_16x16.png");
            this.iml_main.Images.SetKeyName(31, "Merge_16x16.png");
            this.iml_main.Images.SetKeyName(32, "New_16x16.png");
            this.iml_main.Images.SetKeyName(33, "NewTableStyle_16x16.png");
            this.iml_main.Images.SetKeyName(34, "Open_16x16.png");
            this.iml_main.Images.SetKeyName(35, "Open2_16x16.png");
            this.iml_main.Images.SetKeyName(36, "OpenHyperlink_16x16.png");
            this.iml_main.Images.SetKeyName(37, "Reading_16x16.png");
            this.iml_main.Images.SetKeyName(38, "initlize");
            this.iml_main.Images.SetKeyName(39, "Refresh2_16x16.png");
            this.iml_main.Images.SetKeyName(40, "Remove_16x16.png");
            this.iml_main.Images.SetKeyName(41, "RemoveItem_16x16.png");
            this.iml_main.Images.SetKeyName(42, "Reset_16x16.png");
            this.iml_main.Images.SetKeyName(43, "Reset2_16x16.png");
            this.iml_main.Images.SetKeyName(44, "Right_16x16.png");
            this.iml_main.Images.SetKeyName(45, "SelectAll_16x16.png");
            this.iml_main.Images.SetKeyName(46, "SelectAll2_16x16.png");
            this.iml_main.Images.SetKeyName(47, "Show_16x16.png");
            this.iml_main.Images.SetKeyName(48, "Squeeze_16x16.png");
            this.iml_main.Images.SetKeyName(49, "Stretch_16x16.png");
            this.iml_main.Images.SetKeyName(50, "SwitchRowColumn_16x16.png");
            this.iml_main.Images.SetKeyName(51, "Trash_16x16.png");
            this.iml_main.Images.SetKeyName(52, "Up2_16x16.png");
            this.iml_main.Images.SetKeyName(53, "ErrorBarsPercentage_16x16.png");
            this.iml_main.Images.SetKeyName(54, "decompose");
            this.iml_main.Images.SetKeyName(55, "reconstruct");
            this.iml_main.Images.SetKeyName(56, "UpDownBarsNone_16x16.png");
            this.iml_main.Images.SetKeyName(57, "ErrorBarsNone_16x16.png");
            this.iml_main.Images.SetKeyName(58, "HighLowLines_16x16.png");
            this.iml_main.Images.SetKeyName(59, "UpDownBars_16x16.png");
            this.iml_main.Images.SetKeyName(60, "ErrorBarsStandardDeviation_16x16.png");
            this.iml_main.Images.SetKeyName(61, "NoneLines_16x16.png");
            this.iml_main.Images.SetKeyName(62, "ErrorBars_16x16.png");
            this.iml_main.Images.SetKeyName(63, "DropLines_16x16.png");
            this.iml_main.Images.SetKeyName(64, "LineStyle_16x16.png");
            this.iml_main.Images.SetKeyName(65, "ColorMixer_16x16.png");
            this.iml_main.Images.SetKeyName(66, "GlobalColorScheme_16x16.png");
            this.iml_main.Images.SetKeyName(67, "LoadTheme_16x16.png");
            this.iml_main.Images.SetKeyName(68, "LocalColorScheme_16x16.png");
            this.iml_main.Images.SetKeyName(69, "SaveTheme_16x16.png");
            this.iml_main.Images.SetKeyName(70, "BringToFrontOfText_16x16.png");
            this.iml_main.Images.SetKeyName(71, "Through_16x16.png");
            this.iml_main.Images.SetKeyName(72, "InFrontOfText_16x16.png");
            this.iml_main.Images.SetKeyName(73, "Tight_16x16.png");
            this.iml_main.Images.SetKeyName(74, "SendBehindText_16x16.png");
            this.iml_main.Images.SetKeyName(75, "Square_16x16.png");
            this.iml_main.Images.SetKeyName(76, "TopAndBottom_16x16.png");
            this.iml_main.Images.SetKeyName(77, "MoreLayoutOptions_16x16.png");
            this.iml_main.Images.SetKeyName(78, "WrapText_16x16.png");
            this.iml_main.Images.SetKeyName(79, "BehindText_16x16.png");
            this.iml_main.Images.SetKeyName(80, "EditWrapPoints_16x16.png");
            this.iml_main.Images.SetKeyName(81, "WithTextWrapping_BottomCenter_16x16.png");
            this.iml_main.Images.SetKeyName(82, "WithTextWrapping_CenterCenter_16x16.png");
            this.iml_main.Images.SetKeyName(83, "BringForward_16x16.png");
            this.iml_main.Images.SetKeyName(84, "WithTextWrapping_TopCenter_16x16.png");
            this.iml_main.Images.SetKeyName(85, "BringToFront_16x16.png");
            this.iml_main.Images.SetKeyName(86, "WithTextWrapping_BottomRight_16x16.png");
            this.iml_main.Images.SetKeyName(87, "WithTextWrapping_TopLeft_16x16.png");
            this.iml_main.Images.SetKeyName(88, "WithTextWrapping_CenterRight_16x16.png");
            this.iml_main.Images.SetKeyName(89, "WithTextWrapping_CenterLeft_16x16.png");
            this.iml_main.Images.SetKeyName(90, "WithTextWrapping_TopRight_16x16.png");
            this.iml_main.Images.SetKeyName(91, "WithTextWrapping_BottomLeft_16x16.png");
            this.iml_main.Images.SetKeyName(92, "SendToBack_16x16.png");
            this.iml_main.Images.SetKeyName(93, "SendBackward_16x16.png");
            this.iml_main.Images.SetKeyName(94, "InLineWithText_16x16.png");
            this.iml_main.Images.SetKeyName(95, "DoublePrev_16x16.png");
            this.iml_main.Images.SetKeyName(96, "DoubleNext_16x16.png");
            this.iml_main.Images.SetKeyName(97, "Prev_16x16.png");
            this.iml_main.Images.SetKeyName(98, "Next_16x16.png");
            this.iml_main.Images.SetKeyName(99, "Last_16x16.png");
            this.iml_main.Images.SetKeyName(100, "First_16x16.png");
            this.iml_main.Images.SetKeyName(101, "DoubleLast_16x16.png");
            this.iml_main.Images.SetKeyName(102, "DoubleFirst_16x16.png");
            this.iml_main.Images.SetKeyName(103, "MoveDown_16x16.png");
            this.iml_main.Images.SetKeyName(104, "MoveUp_16x16.png");
            this.iml_main.Images.SetKeyName(105, "Record_16x16.png");
            this.iml_main.Images.SetKeyName(106, "Play_16x16.png");
            this.iml_main.Images.SetKeyName(107, "Pause_16x16.png");
            this.iml_main.Images.SetKeyName(108, "Stop_16x16.png");
            this.iml_main.Images.SetKeyName(109, "BODepartment_16x16.png");
            this.iml_main.Images.SetKeyName(110, "BONote_16x16.png");
            this.iml_main.Images.SetKeyName(111, "BOScheduler_16x16.png");
            this.iml_main.Images.SetKeyName(112, "BORole_16x16.png");
            this.iml_main.Images.SetKeyName(113, "BOLocalization_16x16.png");
            this.iml_main.Images.SetKeyName(114, "BOFolder_16x16.png");
            this.iml_main.Images.SetKeyName(115, "BOCountry_16x16.png");
            this.iml_main.Images.SetKeyName(116, "BOSale_16x16.png");
            this.iml_main.Images.SetKeyName(117, "BOSaleItem_16x16.png");
            this.iml_main.Images.SetKeyName(118, "BOContact_16x16.png");
            this.iml_main.Images.SetKeyName(119, "BOUser_16x16.png");
            this.iml_main.Images.SetKeyName(120, "BOProductGroup_16x16.png");
            this.iml_main.Images.SetKeyName(121, "BODetails_16x16.png");
            this.iml_main.Images.SetKeyName(122, "BORules_16x16.png");
            this.iml_main.Images.SetKeyName(123, "BOProduct_16x16.png");
            this.iml_main.Images.SetKeyName(124, "BOOrderItem_16x16.png");
            this.iml_main.Images.SetKeyName(125, "BOPosition_16x16.png");
            this.iml_main.Images.SetKeyName(126, "BOOrder_16x16.png");
            this.iml_main.Images.SetKeyName(127, "BOPerson_16x16.png");
            this.iml_main.Images.SetKeyName(128, "BOEmployee_16x16.png");
            this.iml_main.Images.SetKeyName(129, "BOCustomer_16x16.png");
            this.iml_main.Images.SetKeyName(130, "BOPermission_16x16.png");
            this.iml_main.Images.SetKeyName(131, "BOPosition2_16x16.png");
            this.iml_main.Images.SetKeyName(132, "BOReport_16x16.png");
            this.iml_main.Images.SetKeyName(133, "BOChangeHistory_16x16.png");
            this.iml_main.Images.SetKeyName(134, "BOReport2_16x16.png");
            this.iml_main.Images.SetKeyName(135, "BOFileAttachment_16x16.png");
            this.iml_main.Images.SetKeyName(136, "BOTask_16x16.png");
            this.iml_main.Images.SetKeyName(137, "BOContact2_16x16.png");
            this.iml_main.Images.SetKeyName(138, "BOResume_16x16.png");
            this.iml_main.Images.SetKeyName(139, "BOPivotChart_16x16.png");
            this.iml_main.Images.SetKeyName(140, "OtherCharts_16x16.png");
            this.iml_main.Images.SetKeyName(141, "PieStyleDonut_16x16.png");
            this.iml_main.Images.SetKeyName(142, "Pie2_16x16.png");
            this.iml_main.Images.SetKeyName(143, "DrillDownOnSeries_Pie_16x16.png");
            this.iml_main.Images.SetKeyName(144, "Pie_16x16.png");
            this.iml_main.Images.SetKeyName(145, "PieStylePie_16x16.png");
            this.iml_main.Images.SetKeyName(146, "Doughnut_16x16.png");
            this.iml_main.Images.SetKeyName(147, "RadarWithMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(148, "ExplodedDoughnut_16x16.png");
            this.iml_main.Images.SetKeyName(149, "PieLabelsDataLabels_16x16.png");
            this.iml_main.Images.SetKeyName(150, "DrillDown_16x16.png");
            this.iml_main.Images.SetKeyName(151, "ChangeChartSeriesType_16x16.png");
            this.iml_main.Images.SetKeyName(152, "DrillDownOnArguments_Pie_16x16.png");
            this.iml_main.Images.SetKeyName(153, "ChartShowCaptions_16x16.png");
            this.iml_main.Images.SetKeyName(154, "KPI_16x16.png");
            this.iml_main.Images.SetKeyName(155, "RadarWithoutMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(156, "ExplodedPie_16x16.png");
            this.iml_main.Images.SetKeyName(157, "FilledRadarWithoutMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(158, "Pie3_16x16.png");
            this.iml_main.Images.SetKeyName(159, "AddChartPane_16x16.png");
            this.iml_main.Images.SetKeyName(160, "ChartXAxisSettings_16x16.png");
            this.iml_main.Images.SetKeyName(161, "ClusteredHorizontalPyramid_16x16.png");
            this.iml_main.Images.SetKeyName(162, "Area2_16x16.png");
            this.iml_main.Images.SetKeyName(163, "FullStackedLineWithMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(164, "DrillDownOnArguments_Chart_16x16.png");
            this.iml_main.Images.SetKeyName(165, "ChartsRotate_16x16.png");
            this.iml_main.Images.SetKeyName(166, "ExplodedPie3D_16x16.png");
            this.iml_main.Images.SetKeyName(167, "ScatterWithStraightLinesAndMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(168, "StackedLineWithMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(169, "DrillDownOnSeries_Chart_16x16.png");
            this.iml_main.Images.SetKeyName(170, "ChartYAxisSettings2_16x16.png");
            this.iml_main.Images.SetKeyName(171, "ScatterWithSmoothLinesAndMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(172, "ClusteredPyramid_16x16.png");
            this.iml_main.Images.SetKeyName(173, "PreviewChart_16x16.png");
            this.iml_main.Images.SetKeyName(174, "FullStackedHorizontalCone_16x16.png");
            this.iml_main.Images.SetKeyName(175, "FullStackedHorizontalPyramid_16x16.png");
            this.iml_main.Images.SetKeyName(176, "StackedHorizontalPyramid_16x16.png");
            this.iml_main.Images.SetKeyName(177, "Pie3D_16x16.png");
            this.iml_main.Images.SetKeyName(178, "Cone_16x16.png");
            this.iml_main.Images.SetKeyName(179, "LineWithMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(180, "StackedHorizontalCone_16x16.png");
            this.iml_main.Images.SetKeyName(181, "ClusteredHorizontalCone_16x16.png");
            this.iml_main.Images.SetKeyName(182, "FullStackedLine_16x16.png");
            this.iml_main.Images.SetKeyName(183, "StackedArea2_16x16.png");
            this.iml_main.Images.SetKeyName(184, "FullStackedHorizontalCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(185, "3DStackedArea_16x16.png");
            this.iml_main.Images.SetKeyName(186, "ChartYAxisSettings_16x16.png");
            this.iml_main.Images.SetKeyName(187, "Pyramid_16x16.png");
            this.iml_main.Images.SetKeyName(188, "StackedLine_16x16.png");
            this.iml_main.Images.SetKeyName(189, "FullStackedCone_16x16.png");
            this.iml_main.Images.SetKeyName(190, "StackedHorizontalCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(191, "ScatterWithStraightLines_16x16.png");
            this.iml_main.Images.SetKeyName(192, "ClusteredHorizontalCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(193, "FullStackedColumn_16x16.png");
            this.iml_main.Images.SetKeyName(194, "StackedCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(195, "StackedPyramid_16x16.png");
            this.iml_main.Images.SetKeyName(196, "ChartTitlesNone_16x16.png");
            this.iml_main.Images.SetKeyName(197, "StackedLineWithoutMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(198, "BarOfPie_16x16.png");
            this.iml_main.Images.SetKeyName(199, "3DCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(200, "FullStackedPyramid_16x16.png");
            this.iml_main.Images.SetKeyName(201, "StackedCone_16x16.png");
            this.iml_main.Images.SetKeyName(202, "ClusteredCone_16x16.png");
            this.iml_main.Images.SetKeyName(203, "Area3D_16x16.png");
            this.iml_main.Images.SetKeyName(204, "Line_16x16.png");
            this.iml_main.Images.SetKeyName(205, "StackedArea_16x16.png");
            this.iml_main.Images.SetKeyName(206, "PieOfPie_16x16.png");
            this.iml_main.Images.SetKeyName(207, "Bubble3D_16x16.png");
            this.iml_main.Images.SetKeyName(208, "3DLine_16x16.png");
            this.iml_main.Images.SetKeyName(209, "StackedColumn_16x16.png");
            this.iml_main.Images.SetKeyName(210, "ScatterWithOnlyMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(211, "SplineArea_16x16.png");
            this.iml_main.Images.SetKeyName(212, "StackedSplineArea_16x16.png");
            this.iml_main.Images.SetKeyName(213, "FullStackedCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(214, "LabelsNone_16x16.png");
            this.iml_main.Images.SetKeyName(215, "VerticalAxisThousands_16x16.png");
            this.iml_main.Images.SetKeyName(216, "VerticalAxisTitles_None_16x16.png");
            this.iml_main.Images.SetKeyName(217, "Area3_16x16.png");
            this.iml_main.Images.SetKeyName(218, "Bubble2_16x16.png");
            this.iml_main.Images.SetKeyName(219, "OpenHighLowCloseCandleStick2_16x16.png");
            this.iml_main.Images.SetKeyName(220, "LineWithoutMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(221, "VerticalAxisBillions_16x16.png");
            this.iml_main.Images.SetKeyName(222, "HorizontalAxisBillions_16x16.png");
            this.iml_main.Images.SetKeyName(223, "VerticalAxisMillions_16x16.png");
            this.iml_main.Images.SetKeyName(224, "Line2_16x16.png");
            this.iml_main.Images.SetKeyName(225, "HorizontalAxisMillions_16x16.png");
            this.iml_main.Images.SetKeyName(226, "HorizontalAxisThousands_16x16.png");
            this.iml_main.Images.SetKeyName(227, "VerticalAxisLogScale_16x16.png");
            this.iml_main.Images.SetKeyName(228, "FullStackedArea2_16x16.png");
            this.iml_main.Images.SetKeyName(229, "HorizontalAxisTitle_None_16x16.png");
            this.iml_main.Images.SetKeyName(230, "ClusteredBar3D_16x16.png");
            this.iml_main.Images.SetKeyName(231, "FullStackedBar_16x16.png");
            this.iml_main.Images.SetKeyName(232, "FullStackedBar2_16x16.png");
            this.iml_main.Images.SetKeyName(233, "LegendNone_16x16.png");
            this.iml_main.Images.SetKeyName(234, "SideBySideRangeBar_16x16.png");
            this.iml_main.Images.SetKeyName(235, "FullStackedColumn3D_16x16.png");
            this.iml_main.Images.SetKeyName(236, "HorizontalAxisLogScale_16x16.png");
            this.iml_main.Images.SetKeyName(237, "ScatterWithSmoothLines_16x16.png");
            this.iml_main.Images.SetKeyName(238, "StackedColumn3D_16x16.png");
            this.iml_main.Images.SetKeyName(239, "RangeArea_16x16.png");
            this.iml_main.Images.SetKeyName(240, "VerticalAxisNone_16x16.png");
            this.iml_main.Images.SetKeyName(241, "StackedBar2_16x16.png");
            this.iml_main.Images.SetKeyName(242, "ClusteredColumn_16x16.png");
            this.iml_main.Images.SetKeyName(243, "OpenHighLowCloseCandleStick_16x16.png");
            this.iml_main.Images.SetKeyName(244, "StackedBar_16x16.png");
            this.iml_main.Images.SetKeyName(245, "3DFullStackedArea_16x16.png");
            this.iml_main.Images.SetKeyName(246, "VerticalAxisTitles_RotatedText_16x16.png");
            this.iml_main.Images.SetKeyName(247, "3DColumn_16x16.png");
            this.iml_main.Images.SetKeyName(248, "ChartTitlesCenteredOverlayTitle_16x16.png");
            this.iml_main.Images.SetKeyName(249, "Area_16x16.png");
            this.iml_main.Images.SetKeyName(250, "Bubble_16x16.png");
            this.iml_main.Images.SetKeyName(251, "ClusteredCylinder_16x16.png");
            this.iml_main.Images.SetKeyName(252, "Column_16x16.png");
            this.iml_main.Images.SetKeyName(253, "HorizontalAxisNone_16x16.png");
            this.iml_main.Images.SetKeyName(254, "LabelsNone2_16x16.png");
            this.iml_main.Images.SetKeyName(255, "ChartTitlesAboveChart_16x16.png");
            this.iml_main.Images.SetKeyName(256, "VerticalAxisTitles_VerticalText_16x16.png");
            this.iml_main.Images.SetKeyName(257, "Spline_16x16.png");
            this.iml_main.Images.SetKeyName(258, "Column2_16x16.png");
            this.iml_main.Images.SetKeyName(259, "FullStackedLineWithoutMarkers_16x16.png");
            this.iml_main.Images.SetKeyName(260, "LabelsInsideCenter_16x16.png");
            this.iml_main.Images.SetKeyName(261, "ClusteredBar_16x16.png");
            this.iml_main.Images.SetKeyName(262, "FullStackedArea_16x16.png");
            this.iml_main.Images.SetKeyName(263, "HorizontalAxisLeftToRight_16x16.png");
            this.iml_main.Images.SetKeyName(264, "Stepline_16x16.png");
            this.iml_main.Images.SetKeyName(265, "HorizontalAxisRightToLeft_16x16.png");
            this.iml_main.Images.SetKeyName(266, "AxisTitles_16x16.png");
            this.iml_main.Images.SetKeyName(267, "ColorLegend_16x16.png");
            this.iml_main.Images.SetKeyName(268, "LabelsOutsideEnd_16x16.png");
            this.iml_main.Images.SetKeyName(269, "BottomCenterHorizontalInside_16x16.png");
            this.iml_main.Images.SetKeyName(270, "FullStackedSplineArea_16x16.png");
            this.iml_main.Images.SetKeyName(271, "HorizontalAxisTopDown_16x16.png");
            this.iml_main.Images.SetKeyName(272, "VerticalAxisBottomUp_16x16.png");
            this.iml_main.Images.SetKeyName(273, "3DClusteredColumn_16x16.png");
            this.iml_main.Images.SetKeyName(274, "Bar_16x16.png");
            this.iml_main.Images.SetKeyName(275, "PieLabelsTooltips_16x16.png");
            this.iml_main.Images.SetKeyName(276, "BottomCenterVerticalInside_16x16.png");
            this.iml_main.Images.SetKeyName(277, "TopCenterVerticalInside_16x16.png");
            this.iml_main.Images.SetKeyName(278, "Chart_16x16.png");
            this.iml_main.Images.SetKeyName(279, "BottomLeftVerticalInside_16x16.png");
            this.iml_main.Images.SetKeyName(280, "ChangeChartLegendAlignment_16x16.png");
            this.iml_main.Images.SetKeyName(281, "RangeBar_16x16.png");
            this.iml_main.Images.SetKeyName(282, "TopRightVerticalInside_16x16.png");
            this.iml_main.Images.SetKeyName(283, "BottomLeftHorizontalInside_16x16.png");
            this.iml_main.Images.SetKeyName(284, "BottomRightVerticalInside_16x16.png");
            this.iml_main.Images.SetKeyName(285, "Point_16x16.png");
            this.iml_main.Images.SetKeyName(286, "VerticalAxisTitles_16x16.png");
            this.iml_main.Images.SetKeyName(287, "BottomRightHorizontalInside_16x16.png");
            this.iml_main.Images.SetKeyName(288, "TopLeftVerticalInside_16x16.png");
            this.iml_main.Images.SetKeyName(289, "TopRightHorizontalInside_16x16.png");
            this.iml_main.Images.SetKeyName(290, "StackedBar3D_16x16.png");
            this.iml_main.Images.SetKeyName(291, "LabelsAbove_16x16.png");
            this.iml_main.Images.SetKeyName(292, "LabelsInsideBase_16x16.png");
            this.iml_main.Images.SetKeyName(293, "TopLeftHorizontalInside_16x16.png");
            this.iml_main.Images.SetKeyName(294, "BottomRightHorizontalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(295, "LabelsBelow_16x16.png");
            this.iml_main.Images.SetKeyName(296, "BottomCenterVerticalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(297, "TopCenterHorizontalInside_16x16.png");
            this.iml_main.Images.SetKeyName(298, "BottomCenterHorizontalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(299, "LabelsInsideEnd_16x16.png");
            this.iml_main.Images.SetKeyName(300, "LegendLeft_16x16.png");
            this.iml_main.Images.SetKeyName(301, "TopCenterHorizontalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(302, "BottomLeftVerticalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(303, "LabelsLeft_16x16.png");
            this.iml_main.Images.SetKeyName(304, "TopRightHorizontalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(305, "TopRightVerticalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(306, "BottomLeftHorizontalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(307, "ChartsShowLegend_16x16.png");
            this.iml_main.Images.SetKeyName(308, "TopLeftVerticalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(309, "TopLeftHorizontalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(310, "VerticalAxisTitles_HorizonlalText_16x16.png");
            this.iml_main.Images.SetKeyName(311, "TopCenterVerticalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(312, "BottomRightVerticalOutside_16x16.png");
            this.iml_main.Images.SetKeyName(313, "LabelsRight_16x16.png");
            this.iml_main.Images.SetKeyName(314, "LegendTop_16x16.png");
            this.iml_main.Images.SetKeyName(315, "LegendBottom_16x16.png");
            this.iml_main.Images.SetKeyName(316, "LegendRight_16x16.png");
            this.iml_main.Images.SetKeyName(317, "LegendRightOverlay_16x16.png");
            this.iml_main.Images.SetKeyName(318, "Axes_16x16.png");
            this.iml_main.Images.SetKeyName(319, "FullStackedBar3d_16x16.png");
            this.iml_main.Images.SetKeyName(320, "HorizontalAxisTitle_16x16.png");
            this.iml_main.Images.SetKeyName(321, "Bar2_16x16.png");
            this.iml_main.Images.SetKeyName(322, "StepArea_16x16.png");
            this.iml_main.Images.SetKeyName(323, "HorizontalAxisDefault_16x16.png");
            this.iml_main.Images.SetKeyName(324, "LabelsCenter_16x16.png");
            this.iml_main.Images.SetKeyName(325, "VerticalAxisDefault_16x16.png");
            this.iml_main.Images.SetKeyName(326, "LegendLeftOverlay_16x16.png");
            this.iml_main.Images.SetKeyName(327, "HorizontalAxisWithoutLabeling_16x16.png");
            this.iml_main.Images.SetKeyName(328, "VerticallAxisWithoutLabeling_16x16.png");
            this.iml_main.Images.SetKeyName(329, "OpenHighLowCloseStock_16x16.png");
            this.iml_main.Images.SetKeyName(330, "HighLowClose2_16x16.png");
            this.iml_main.Images.SetKeyName(331, "Scatter_32x32.png");
            this.iml_main.Images.SetKeyName(332, "HighLowClose_16x16.png");
            this.iml_main.Images.SetKeyName(333, "Scatter_16x16.png");
            this.iml_main.Images.SetKeyName(334, "IconSetSymbolsCircled3_16x16.png");
            this.iml_main.Images.SetKeyName(335, "IconSetFlags3_16x16.png");
            this.iml_main.Images.SetKeyName(336, "IconSetStars3_16x16.png");
            this.iml_main.Images.SetKeyName(337, "RulesManager_32x32.png");
            this.iml_main.Images.SetKeyName(338, "IconSetTrafficLights3_16x16.png");
            this.iml_main.Images.SetKeyName(339, "IconSetTrafficLights4_16x16.png");
            this.iml_main.Images.SetKeyName(340, "IconSetSigns3_16x16.png");
            this.iml_main.Images.SetKeyName(341, "ClearRules_16x16.png");
            this.iml_main.Images.SetKeyName(342, "IconSetRedToBlack4_16x16.png");
            this.iml_main.Images.SetKeyName(343, "IconSetArrows3_16x16.png");
            this.iml_main.Images.SetKeyName(344, "IconSetArrows4_16x16.png");
            this.iml_main.Images.SetKeyName(345, "IconSetArrows5_16x16.png");
            this.iml_main.Images.SetKeyName(346, "ManageRules_16x16.png");
            this.iml_main.Images.SetKeyName(347, "IconSetArrowsGray3_16x16.png");
            this.iml_main.Images.SetKeyName(348, "IconSetArrowsGray4_16x16.png");
            this.iml_main.Images.SetKeyName(349, "IconSetArrowsGray5_16x16.png");
            this.iml_main.Images.SetKeyName(350, "IconSetTrafficLightsRimmed3_16x16.png");
            this.iml_main.Images.SetKeyName(351, "IconSetQuarters5_16x16.png");
            this.iml_main.Images.SetKeyName(352, "IconSetSymbols3_16x16.png");
            this.iml_main.Images.SetKeyName(353, "Bottom10%_16x16.png");
            this.iml_main.Images.SetKeyName(354, "Top_16x16.png");
            this.iml_main.Images.SetKeyName(355, "TopBottomRules_16x16.png");
            this.iml_main.Images.SetKeyName(356, "IconSetTriangles3_16x16.png");
            this.iml_main.Images.SetKeyName(357, "IconSetBoxes5_16x16.png");
            this.iml_main.Images.SetKeyName(358, "AboveAverage_16x16.png");
            this.iml_main.Images.SetKeyName(359, "IconSetRating4_16x16.png");
            this.iml_main.Images.SetKeyName(360, "IconSetRating5_16x16.png");
            this.iml_main.Images.SetKeyName(361, "BelowAverage_16x16.png");
            this.iml_main.Images.SetKeyName(362, "Top10Items_16x16.png");
            this.iml_main.Images.SetKeyName(363, "Bottom10Items_16x16.png");
            this.iml_main.Images.SetKeyName(364, "LessThan_16x16.png");
            this.iml_main.Images.SetKeyName(365, "ConditionalFormatting_16x16.png");
            this.iml_main.Images.SetKeyName(366, "HighlightCellsRules_16x16.png");
            this.iml_main.Images.SetKeyName(367, "GreaterThan_16x16.png");
            this.iml_main.Images.SetKeyName(368, "ADateOccurring_16x16.png");
            this.iml_main.Images.SetKeyName(369, "TextThatContains_16x16.png");
            this.iml_main.Images.SetKeyName(370, "DuplicateValues_16x16.png");
            this.iml_main.Images.SetKeyName(371, "Between_16x16.png");
            this.iml_main.Images.SetKeyName(372, "GreenYellow_16x16.png");
            this.iml_main.Images.SetKeyName(373, "EqualTo_16x16.png");
            this.iml_main.Images.SetKeyName(374, "RulesManager_16x16.png");
            this.iml_main.Images.SetKeyName(375, "GradientBlueDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(376, "GradientGreenDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(377, "GradientLightBlueDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(378, "GradientOrangeDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(379, "GradientPurpleDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(380, "GradientRedDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(381, "SolidBlueDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(382, "SolidGreenDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(383, "SolidLightBlueDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(384, "SolidOrangeDataBar_16x16.png");
            this.iml_main.Images.SetKeyName(385, "SolidPurpleDataBar_16x16 - 副本.png");
            this.iml_main.Images.SetKeyName(386, "SolidRedDataBar_16x16 - 副本.png");
            this.iml_main.Images.SetKeyName(387, "stop");
            this.iml_main.Images.SetKeyName(388, "run");
            // 
            // propertyGrid_main
            // 
            this.propertyGrid_main.CategorySplitterColor = System.Drawing.SystemColors.AppWorkspace;
            this.propertyGrid_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_main.LineColor = System.Drawing.SystemColors.GrayText;
            this.propertyGrid_main.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid_main.Name = "propertyGrid_main";
            this.propertyGrid_main.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid_main.Size = new System.Drawing.Size(242, 296);
            this.propertyGrid_main.TabIndex = 0;
            this.propertyGrid_main.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnCaseModified);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tabControl_main);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl_Log);
            this.splitContainer3.Size = new System.Drawing.Size(1036, 715);
            this.splitContainer3.SplitterDistance = 564;
            this.splitContainer3.TabIndex = 0;
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.tabPage_model);
            this.tabControl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_main.ItemSize = new System.Drawing.Size(42, 18);
            this.tabControl_main.Location = new System.Drawing.Point(0, 0);
            this.tabControl_main.Multiline = true;
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.Padding = new System.Drawing.Point(3, 3);
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(1034, 562);
            this.tabControl_main.TabIndex = 2;
            // 
            // tabPage_model
            // 
            this.tabPage_model.Controls.Add(this.progressPlane_main);
            this.tabPage_model.Controls.Add(this.foamViewer_main);
            this.tabPage_model.Location = new System.Drawing.Point(4, 22);
            this.tabPage_model.Name = "tabPage_model";
            this.tabPage_model.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage_model.Size = new System.Drawing.Size(1026, 536);
            this.tabPage_model.TabIndex = 0;
            this.tabPage_model.Text = "模型";
            this.tabPage_model.UseVisualStyleBackColor = true;
            // 
            // progressPlane_main
            // 
            this.progressPlane_main.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPlane_main.Appearance.Options.UseBackColor = true;
            this.progressPlane_main.BarAnimationElementThickness = 2;
            this.progressPlane_main.LineAnimationElementType = DevExpress.Utils.Animation.LineAnimationElementType.Rectangle;
            this.progressPlane_main.Location = new System.Drawing.Point(372, 207);
            this.progressPlane_main.Name = "progressPlane_main";
            this.progressPlane_main.Size = new System.Drawing.Size(212, 65);
            this.progressPlane_main.TabIndex = 1;
            this.progressPlane_main.Text = "progressPanel1";
            // 
            // foamViewer_main
            // 
            this.foamViewer_main.AddTestActors = false;
            this.foamViewer_main.BackColor = System.Drawing.Color.Transparent;
            this.foamViewer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foamViewer_main.HightLightColor = System.Drawing.Color.Red;
            this.foamViewer_main.Location = new System.Drawing.Point(1, 1);
            this.foamViewer_main.MeshColor = System.Drawing.Color.SlateGray;
            this.foamViewer_main.Name = "foamViewer_main";
            this.foamViewer_main.Representation = lflow_pre.FoamViewer.MeshRepresentation.Point;
            this.foamViewer_main.Size = new System.Drawing.Size(1024, 534);
            this.foamViewer_main.TabIndex = 0;
            this.foamViewer_main.TestText = null;
            // 
            // tabControl_Log
            // 
            this.tabControl_Log.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl_Log.Controls.Add(this.tablePage_common);
            this.tabControl_Log.Controls.Add(this.tabPage_openfoam);
            this.tabControl_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Log.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Log.Multiline = true;
            this.tabControl_Log.Name = "tabControl_Log";
            this.tabControl_Log.SelectedIndex = 0;
            this.tabControl_Log.Size = new System.Drawing.Size(1034, 145);
            this.tabControl_Log.TabIndex = 1;
            // 
            // tablePage_common
            // 
            this.tablePage_common.Controls.Add(this.rtb_log);
            this.tablePage_common.Location = new System.Drawing.Point(4, 4);
            this.tablePage_common.Name = "tablePage_common";
            this.tablePage_common.Padding = new System.Windows.Forms.Padding(3);
            this.tablePage_common.Size = new System.Drawing.Size(1008, 137);
            this.tablePage_common.TabIndex = 0;
            this.tablePage_common.Text = "通用";
            this.tablePage_common.UseVisualStyleBackColor = true;
            // 
            // rtb_log
            // 
            this.rtb_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Location = new System.Drawing.Point(3, 3);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.ReadOnly = true;
            this.rtb_log.Size = new System.Drawing.Size(1002, 131);
            this.rtb_log.TabIndex = 0;
            this.rtb_log.Text = "";
            // 
            // tabPage_openfoam
            // 
            this.tabPage_openfoam.Controls.Add(this.tb_foamlog);
            this.tabPage_openfoam.Location = new System.Drawing.Point(4, 4);
            this.tabPage_openfoam.Name = "tabPage_openfoam";
            this.tabPage_openfoam.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_openfoam.Size = new System.Drawing.Size(1008, 137);
            this.tabPage_openfoam.TabIndex = 1;
            this.tabPage_openfoam.Text = "求解";
            this.tabPage_openfoam.UseVisualStyleBackColor = true;
            // 
            // tb_foamlog
            // 
            this.tb_foamlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_foamlog.Location = new System.Drawing.Point(3, 3);
            this.tb_foamlog.Multiline = true;
            this.tb_foamlog.Name = "tb_foamlog";
            this.tb_foamlog.ReadOnly = true;
            this.tb_foamlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_foamlog.Size = new System.Drawing.Size(1002, 131);
            this.tb_foamlog.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 878);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.rbc_main);
            this.Name = "MainForm";
            this.Text = "钢包底吹氩搅拌仿真系统";
            ((System.ComponentModel.ISupportInitialize)(this.rbc_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl_main.ResumeLayout(false);
            this.tabPage_model.ResumeLayout(false);
            this.tabControl_Log.ResumeLayout(false);
            this.tablePage_common.ResumeLayout(false);
            this.tabPage_openfoam.ResumeLayout(false);
            this.tabPage_openfoam.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rbc_main;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp_file;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_case;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGrid_main;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBox rtb_log;
        private DevExpress.XtraBars.BarButtonItem bu_case_new;
        private DevExpress.XtraBars.BarButtonItem bu_case_open;
        private DevExpress.XtraBars.BarButtonItem bu_save;
        private DevExpress.XtraBars.BarButtonItem bu_saveas;
        private DevExpress.XtraBars.BarButtonItem bu_case_save;
        private DevExpress.XtraBars.BarButtonItem bu_case_saveas;
        private DevExpress.XtraBars.BarButtonItem bu_desc_new;
        private DevExpress.XtraBars.BarButtonItem bu_desc_open;
        private DevExpress.XtraBars.BarButtonItem bu_desc_saveas;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_mesh;
        private DevExpress.XtraBars.BarButtonItem bu_mesh_open;
        private DevExpress.XtraBars.BarButtonItem bu_mesh_preview;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_mesh_view;
        private DevExpress.XtraBars.BarCheckItem bci_mesh_surface;
        private DevExpress.XtraBars.BarCheckItem bci_mesh_wireframe;
        private DevExpress.XtraBars.BarCheckItem bci_mesh_point;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp_tools;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp_help;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem bu_tools_config;
        private DevExpress.XtraBars.BarButtonItem bu_help_help;
        private DevExpress.XtraBars.BarButtonItem bu_help_about;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private System.Windows.Forms.TreeView treeView_main;
        private System.Windows.Forms.ImageList iml_main;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem bu_run_run;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_run;
        private DevExpress.XtraBars.BarButtonItem bu_run_initlize;
        private DevExpress.XtraBars.BarButtonItem bu_run_decomposepar;
        private DevExpress.XtraBars.BarButtonItem bu_run_reconstruct;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp_solve;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_model;
        private FoamViewer foamViewer_main;
        private DevExpress.XtraWaitForm.ProgressPanel progressPlane_main;
        private System.Windows.Forms.TabControl tabControl_Log;
        private System.Windows.Forms.TabPage tablePage_common;
        private System.Windows.Forms.TabPage tabPage_openfoam;
        private System.Windows.Forms.TextBox tb_foamlog;
        private DevExpress.XtraBars.BarButtonItem bu_mesh_generate;
    }
}