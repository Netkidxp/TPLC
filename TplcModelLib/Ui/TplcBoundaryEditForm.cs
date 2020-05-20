using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tplc.Model.Boundary;

namespace Tplc.UI
{
    public partial class TplcBoundaryEditForm : Form
    {
        private List<BoundaryBase> patchBoundarys = new List<BoundaryBase>();
        private List<string> patchNames = new List<string>();
        private TreeNode currentNode = null;

        public TplcBoundaryEditForm()
        {
            InitializeComponent();
        }

        public List<BoundaryBase> PathchBoundarys {
            get => patchBoundarys;
            set
            {
                patchBoundarys = value;
                UpdateTreeView();
            }
        }
        public List<string> PatchNames {
            set
            {
                patchNames = value;
                UpdateTreeView();
            }
        } 
        private void UpdateTreeView()
        {
            tv_patch.Nodes.Clear();
            foreach(string patchName in patchNames)
            {
                TreeNode patchNode = new TreeNode(patchName);
                patchNode.Name = "patch";
                patchNode.Text = patchName;

                BoundaryBase boundary = GetBoundaryByPatchName(patchName);
                if(boundary !=null)
                {
                    TreeNode boundaryNode = new TreeNode(boundary.ToString());
                    boundaryNode.Name = "boundary";
                    boundaryNode.Text = boundary.ToString();
                    patchNode.Nodes.Add(boundaryNode);
                }
                tv_patch.Nodes.Add(patchNode);
            }
            tv_patch.ExpandAll();
        }
        private BoundaryBase GetBoundaryByPatchName(string patchName)
        {
            BoundaryBase rb = null;
            foreach (BoundaryBase b in patchBoundarys)
            {
                if (b.PatchName == patchName)
                    rb = b;
            }
            return rb;
        }

        private void OnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentNode = e.Node;
            pg_patch.SelectedObject = GetBoundaryByPatchName(SelectedPatchName);
            if(e.Button == MouseButtons.Right)
            {
                cms_main.Show(this,e.Location);
            }
        }

        public string SelectedPatchName
        {
            get
            {
                string patchName = "";
                TreeNode node = currentNode;
                if(node!=null)
                {
                    if (node.Name == "patch")
                    {
                        patchName = node.Text;
                    }
                    else if (node.Name == "boundary")
                    {
                        patchName = node.Parent.Text;
                    }
                }
                return patchName;
            }
        }

        private void mi_toInlet_Click(object sender, EventArgs e)
        {
            if (SelectedPatchName == null)
                return;
            Inlet inlet = new Inlet(SelectedPatchName, 1.0f, 0.01f, 0.1f);
            BoundaryBase b = GetBoundaryByPatchName(SelectedPatchName);
            if (b != null)
                patchBoundarys.Remove(b);
            patchBoundarys.Add(inlet);
            pg_patch.SelectedObject = inlet;
            string oldName = SelectedPatchName;
            UpdateTreeView();
            SelectNode(oldName);
        }

        private void mi_toOutlet_Click(object sender, EventArgs e)
        {
            if (SelectedPatchName == null)
                return;
            Outlet outlet = new Outlet(SelectedPatchName);
            BoundaryBase b = GetBoundaryByPatchName(SelectedPatchName);
            if (b != null)
                patchBoundarys.Remove(b);
            patchBoundarys.Add(outlet);
            pg_patch.SelectedObject = outlet;
            string oldName = SelectedPatchName;
            UpdateTreeView();
            SelectNode(oldName);
        }

        private void mi_toWall_Click(object sender, EventArgs e)
        {
            if (SelectedPatchName == null)
                return;
            Wall wall = new Wall(SelectedPatchName);
            BoundaryBase b = GetBoundaryByPatchName(SelectedPatchName);
            if (b != null)
                patchBoundarys.Remove(b);
            patchBoundarys.Add(wall);
            pg_patch.SelectedObject = wall;
            string oldName = SelectedPatchName;
            UpdateTreeView();
            SelectNode(oldName);
        }

        private TreeNode GetTreeNodeByPatchName(string patchName)
        {
            TreeNode node = null;
            foreach(TreeNode n in tv_patch.Nodes)
            {
                if (n.Text == patchName)
                    node = n;
            }
            return node;
        }
        private void SelectNode(string patchName)
        {
            TreeNode node = GetTreeNodeByPatchName(patchName);
            if (node != null)
                tv_patch.SelectedNode = node;
        }
    }
}
