using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.UI.Post.Controls;
using FoamLib.UI.Post.Model;
using Tplc.Model;
using FoamLib.IO;
using FoamLib.Util;
using System.IO;
using FoamLib.UI.Post.Display;
using FoamLib.Model;
using Fqh.CommonLib.WebLicenseComponent;
using TplcModelLib.Ui;
using System.Reflection;
using Fqh.CommonLib.LocalLicenseComonent;
using Fqh.CommonLib;

namespace TplcPost
{
    [LicenseProvider(typeof(LocalLicenseProvider))]
    public partial class MainWindow : Form, IMonitor
    {

        public delegate void DgSetText(string sv);
        public delegate void DgSetInt(int iv);
        public delegate void DgSetControlBool(Control c, bool st);
        private delegate void DgAppendRichBoxText(string txt, Color color);
        private delegate void DgAppendTextBoxText(string txt);
        private delegate void DgSetProgressValue(float v);
        private delegate void DgSetProgressStyle(ProgressBarStyle style);
        private delegate void DgOpenMesh(string vxt);
        private delegate void DgClearMesh();
        private delegate void DgTabPageOpenate(TabControl c, TabPage p, bool show);
        private delegate void DgTabPageSelect(TabControl c, TabPage p);
        private delegate void DgStop();

        private const string szStateFileFilter = "*.tsf|*.tsf";

        private bool stateModified = false;
        private bool isBusy = false;
        ToolStripItemManager tsimMain = null;
        TplcCaseReader caseReader = null;
        FoamTask<bool> readCaseTask = null;
        PostDesc currentDesc = null;
        EventHandler ehStop = null;
        LocalLicense license = null;
        
        public MainWindow(string fileName = "")
        {
            /*
            license = LicenseManager.Validate(typeof(MainWindow), this);
            if (!(license as WebLicense).LicenseResult.Result.IsOK)
            {
                MessageBox.Show(@"Unable to communicate with license server, please check your network", "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((license as WebLicense).LicenseResult.Data.state == LicenseState.NotRegisted)
            {
                //MessageBox.Show("Software is not registered!", "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new RegistForm().ShowDialog();
                return;
            }
            if ((license as WebLicense).LicenseResult.Data.state == LicenseState.Expired)
            {
                //MessageBox.Show("License is expired!", "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new RegistForm().ShowDialog();
                return;
            }
            */

            
            
            license = (LocalLicense)LicenseManager.Validate(typeof(MainWindow), this);
            if (license.LicenseData == null)
            {
                //MessageBox.Show("license data is null");
                new RegistForm("No license file found").ShowDialog();
                return;
            }
            
            /*
            if (license.LicenseData.UsedCount >= license.LicenseData.MaxCount)
            {
                MessageBox.Show("use count error " + license.LicenseData.MaxCount + " " + license.LicenseData.UsedCount);

                new RegistForm().ShowDialog();
                return;
            }
            */
            
            if (license.LicenseData.HardwareId != LocalInformation.HardwareId)
            {
                //MessageBox.Show("hardwareid is error");
                new RegistForm("The license file is not for this device").ShowDialog();
                return;
            }
            DateTime now = LocalLicenseUtil.GetNetDateTime();
            if (now == new DateTime(0))
                now = DateTime.Now;
            if (now > license.LicenseData.ExpiredDate)
            {
                //MessageBox.Show("expired date error " + now + " " + license.LicenseData.ExpiredDate);
                new RegistForm("The license expires, and the final use period is " + license.LicenseData.ExpiredDate).ShowDialog();
                return;
            }
            
            GlobalParameters.FieldNames.Clear();
            GlobalParameters.FieldNames.AddRange(new string[] {"p","U","k","epsilon" });
            caseReader = new TplcCaseReader(this);
            readCaseTask = new FoamTask<bool>();
            readCaseTask.OnTaskFinished += OnReadCaseFinished;
            InitializeComponent();
            tsimMain = new ToolStripItemManager(ts_main);
            tsimMain.Add("case_open", new List<ToolStripItem>() { mi_opencase, tsb_opencase });
            tsimMain.Add("state_open", new List<ToolStripItem>() { mi_openstate, tsb_openstate });
            tsimMain.Add("state_saveas", new List<ToolStripItem>() { mi_savestateas, tsb_savestateas });
            tsimMain.Add("export", new List<ToolStripItem>() { mi_export, tsb_export });
            tsimMain.Add("field", new List<ToolStripItem>() { tscb_field });
            tsimMain.Add("range", new List<ToolStripItem>() { tstb_max, tstb_min });
            tsimMain.Add("stop", new List<ToolStripItem>() { tsb_stop });
            fdvMain.OnReadMeshFinished += OnLoadDataFinished;
            ptvMain.SelectedImageIndex = 0;
            ptvMain.IndexCutList = 3;
            ptvMain.IndexPatchList = 4;
            ptvMain.IndexPatch = 2;
            ptvMain.IndexCutter = 1;
            foreach(string f in GlobalParameters.FieldNames)
            {
                tscb_field.Items.Add(f);
            }
            tscb_field.SelectedIndex = 0;
            StateModified = false;
            if (fileName != "")
            {
                if (String.Compare(Path.GetExtension(fileName), ".tcs", true) == 0)
                {
                    currentDesc = new PostDesc();
                    currentDesc.CaseFileName = fileName;
                    ptvMain.PostDesc = currentDesc;
                    ReadCase(fileName);
                    StateModified = true;
                }
                else if (String.Compare(Path.GetExtension(fileName), ".tsf", true) == 0)
                {
                    StateModified = false;
                    OpenStateFile(fileName);
                }
            }
        }
        private void SetControlEnable(Control c, bool st)
        {
            if (c.InvokeRequired)
            {
                DgSetControlBool dsc = new DgSetControlBool(SetControlEnable);
                c.Invoke(dsc, new object[] { c, st });
            }
            else
                c.Enabled = st;
        }
        private void EnableControls(params Control[] controls)
        {
            foreach (var c in controls)
            {
                SetControlEnable(c, true);
            }
        }
        
        private void DisableControls(params Control[] controls)
        {
            foreach (var c in controls)
            {
                SetControlEnable(c, false);
            }
        }
        public void Progress(float progress)
        {
            if (pb_main.ProgressBar.InvokeRequired)
            {
                DgSetProgressValue ds = new DgSetProgressValue(Progress);
                pb_main.ProgressBar.Invoke(ds, new object[] { progress });
            }
            else
                pb_main.Value = (int)(100.0f * progress);
        }
        public void ProgressStyle(ProgressBarStyle style)
        {
            if (pb_main.ProgressBar.InvokeRequired)
            {
                DgSetProgressStyle ds = new DgSetProgressStyle(ProgressStyle);
                pb_main.ProgressBar.Invoke(ds, new object[] { style });
            }
            else
                pb_main.ProgressBar.Style = style;

        }
        private void Busy(string message, Control[] disableContorls, string[] disableItems, EventHandler stopHandler = null)
        {
            isBusy = true;
            tssl_main.Text = message;
            tsimMain.Disable(disableItems);
            foreach (Control c in disableContorls)
            {
                SetControlEnable(c, false);
            }
            ProgressStyle(ProgressBarStyle.Marquee);
            if (stopHandler != null)
            {
                tsimMain.Show("stop");
                if (ehStop != null)
                {
                    tsb_stop.ButtonClick -= ehStop;
                }
                tsb_stop.ButtonClick += stopHandler;
                ehStop = stopHandler;
            }
            else
            {
                tsimMain.Hide("stop");
            }
        }
        private void UnBusy(Control[] enableContorls, string[] enableItems)
        {
            tssl_main.Text = "Ready";
            tsimMain.Enable(enableItems);
            foreach (Control c in enableContorls)
            {
                SetControlEnable(c, true);
            }
            ProgressStyle(ProgressBarStyle.Continuous);
            tsimMain.Hide("stop");
            isBusy = false;
        }
        private void OnPostTreeViewItemSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node is UnitTreeNode)
            {
                UnitTreeNode utn = e.Node as UnitTreeNode;
                pgMain.SelectedObject = utn.Property;
                //fdvMain.Render();
            } 
            else
            {
                pgMain.SelectedObject = null;
               //fdvMain.Render();
            }
            if(e.Node.Text == "Pressure Drop")
            {
                PressureDropProperty pdp = new PressureDropProperty();
                pdp.UnitManager = fdvMain.UnitManager;
                pgMain.SelectedObject = pdp;
            }
                
        }

        private void OnUnitPropertyChanged(object sender, EventArgs e)
        {
            StateModified = true;
            fdvMain.UnitManager.Render();
        }
        private void ReadCase(string cas)
        {
            Busy("Reading", new Control[] { pgMain ,ptvMain}, new string[] { "case_open","state_open","state_saveas","export","field","range"});
            readCaseTask.Do(() => caseReader.Read(cas));
        }
        private void OnReadCaseFinished(object sender,TaskEventArgs<bool> e)
        {
            UnBusy(new Control[] { pgMain ,ptvMain}, new string[] { "case_open", "state_open", "state_saveas", "export", "field", "range" });
            if (e.Result)
            {
                string postfile = Path.Combine(caseReader.CaseRunRoot, "post.foam");
                if (!File.Exists(postfile))
                {
                    File.Create(postfile).Close();
                }
                Busy("Rendering", new Control[] { pgMain , ptvMain }, new string[] { "case_open", "state_open", "state_saveas", "export", "field", "range" });
                fdvMain.UnitManager.PressureScaleCoefficient = caseReader.CaseDict.MaterialProperty.Density;
                fdvMain.SetMeshFileName(postfile);
            }
        }
        private void OnLoadDataFinished(object sender, EventArgs e)
        {
            if (currentDesc.PatchProperty.Count>0)
            {
                foreach(DatasetUnitProperty unitProperty in currentDesc.PatchProperty)
                {
                    unitProperty.Unit = fdvMain.UnitManager.FindUnitWithName(unitProperty.Name);
                    unitProperty.OnPropertyChanged -= OnUnitPropertyChanged;
                    unitProperty.OnPropertyChanged += OnUnitPropertyChanged;
                    unitProperty.FieldName = CurrentFieldName;
                }
            }
            else
            {
                foreach(PatchUnit patchUnit in fdvMain.UnitManager.GetAllPatchUnits())
                {
                    DatasetUnitProperty unitProperty = new DatasetUnitProperty(patchUnit);
                    unitProperty.OnPropertyChanged += OnUnitPropertyChanged;
                    unitProperty.FieldName = CurrentFieldName;
                    currentDesc.PatchProperty.Add(unitProperty);
                }
            }
            if(currentDesc.CutUnitProperty.Count>0)
            {
                foreach(CutterUnitProprety cutterUnitProperty in currentDesc.CutUnitProperty)
                {
                    CutUnit cu = new CutUnit(cutterUnitProperty.Name, fdvMain.UnitManager.GetInternalMeshUnit(),fdvMain.UnitManager.RenderWindow.GetInteractor());
                    /*
                    cu.FieldName = cutterUnitProperty.FieldName;
                    cu.ScalarRange = new double[2] { cutterUnitProperty.Min, cutterUnitProperty.Max };
                    cu.Origin = cutterUnitProperty.Origin.ToBlock();
                    cu.Normal = cutterUnitProperty.Normal.ToBlock();
                    */
                    
                    cutterUnitProperty.Unit = cu;
                    cutterUnitProperty.OnPropertyChanged -= OnUnitPropertyChanged;
                    cutterUnitProperty.OnPropertyChanged += OnUnitPropertyChanged;
                    cutterUnitProperty.FieldName = CurrentFieldName;
                    cutterUnitProperty.OnPlaneWidgetParameterChanged += OnCutUnitPlaneWidgetParametersChanged;
                    cu.UpdateCut();
                    fdvMain.UnitManager.AddCutUnit(cu);
                    fdvMain.Render();
                }
            }
            ptvMain.PostDesc = currentDesc;
            if (currentDesc.PatchProperty.Count > 0)
            {
                fdvMain.UnitManager.ActiveUnitName = currentDesc.PatchProperty[0].Name;
                fdvMain.UnitManager.ScalarBarOn = true;
            } 
            else
            {
                fdvMain.UnitManager.ActiveUnitName = "";
                fdvMain.UnitManager.ScalarBarOn = false;
            }
            ResetRange();
            UnBusy(new Control[] { pgMain ,ptvMain}, new string[] { "case_open", "state_open", "state_saveas", "export", "field", "range" });
        }
        private void mi_opencase_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.CheckFileExists = true;
            d.Filter = " *.tcs | *.tcs";
            if(d.ShowDialog() == DialogResult.OK)
            {
                currentDesc = new PostDesc();
                currentDesc.CaseFileName = d.FileName;
                ptvMain.PostDesc = currentDesc;
                ReadCase(d.FileName);
                StateModified = true;
            }
        }

        private void mi_openstate_Click(object sender, EventArgs e)
        {
            CheckStateModify();
            OpenFileDialog d = new OpenFileDialog();
            d.CheckFileExists = true;
            d.Filter = szStateFileFilter;
            if (d.ShowDialog() == DialogResult.OK)
            {
                StateModified = false;
                OpenStateFile(d.FileName);
            }
        }

        private bool OpenStateFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show("File not exist : " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            BinSerializerTool bs = new BinSerializerTool();
            currentDesc = bs.FromFile(fileName) as PostDesc;
            if(currentDesc == null)
            {
                MessageBox.Show("File format error : " + bs.LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!File.Exists(currentDesc.CaseFileName))
            {
                MessageBox.Show("Case file not exist : " + currentDesc.CaseFileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentDesc = null;
                return false;
            }
            ptvMain.PostDesc = currentDesc;
            ReadCase(currentDesc.CaseFileName);
            return true;
        }

        private void mi_savestateas_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = szStateFileFilter;
            if(d.ShowDialog()== DialogResult.OK)
            {
                SaveStateFile(d.FileName);
                StateModified = false;
            }
        }

        private bool SaveStateFile(string fileName)
        {
            BinSerializerTool bs = new BinSerializerTool();
            if (!bs.ToFile(currentDesc, fileName))
            {
                MessageBox.Show("Save state file error, message : " + bs.LastError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }
        private void mi_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ErrorLine(string err)
        {
            MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LogLine(string log)
        {
            throw new NotImplementedException();
        }

        private string CurrentFieldName
        {
            get
            {
                return tscb_field.SelectedItem.ToString();
            }
        }

        public bool StateModified
        {
            get => stateModified;
            set
            {
                stateModified = value;
                tsimMain.Set("state_saveas", stateModified ? 0 : -1);
            }
        }

        private void OnFieldNameSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (UnitTreeNode utn in ptvMain.NodePatchList.Nodes)
            {
                utn.Property.FieldName = CurrentFieldName;
            }
            foreach(UnitTreeNode utn in ptvMain.NodeCutList.Nodes)
            {
                utn.Property.FieldName = CurrentFieldName;
            }
            ResetRange();
        }
        private void ResetRange()
        {
            double[] range = GetVisible2dUnitInputRange();
            tstb_min.Text = range[0].ToString();
            tstb_max.Text = range[1].ToString();
        }
        private double[] GetVisible2dUnitInputRange()
        {
            double max = double.MinValue;
            double min = double.MaxValue;
            foreach (UnitTreeNode utn in ptvMain.NodePatchList.Nodes)
            {
                if (utn.Property.Visible)
                {
                    double[] range = utn.Property.Unit.GetInputScalarRange();
                    min = range[0] < min ? range[0] : min;
                    max = range[1] > max ? range[1] : max;
                }
            }
            foreach (UnitTreeNode utn in ptvMain.NodeCutList.Nodes)
            {
                if (utn.Property.Visible)
                {
                    double[] range = utn.Property.Unit.GetInputScalarRange();
                    min = range[0] < min ? range[0] : min;
                    max = range[1] > max ? range[1] : max;
                }
            }
            if (max < min)
                max = min = 0.0;
            return new double[]{ min, max};
        }
        private void SetVisible2dUnitRange(double[] range)
        {
            double min = range[0];
            double max = range[1];
            foreach (UnitTreeNode utn in ptvMain.NodePatchList.Nodes)
            {
                if (utn.Property.Visible)
                {
                    utn.Property.Max = max;
                    utn.Property.Min = min;
                }
            }
            foreach (UnitTreeNode utn in ptvMain.NodeCutList.Nodes)
            {
                if (utn.Property.Visible)
                {
                    utn.Property.Max = max;
                    utn.Property.Min = min;
                }
            }
        }

        private void OnRangeChanged(object sender, EventArgs e)
        {
            if (ptvMain.NodePatchList.Nodes.Count == 0 && ptvMain.NodeCutList.Nodes.Count == 0)
                return;
            double min, max;
            if (!double.TryParse(tstb_min.Text,out min))
            {
                MessageBox.Show("Min must be a float value!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(tstb_max.Text, out max))
            {
                MessageBox.Show("Max must be a float value!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(max < min)
            {
                MessageBox.Show("Min > Max!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (UnitTreeNode utn in ptvMain.NodePatchList.Nodes)
            {
                if (utn.Property.Visible)
                {
                    utn.Property.Max = max;
                    utn.Property.Min = min;
                }
            }
            foreach (UnitTreeNode utn in ptvMain.NodeCutList.Nodes)
            {
                if (utn.Property.Visible)
                {
                    utn.Property.Max = max;
                    utn.Property.Min = min;
                }
            }
        }

        private void tsb_resetrange_Click(object sender, EventArgs e)
        {
            ResetRange();
        }

        private void mi_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.AddExtension = true;
            d.DefaultExt = ".png";
            d.Filter = "*.png|*.png";
            if(d.ShowDialog()== DialogResult.OK)
            {
                fdvMain.SavePngImage(d.FileName);
            }
        }

        private void mi_newcut_Click(object sender, EventArgs e)
        {
            CutUnit cu = fdvMain.UnitManager.NewCutUnit();
            CutterUnitProprety cup = new CutterUnitProprety(cu);
            cup.OnPropertyChanged += this.OnUnitPropertyChanged;
            cup.OnPlaneWidgetParameterChanged += OnCutUnitPlaneWidgetParametersChanged;
            cup.FieldName = CurrentFieldName;
            cup.Min = double.Parse(tstb_min.Text);
            cup.Max = double.Parse(tstb_max.Text);
            ptvMain.NodeCutList.Nodes.Add(new UnitTreeNode(cup, 2));
            currentDesc.CutUnitProperty.Add(cup);
            StateModified = true;
        }

        private void mi_removecut_Click(object sender, EventArgs e)
        {
            TreeNode node = ptvMain.SelectedNode;
            if(node != null)
                if(node.Parent == ptvMain.NodeCutList)
                {
                    UnitTreeNode unitNode = node as UnitTreeNode;
                    CutterUnitProprety cup = unitNode.Property as CutterUnitProprety;
                    currentDesc.CutUnitProperty.Remove(cup);
                    ptvMain.NodeCutList.Nodes.Remove(node);
                    if (ptvMain.NodePatchList.Nodes.Count > 0)
                        ptvMain.SelectedNode = ptvMain.NodePatchList.Nodes[0];
                    else
                        ptvMain.SelectedNode = null;
                    fdvMain.UnitManager.Remove(cup.Unit.Name);
                    fdvMain.Render();
                    StateModified = true;
                }
        }
        private void OnCutUnitPlaneWidgetParametersChanged(object sender, EventArgs e)
        {
            pgMain.Refresh();
        }
        private void OnTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if (e.Node is UnitTreeNode)
                {
                    UnitTreeNode un = e.Node as UnitTreeNode;
                    mi_showunit.Visible = !un.Property.Visible;
                    mi_hideunit.Visible = un.Property.Visible;
                    mi_updatecut.Visible = (e.Node.Parent == ptvMain.NodeCutList);
                }
                else
                {
                    mi_showunit.Visible = false;
                    mi_hideunit.Visible = false;
                    mi_updatecut.Visible = false;
                }


                if (ptvMain.NodePatchList.Nodes.Count==0)
                {
                    mi_newcut.Visible = false;
                    tsb_newcut.Enabled = false;
                    mi_removecut.Visible = false;
                    tsb_removecut.Enabled = false;
                }
                else
                {
                    if (e.Node == ptvMain.NodeCutList)
                    {
                        mi_newcut.Visible = true;
                        tsb_newcut.Enabled = true;
                        mi_removecut.Visible = false;
                        tsb_removecut.Enabled = false;
                    }
                    else if (e.Node.Parent == ptvMain.NodeCutList)
                    {
                        mi_newcut.Visible = false;
                        tsb_newcut.Enabled = false;
                        mi_removecut.Visible = true;
                        tsb_removecut.Enabled = true;
                    }
                    else
                    {
                        mi_newcut.Visible = false;
                        tsb_newcut.Enabled = false;
                        mi_removecut.Visible = false;
                        tsb_removecut.Enabled = false;
                    }
                }
            }
        }

        private void mi_showunit_Click(object sender, EventArgs e)
        {
            UnitTreeNode un = ptvMain.SelectedNode as UnitTreeNode;
            if (un != null)
                un.Property.Visible = true;
        }

        private void mi_hideunit_Click(object sender, EventArgs e)
        {
            UnitTreeNode un = ptvMain.SelectedNode as UnitTreeNode;
            if(un!=null)
                un.Property.Visible = false;
        }

        private void mi_updatecut_Click(object sender, EventArgs e)
        {
            UnitTreeNode un = ptvMain.SelectedNode as UnitTreeNode;
            if (un != null)
            {
                CutUnit cu = un.Property.Unit as CutUnit;
                cu.UpdateCut();
            }
            fdvMain.Render();
        }

        private void OnPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if(ptvMain.SelectedNode.Text != "Pressure Drop")
            {
                StateModified = true;
            }
        }

        private bool CheckStateModify()
        {
            if (StateModified)
            {
                if (MessageBox.Show("State is modified, Save it ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mi_savestateas_Click(this, new EventArgs());
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if(isBusy)
            {
                MessageBox.Show("Application is working!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            e.Cancel = CheckStateModify();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AboutForm af = new AboutForm("TPLC-Post",
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                "Copyright 2020 NETKIDXP.CN",
                license.LicenseData.ExpiredDate);
            af.ShowDialog();
        }
    }
}
