using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace FoamLib.UI
{
    public partial class MainForm : Form, IMonitor
    {

        LflowMsgManager msgManager = null;
        FoamRunner runner = null;
        Case currentCase = null;
        bool caseModified = false;
        int busyFlag = 0;
        string currentMeshFileName = "";
        string currentCaseFileName = "";
        string configFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"config");
        string logFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "commom.log");
        GlobalConfig foamConfig;
        CaseReader caseReader = null;
        CaseWriter caseWriter = null;
        CaseConvertor caseConvertor = null;
        TreeNode treeNode_boundary = null;
        OnBoundaryTypeChange onBoundaryTypeChange;
        OnWallHeatTypeChange onWallHeatTypeChange;
        OnPlugTypeChange onPlugTypeChange;
        TreeNode currentTreeNode;
        ResidualErrorChart currentChart;
        List<MonitorChart> monitorCharts = new List<MonitorChart>();
        StreamWriter commonLogWriter = null;

        public MainForm()
        {
            caseReader = new CaseReader();
            caseWriter = new CaseWriter();
            caseConvertor = new CaseConvertor();
            caseReader.OnTaskStarted += OnOpenCaseStart;
            caseReader.OnTaskFinished += OnOpenCaseFinished;
            caseWriter.OnTaskStarted += OnWriteCaseStart;
            caseWriter.OnTaskFinished += OnWriteCaseFinished;
            caseConvertor.OnTaskStarted += OnConvertCaseStart;
            caseConvertor.OnTaskFinished += OnConvertCaseFinished;
            currentTreeNode = null;
            if(!File.Exists(configFileName))
            {
                foamConfig = GlobalConfig.Default;
                foamConfig.Write(configFileName);
            }
            else
                foamConfig = GlobalConfig.Read(configFileName);
            if (!File.Exists(logFileName))
                File.Create(logFileName).Close();
            commonLogWriter = new StreamWriter(logFileName, true, new UTF8Encoding(false));
            runner = new FoamRunner(foamConfig);
            InitializeComponent();
            runner.OnOutput = this.OnRunnerOutput;
            runner.OnError = this.OnRunnerError;
            runner.OnExit = this.OnRunnerExit;
            runner.IsWriteErr = true;
            runner.IsWriteLog = true;
            onPlugTypeChange = new OnPlugTypeChange(
                (me,patchName,from,to)=>
                {
                    if (from == to)
                        return;
                    Plug plug = null;
                    switch(to)
                    {
                        case Plug.PLUG_TYPE.BasicPlug:
                            plug = new Plug(patchName);
                            plug.Alpha = me.Alpha;
                            plug.GasVelocity = me.GasVelocity;
                            plug.MeanSlitRadius = me.MeanSlitRadius;plug.Radius = me.Radius;
                            plug.Radius = me.Radius;
                            break;
                        case Plug.PLUG_TYPE.PorousPlug:
                            plug = new PorousPlug(patchName);
                            plug.Alpha = me.Alpha;
                            plug.GasVelocity = me.GasVelocity;
                            plug.MeanSlitRadius = me.MeanSlitRadius; plug.Radius = me.Radius;
                            plug.Radius = me.Radius;
                            break;
                        case Plug.PLUG_TYPE.SlitPlug:
                            plug = new SlitPlug(patchName);
                            break;
                    }
                    if(plug!=null)
                    {
                        currentCase.Field.Boundarys.Remove(me);
                        currentCase.Field.Boundarys.Add(plug);
                        currentTreeNode = treeView_main.SelectedNode;
                        UpdateCaseObject();
                    }
                }
            );
            onWallHeatTypeChange = new OnWallHeatTypeChange(
                (me,patchName,from,to)=>
                {
                    if (from == to)
                        return;

                    Wall wall = null;
                    switch (to)
                    {
                        case Wall.WALL_TYPE.Adiabat:
                            wall = new AdiabatWall(patchName);
                            wall.Layers = me.Layers;
                            break;
                        case Wall.WALL_TYPE.Coefficent:
                            wall = new HeatTransferWall(patchName);
                            wall.Layers = me.Layers;
                            break;
                        case Wall.WALL_TYPE.Flux:
                            wall = new HeatFluxWall(patchName);
                            wall.Layers = me.Layers;
                            break;
                        case Wall.WALL_TYPE.Power:
                            wall = new PowerWall(patchName);
                            wall.Layers = me.Layers;
                            break;
                        case Wall.WALL_TYPE.Temperature:
                            wall = new TemperatureWall(patchName);
                            wall.Layers = me.Layers;
                            break;
                    }
                    if(wall!=null)
                    {
                        currentCase.Field.Boundarys.Remove(me);
                        currentCase.Field.Boundarys.Add(wall);
                        currentTreeNode = treeView_main.SelectedNode;
                        UpdateCaseObject();
                    }
                }
                );
            onBoundaryTypeChange = new OnBoundaryTypeChange(
                (me, patchName, from, to) =>
                {
                    if (from != to)
                    {
                        
                        Boundary bn = null;
                        switch (to)
                        {
                            case Boundary.BOUNDARY_TYPE.Plug:
                                bn = new Plug(patchName);
                                break;
                            case Boundary.BOUNDARY_TYPE.Symmetry:
                                bn = new Symmetry(patchName);
                                break;
                            case Boundary.BOUNDARY_TYPE.Upface:
                                bn = new Upface(patchName);
                                break;
                            case Boundary.BOUNDARY_TYPE.Wall:
                                bn = new AdiabatWall(patchName);
                                break;
                        }
                        if (bn != null)
                        {
                            currentCase.Field.Boundarys.Remove(me);
                            currentCase.Field.Boundarys.Add(bn);
                            currentTreeNode = treeView_main.SelectedNode;
                            UpdateCaseObject();
                        }
                    }
                }
                );
            foamViewer_main.OnReadMeshStarted += OnReadMeshStart;
            foamViewer_main.OnReadMeshFinished += OnReadMeshFinished;
            foamViewer_main.OnPatchSelected += OnPatchSelected;
            foamViewer_main.MeshColor = foamConfig.MeshColor;
            foamViewer_main.HightLightColor = foamConfig.HighLightMeshColor;
            InitCase();
            InitControls();
            treeNode_boundary = treeView_main.Nodes["Case"].Nodes["Field"].Nodes["Boundary"];
            msgManager = new LflowMsgManager(rtb_log,tb_foamlog);
            //msgManager.AsyncStartProcesser();

        }

        public void InitCase()
        {
            currentCase = new Case();
            CaseModified = false;
            UpdateCaseObject();
        }

        public void InitControls()
        {
            progressPlane_main.Visible = false;
            foamViewer_main.Representation = FoamViewer.MeshRepresentation.Surface;
            bu_run_run.ImageOptions.Image = iml_main.Images["run"];
            bu_run_initlize.ImageOptions.Image = iml_main.Images["initlize"];
            bu_run_decomposepar.ImageOptions.Image = iml_main.Images["decompose"];
            bu_run_reconstruct.ImageOptions.Image = iml_main.Images["reconstruct"];
        }

        public void FillBoundaryTreeNode()
        {
            PolyMesh mesh = new PolyMesh(currentMeshFileName);
            List<string> patchNames = mesh.GetAllPatchNames();
            treeNode_boundary.Nodes.Clear();
            foreach (string patchName in patchNames)
            {
                treeNode_boundary.Nodes.Add(patchName, patchName, 2);
            }
        }

        public void InitCaseBoundary()
        {
            if (currentMeshFileName == "")
                return;
            PolyMesh mesh = new PolyMesh(currentMeshFileName);
            List<string> patches = mesh.GetPatchNamesByType("patch");
            List<string> walls = mesh.GetPatchNamesByType("wall");
            List<string> syms = mesh.GetPatchNamesByType("symmetry");
            currentCase.Field.Boundarys.Clear();
            
            foreach (string wall in walls)
            {
                AdiabatWall aw = new AdiabatWall(wall);
                currentCase.Field.Boundarys.Add(aw);
            }
            foreach (string sym in syms)
            {
                Symmetry sp = new Symmetry(sym);
                currentCase.Field.Boundarys.Add(sp);
            }
            foreach(string pn in patches)
            {
                if(pn.StartsWith("up",StringComparison.OrdinalIgnoreCase))
                {
                    Upface uf = new Upface(pn);
                    currentCase.Field.Boundarys.Add(uf);
                }
                else
                {
                    Plug p = new Plug(pn);
                    currentCase.Field.Boundarys.Add(p);
                }
            }
            UpdateCaseObject();
        }

        private delegate void DgAppendRichBoxText(string txt, Color color);

        private void AppendCommonLog(string text, Color color)
        {
            if (rtb_log.InvokeRequired)
            {
                DgAppendRichBoxText dg = new DgAppendRichBoxText(AppendCommonLog);
                rtb_log.Invoke(dg, new object[] { text, color });
            }
            else
            {
                rtb_log.SelectionStart = rtb_log.TextLength;
                rtb_log.SelectionLength = 0;
                rtb_log.SelectionColor = color;
                rtb_log.AppendText(text);
            }
        }

        private delegate void DgAppendTextBoxText(string txt);

        private void AppendFoamLog(string text)
        {
            int maxLogSize = (int)(foamConfig.MaxLogBufferSize >= 60000 ? foamConfig.MaxLogBufferSize : 60000);
            if (tb_foamlog == null)
                return;
            if (tb_foamlog.InvokeRequired)
            {
                DgAppendTextBoxText dg = new DgAppendTextBoxText(AppendFoamLog);
                tb_foamlog.Invoke(dg, new object[] { text });
            }
            else
            {
                if (tb_foamlog.Text.Length > maxLogSize)
                    tb_foamlog.Text = tb_foamlog.Text.Substring(maxLogSize / 2 );
                tb_foamlog.AppendText(text);
            }

        }

        public void FoamLog(string log)
        {
            AppendFoamLog(log + "\r\n");
            //msgManager.Push(Message.FoamLogMessage(log,this));
        }

        public void FoamError(string err)
        {
            AppendFoamLog(err + "\r\n");
            //msgManager.Push(Message.FoamErrorMessage(err, this));
        }

        public void ErrorLine(string err)
        {
            //msgManager.Push(Message.ErrorMessage(err, this));
            var txt = "[" + DateTime.Now + "] "+ err;
            AppendCommonLog(txt + "\r\n", Color.Red);
            if(commonLogWriter!=null)
                commonLogWriter.WriteLineAsync(txt);
        }

        public void Error(string err)
        {
            //msgManager.Push(Message.ErrorMessage(err, this));
            AppendCommonLog(err, Color.Red);
            if (commonLogWriter != null)
                commonLogWriter.WriteLineAsync(err);
        }

        public void LogLine(string log)
        {
            //msgManager.Push(Message.InformationMessage(log, this));
            var txt = "[" + DateTime.Now + "] " + log;
            AppendCommonLog(txt + "\r\n", Color.Blue);
            if (commonLogWriter != null)
                commonLogWriter.WriteLineAsync(txt);
        }

        public void Log(string log)
        {
            //msgManager.Push(Message.InformationMessage(log, this));
            AppendCommonLog(log, Color.Black);
            if (commonLogWriter != null)
                commonLogWriter.WriteLineAsync(log);
        }

        public void Progress(float point)
        {
            msgManager.Push(Message.ProgressMessage(point, this));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(runner.IsRunning)
            {
                DialogResult r = MessageBox.Show("强行停止会导致不可预料的错误，是否继续?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    runner.Abort();
                    DisableBars(bu_run_run);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            if(IsBusy)
            {
                MessageBox.Show("系统正在工作，请稍后", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            CheckModified();
            base.OnClosing(e);
        }

        private void NewCase()
        {
            currentCase = new Case();
            UpdateCaseObject();
            CaseModified = true;
            currentCaseFileName = "";
            currentMeshFileName = "";
            foamViewer_main.SetMeshFileName("");
            treeNode_boundary.Nodes.Clear();
        }

        private void CheckModified()
        {
            if (CaseModified)
            {
                if (MessageBox.Show("工程已经被修改，是否保存?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (currentCaseFileName == "")
                    {
                        OnSaveCaseAs(this, null);
                    }
                    else
                    {
                        OnSaveCase(this, null);
                    }
                }
            }
            
        }

        private void OpenCase()
        {
            caseReader.AsyncOpenCase(currentCaseFileName,this);
        }

        private void OnNewCase(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckModified();
            NewCase();
        }

        private void OnOpenCase(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckModified();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.DefaultExt = "lfw";
            ofd.Filter = "*.lfw|*.lfw";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentCaseFileName = ofd.FileName;
                CaseModified = false;
                OpenCase();
            }
        }

        private void SaveCase()
        {
            caseWriter.AsyncSaveCase(currentCase, currentMeshFileName, currentCaseFileName);
        }

        private bool CheckMeshBeforeSave()
        {
            if (currentMeshFileName == "")
            {
                MessageBox.Show("当前工程文件没有网格", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLine("当前工程文件没有网格");
                return false;
            }
            if (!FoamMeshVerifier.VerifyVxtPath(currentMeshFileName))
            {
                MessageBox.Show("网格路径非法", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLine("网格路径非法");
                return false;
            }
            return true;
        }

        private void OnSaveCase(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckMeshBeforeSave())
                return;
            if (currentCaseFileName == "")
                OnSaveCaseAs(sender, e);
            else
                SaveCase();
                
        }

        private void OnSaveCaseAs(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckMeshBeforeSave())
                return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = "lfw";
            sfd.Filter = "*.lfw|*.lfw";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                currentCaseFileName = sfd.FileName;
                CaseModified = false;
                SaveCase();
            }
        }

        private void OnOpenMesh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                if(!FoamMeshVerifier.VerifyDirectory(fbd.SelectedPath))
                {
                    MessageBox.Show("网格验证失败，路径:\n" + fbd.SelectedPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLine("网格验证失败，路径: " + fbd.SelectedPath);
                    return;
                }
                else
                {
                    CaseModified = true;
                    currentMeshFileName = FoamConst.GetVxtFilePath(fbd.SelectedPath);
                    File.Create(currentMeshFileName);
                    FillBoundaryTreeNode();
                    InitCaseBoundary();
                    foamViewer_main.SetMeshFileName(currentMeshFileName);
                }

            }
        }

        private void OnOpenFluentMesh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void OnMeshViewSurface(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foamViewer_main.Representation =  FoamViewer.MeshRepresentation.Surface;
        }

        private void OnMeshViewWireframe(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foamViewer_main.Representation = FoamViewer.MeshRepresentation.Wireframe;
        }

        private void OnMeshViewPoint(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foamViewer_main.Representation = FoamViewer.MeshRepresentation.Point;
        }

        private void OnToolConfig(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetupDlg dlg = new SetupDlg(foamConfig);
            dlg.ShowDialog();
            foamConfig.Write();
        }

        private void OnHelpHelp(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void OnHelpAbout(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private bool CheckModified(string msg)
        {
            if (CaseModified)
            {
                if (MessageBox.Show(msg, "question") == DialogResult.OK)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private void Busy(string toolTipTitle, string toolTip)
        {
            busyFlag += 1;
            progressPlane_main.Caption = toolTip;
            progressPlane_main.Description = toolTipTitle;
            progressPlane_main.Visible = IsBusy;
        }

        private void UnBusy()
        {
            busyFlag -= 1;
            progressPlane_main.Visible = IsBusy;
        }

        bool IsBusy
        {
            get => busyFlag != 0;
        }

        public bool CaseModified
        {
            get
            {
                return caseModified;
            }
            set
            {
                bu_case_save.Enabled = value;
                bu_case_saveas.Enabled = value;
                bu_run_initlize.Enabled = !value;
                bu_run_decomposepar.Enabled = !value;
                bu_run_run.Enabled = !value;
                bu_run_reconstruct.Enabled = !value;
                caseModified = value;
            }
        }

        private void OnConvertCaseStart(Object sender, TaskEventArgs e)
        {
            Busy("正在建立求解环境", "请稍等...");
            LogLine("开始建立求解环境,目标目录:"+e.Objects["RunDirName"]);
            propertyGrid_main.Enabled = false;
            SetFileOperationStatus(false);
            SetRibbonPageGroupsStatus(false, rpg_run);
        }

        private void OnConvertCaseFinished(Object sender, TaskEventArgs e)
        {
            
            if(e.Result)
            {
                LogLine("建立求解环境成功");
                runner.AsyncLocalSetFields(RunDirName);
            }
            else
            {
                ErrorLine("建立求解环境失败,错误信息：" + e.Message);
            }
            UnBusy();
        }

        private void OnOpenCaseStart(Object sender, TaskEventArgs e)
        {
            Busy("正在载入工程文件", "请稍后...");
            LogLine("开始载入工程文件,文件名:" + e.Objects["CaseFilePath"]);
            propertyGrid_main.Enabled = false;
            SetFileOperationStatus(false);
            SetRibbonPageGroupsStatus(false, rpg_run);
        }

        private void OnOpenCaseFinished(Object sender, TaskEventArgs e)
        {

            if (e.Result)
            {
                currentCase = caseReader.CaseObject;
                UpdateCaseObject();
                currentMeshFileName = caseReader.VxtFile;
                propertyGrid_main.Enabled = true;
                SetFileOperationStatus(true);
                SetRibbonPageGroupsStatus(true, rpg_run);
                LogLine("载入工程文件成功");
                if (FoamMeshVerifier.VerifyVxtPath(caseReader.VxtFile))
                {
                    FillBoundaryTreeNode();
                    LogLine("验证网格成功");
                    foamViewer_main.SetMeshFileName(currentMeshFileName);
                }
                else
                {
                    MessageBox.Show("非法网格", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLine("非法网格,工程文件:" + e.Objects["CaseFilePath"]);
                    currentMeshFileName = "";
                }
            }
            else
            {
                ErrorLine("建立求解环境失败，错误信息：" + e.Message);
            }
            UnBusy();
        }

        private void OnWriteCaseStart(Object sender, TaskEventArgs e)
        {
            Busy("正在保存工程文件", "请稍后...");
            LogLine("开始保存工程文件,文件名:" + e.Objects["SaveFilePath"]);
            propertyGrid_main.Enabled = false;
            SetFileOperationStatus(false);
            SetRibbonPageGroupsStatus(false, rpg_run);
        }

        private void OnWriteCaseFinished(Object sender, TaskEventArgs e)
        {
            if(e.Result)
            {
                propertyGrid_main.Enabled = true;
                SetFileOperationStatus(true);
                SetRibbonPageGroupsStatus(true, rpg_run);
                CaseModified = false;
                LogLine("工程文件保存成功,文件名：" + e.Objects["SaveFilePath"]);
            }
            else
            {
                ErrorLine("工程文件保存失败,错误信息:" + e.Message);
            }
            UnBusy();
            
        }

        private void OnReadMeshStart(Object sender, EventArgs e)
        {
            Busy("正在载入网格", "请稍后...");
            LogLine("正在载入网格,文件名:" + currentMeshFileName);
            propertyGrid_main.Enabled = false;
            SetFileOperationStatus(false);
            SetRibbonPageGroupsStatus(false, rpg_run);
        }

        private void OnReadMeshFinished(Object sender, EventArgs e)
        {
            LogLine("网格载入完毕");
            UnBusy();
            propertyGrid_main.Enabled = true;
            SetFileOperationStatus(true);
            SetRibbonPageGroupsStatus(true, rpg_run);
        }

        private void UpdateCaseObject()
        {
            foreach(Boundary b in currentCase.Field.Boundarys)
            {
                b.onTypeChange = onBoundaryTypeChange;
                if(b is Wall)
                {
                    Wall w = (Wall)b;
                    w.onHeatTypeChange = onWallHeatTypeChange;
                }
                if(b is Plug)
                {
                    Plug p = (Plug)b;
                    p.onPlugTypeChange = onPlugTypeChange;
                }
            }
            propertyGrid_main.SelectedObject = currentCase;
            if (currentTreeNode != null)
            {
                treeView_main.SelectedNode = currentTreeNode;
                TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(currentTreeNode, MouseButtons.Left, 1, 0, 0);
                OnTreeViewItemClick(this, args);
            }
            propertyGrid_main.ExpandAllGridItems();
            treeView_main.ExpandAll();
        }

        private void OnTreeViewItemClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "Case":
                    propertyGrid_main.SelectedObject = currentCase;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Geometry":
                    propertyGrid_main.SelectedObject = currentCase.Geometry;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Phase":
                    propertyGrid_main.SelectedObject = currentCase.Phases;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Gas":
                    propertyGrid_main.SelectedObject = currentCase.Phases.Gas;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Steel":
                    propertyGrid_main.SelectedObject = currentCase.Phases.Steel;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Interfacial":
                    propertyGrid_main.SelectedObject = currentCase.PhaseInterfacial;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Field":
                    propertyGrid_main.SelectedObject = currentCase.Field;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Mixing":
                    propertyGrid_main.SelectedObject = currentCase.Mixing;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Boundary":
                    propertyGrid_main.SelectedObject = currentCase.Field.Boundarys;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Internal Field":
                    propertyGrid_main.SelectedObject = currentCase.Field.InternalField;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "GasInternalField":
                    propertyGrid_main.SelectedObject = currentCase.Field.InternalField.Gas;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "SteelInternalField":
                    propertyGrid_main.SelectedObject = currentCase.Field.InternalField.Steel;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "SteelInitlizePoint1":
                    propertyGrid_main.SelectedObject = currentCase.Field.InternalField.SteelInitlizePoint1;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "SteelInitlizePoint2":
                    propertyGrid_main.SelectedObject = currentCase.Field.InternalField.SteelInitlizePoint2;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Operation Condition":
                    propertyGrid_main.SelectedObject = currentCase.OperationCondition;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Pimple":
                    propertyGrid_main.SelectedObject = currentCase.Pimple;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Control":
                    propertyGrid_main.SelectedObject = currentCase.Control;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                case "Other":
                    propertyGrid_main.SelectedObject = currentCase.OtherOptions;
                    propertyGrid_main.ExpandAllGridItems();
                    break;
                default:
                    if(e.Node.Parent == treeNode_boundary)
                        OnTreeViewBoundaryItemClick(e.Node.Name);
                    break;
            }
            if (e.Node.Parent == treeNode_boundary)
                foamViewer_main.SetHighLightPatch(new List<string>() {e.Node.Name });
            else
                foamViewer_main.SetHighLightPatch(new List<string>());

        }

        private void OnTreeViewBoundaryItemClick(string patchName)
        {
            Predicate<Boundary> pd = new Predicate<Boundary>( b => {
                if (b.PatchName == patchName)
                    return true;
                else
                    return false;
            });
            Boundary cb = currentCase.Field.Boundarys.Find(pd);
            propertyGrid_main.SelectedObject = cb;
            propertyGrid_main.ExpandAllGridItems();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bu_run_initlize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RunDirName == "")
                return;
            if (Directory.Exists(RunDirName))
            {
                if (MessageBox.Show("求解目录存在, 是否覆盖?", "question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        Directory.Delete(RunDirName, true);
                    }
                    catch(System.IO.IOException ioe)
                    {
                       
                    }
                }
                else
                    return;
            }
            Directory.CreateDirectory(RunDirName);
            caseConvertor.AsyncConvertCase(currentCaseFileName, foamConfig.TempleteFiles[0].Value, RunDirName, this);

        }

        private string RunDirName
        {
            get
            {
                if (currentCaseFileName == "")
                    return "";
                string parentDirName = Path.GetDirectoryName(currentCaseFileName);
                string caseName = Path.GetFileNameWithoutExtension(currentCaseFileName);
                string runDirName = Path.Combine(parentDirName, caseName + ".run");
                return runDirName;
            }
        }

        private void bu_run_decomposepar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RunDirName == "")
                return;
            if (!runner.IsRunning)
            {
                SetFileOperationStatus(false);
                SetRibbonButtonsStatus(false, bu_run_initlize, bu_run_reconstruct, bu_run_run);
                propertyGrid_main.Enabled = false;
                bu_run_decomposepar.ImageOptions.Image = iml_main.Images["stop"];
                runner.AsyncLocalDecomposePar(RunDirName);
            }
            else
            {
                DialogResult r = MessageBox.Show("强行停止求解器会导致不可预料的错误，是否继续?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    runner.Abort();
                    DisableBars(bu_run_decomposepar);
                }
            }
        }

        private TabPage NewResTabPage(string name)
        {
            TabPage tp = new TabPage(name);
            ResidualErrorChart chart = new ResidualErrorChart();
            chart.Name = "Chart";
            chart.Dock = DockStyle.Fill;
            tp.Controls.Add(chart);
            return tp;
        }

        private TabPage NewMonitorPage(string name)
        {
            TabPage tp = new TabPage(name);
            MonitorChart chart = new MonitorChart();
            chart.Name = "Chart";
            chart.Dock = DockStyle.Fill;
            tp.Controls.Add(chart);
            return tp;
        }

        private void bu_run_run_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckModified();
            if (RunDirName == "")
                return;
            if (!runner.IsRunning)
            {
                SetFileOperationStatus(false);
                SetRibbonButtonsStatus(false, bu_run_initlize, bu_run_reconstruct, bu_run_decomposepar);
                propertyGrid_main.Enabled = false;
                bu_run_run.ImageOptions.Image = iml_main.Images["stop"];
                List<TabPage> tps = new List<TabPage>();
                for(int i=1;i<tabControl_main.TabPages.Count;i++)
                {
                    tps.Add(tabControl_main.TabPages[i]);
                }
                foreach(var ctp in tps)
                {
                    tabControl_main.TabPages.Remove(ctp);
                }
                TabPage tp = NewResTabPage(RunDirName);
                tabControl_main.TabPages.Add(tp);
                tabControl_main.SelectedTab = tp;
                tabControl_Log.SelectedIndex = 1;
                currentChart = (ResidualErrorChart)tp.Controls["Chart"];
                runner.AsyncLocalSolve("ladleFoam.exe", RunDirName, currentCase.Control.NParallel);
                if (currentChart != null)
                {
                    currentChart.AsyncMonite(runner.LogFileName, 5000);
                }
                foreach(var m in currentCase.Control.Monitors)
                {
                    var dir = Path.Combine(RunDirName, "postProcessing", m.Name, "0");
                    var mtp = NewMonitorPage(m.Name);
                    tabControl_main.TabPages.Add(mtp);
                    var mc = (MonitorChart)mtp.Controls["Chart"];
                    monitorCharts.Add(mc);
                    mc.AsyncMonite(dir, 5000);
                }
            }
            else
            {
                DialogResult r = MessageBox.Show("强行停止求解器会导致不可预料的错误，是否继续?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(r == DialogResult.Yes)
                {
                    runner.Abort();
                    DisableBars(bu_run_run);
                }
            }
        }

        private void bu_run_reconstruct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckModified();
            if (RunDirName == "")
                return;
            if (!runner.IsRunning)
            {
                SetFileOperationStatus(false);
                SetRibbonButtonsStatus(false, bu_run_initlize, bu_run_run, bu_run_decomposepar);
                propertyGrid_main.Enabled = false;
                bu_run_reconstruct.ImageOptions.Image = iml_main.Images["stop"];
                runner.AsyncLocalReconstructPar(RunDirName);
            }
            else
            {
                DialogResult r = MessageBox.Show("强行停止求解器会导致不可预料的错误，是否继续?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    runner.Abort();
                    DisableBars(bu_run_reconstruct);
                }
            }
            
        }

        private void OnCaseModified(object s, PropertyValueChangedEventArgs e)
        {
            CaseModified = true;
        }

        private void OnRunnerOutput(object sender, DataReceivedEventArgs args)
        {
            if(!string.IsNullOrEmpty(args.Data))
            {
                FoamLog(args.Data);
            }
                
        }

        private void OnRunnerError(object sender, DataReceivedEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.Data))
                FoamError(args.Data);
        }

        private void OnRunnerExit(object sender, EventArgs args)
        {
            LogLine(runner.Process.StartInfo.FileName + " " + runner.Process.StartInfo.Arguments + " finished");
            SetFileOperationStatus(true);
            SetFileOperationStatus(true);
            SetRibbonPageGroupsStatus(true, rpg_run);
            SetRibbonButtonsStatus(true, bu_run_initlize, bu_run_reconstruct, bu_run_run, bu_run_decomposepar);
            SafeSetControlStatus(propertyGrid_main, true);
            bu_run_run.ImageOptions.Image = iml_main.Images["run"];
            bu_run_initlize.ImageOptions.Image = iml_main.Images["initlize"];
            bu_run_decomposepar.ImageOptions.Image = iml_main.Images["decompose"];
            bu_run_reconstruct.ImageOptions.Image = iml_main.Images["reconstruct"];
            if(currentChart!=null)
            {
                currentChart.StopMonite();
                currentChart = null;
            }
            foreach(var mc in monitorCharts)
            {
                mc.StopMonite();
            }
            monitorCharts.Clear();
        }

        private void DisableBars(params BarButtonItem[] controls)
        {
            foreach (BarButtonItem c in controls)
                c.Enabled = false;
        } 

        private void EnableBars(params BarButtonItem[] controls)
        {
            foreach (BarButtonItem c in controls)
                c.Enabled = true;
        }

        private void DisableControls(params Control[] controls)
        {
            foreach (Control c in controls)
                c.Enabled = false;
        }

        private void EnableControls(params Control[] controls)
        {
            foreach (Control c in controls)
                c.Enabled = true;
        }

        private void SetRibbonButtonsStatus(bool status, params BarButtonItem[] buttons)
        {
            foreach (var b in buttons)
                b.Enabled = status;
        }

        private void SetRibbonPageGroupsStatus(bool status, params RibbonPageGroup[] groups)
        {
            foreach (var g in groups)
                g.Enabled = status;
        }

        private void SetFileOperationStatus(bool status)
        {
            SetRibbonPageGroupsStatus(status, rpg_case, rpg_mesh);
        }

        private delegate void DgSetControlStatus(Control control, bool status);

        private void SafeSetControlStatus(Control control,bool status)
        {
            if (control.InvokeRequired)
            {
                var dg = new DgSetControlStatus(SafeSetControlStatus);
                control.Invoke(dg, new object[] { control, status });
            }
            else
                control.Enabled = status;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            commonLogWriter.Close();
            commonLogWriter = null;
            base.OnFormClosed(e);
        }

        private void OnPatchSelected(object o, FoamViewer.SelectPatchEventArgs ea)
        {
            string name = ea.PatchName;
            foreach(TreeNode n in treeNode_boundary.Nodes)
            {
                if (n.Name == name)
                {
                    treeView_main.SelectedNode = n;
                    OnTreeViewBoundaryItemClick(name);
                }  
            }
        }

        private void bu_mesh_generate_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bu_mesh_preview_ItemClick(object sender, ItemClickEventArgs e)
        {
            new OpenSCadWriter(currentCase.Geometry, foamConfig).Write(@"f:\1.stl");
            GeometryViewForm f = new GeometryViewForm(@"f:\1.stl");
            f.Show();
        }
    }
}
