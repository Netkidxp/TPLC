using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using FoamLib.Util;

namespace FoamLib.UI
{

    public partial class MeshTreeView : UserControl
    {
        private string fileName;
        private const string rsPatchName = @"^[A-Za-z]\w*$";
        public EventHandler<PatchNameChangeEventArgs> OnPatchNameChanged;
        public EventHandler<PatchNodeSelectEventArgs> OnPatchNodeSelected;
        private TreeNode tnRoot = new TreeNode("Patches", 0, 2);
        
        public MeshTreeView()
        {
            InitializeComponent();
            tv_main.Nodes.Add(tnRoot);
        }
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                if (File.Exists(fileName))
                {
                    FillMeshTreeView();
                }
            }
        }
        private delegate void DgFillMeshTreeView();

        private void FillMeshTreeView()
        {
            if(this.InvokeRequired)
            {
                DgFillMeshTreeView df = new DgFillMeshTreeView(FillMeshTreeView);
                this.Invoke(df, new object[] { });
            }
            else
            {
                PolyMesh mesh = new PolyMesh(fileName);
                List<string> patchNames = mesh.GetAllPatchNames();
                tnRoot.Nodes.Clear();
                foreach (string patchName in patchNames)
                {
                    tnRoot.Nodes.Add(patchName, patchName, 1);
                }
                tv_main.ExpandAll();
            }
        }

        public class PatchNameChangeEventArgs : EventArgs
        {
            public PatchNameChangeEventArgs(string oldName, string newName)
            {
                OldName = oldName;
                NewName = newName;
            }

            public string OldName { get; set; }
            public string NewName { get; set; }
        }
        public class PatchNodeSelectEventArgs : EventArgs
        {
            public PatchNodeSelectEventArgs(string patchName)
            {
                PatchName = patchName;
            }

            public string PatchName { get; set; }
        }
        private void OnNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Parent == tnRoot)
            {
                e.Node.BeginEdit();
            }
        }

        private void OnAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string newLabel = e.Label;
            if(newLabel == e.Node.Text || e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }
            if (!(Regex.Match(newLabel, rsPatchName).Success))
            {
                MessageBox.Show("Patch name must start with a letter and consist of letters, numbers, and underscores!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
                return;
            }
            foreach(TreeNode n in tnRoot.Nodes)
            {
                if (n == e.Node)
                    continue;
                if(newLabel == n.Text)
                {
                    MessageBox.Show("Patch name duplicate!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                    return;
                }
            }
            PatchNameChangeEventArgs arg = new PatchNameChangeEventArgs(e.Node.Text, newLabel);
            //PolyMesh mesh = new PolyMesh(fileName);
            //mesh.ChangePatchName(arg.OldName, arg.NewName);
            if (OnPatchNameChanged!=null)
                OnPatchNameChanged(this, arg);
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == tnRoot && OnPatchNodeSelected != null)
                OnPatchNodeSelected(this, new PatchNodeSelectEventArgs(e.Node.Text));
        }
    }
}
