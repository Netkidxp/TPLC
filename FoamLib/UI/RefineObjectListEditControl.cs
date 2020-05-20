using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.Model.CfMesh;
using static FoamLib.Model.CfMesh.CfMeshDict;

namespace FoamLib.UI
{
    public partial class RefineObjectListEditControl : UserControl
    {
        List<ObjectRefinementSubDict> refineObjects = null;
        public RefineObjectListEditControl(List<ObjectRefinementSubDict> refineObjects)
        {
            InitializeComponent();
            this.refineObjects = refineObjects;
            tvMain.Nodes.Clear();
            if (refineObjects == null)
                return;
            foreach (ObjectRefinementSubDict ag in refineObjects)
            {
                TreeNode node = new TreeNode(ag.Geometry.Name);
                tvMain.Nodes.Add(node);
            }
        }
        
        private ObjectRefinementSubDict FindObjectByNode(TreeNode node)
        {
            foreach (ObjectRefinementSubDict ag in refineObjects)
            {
                if (node.Text == ag.Geometry.Name)
                    return ag;
                
            }
            return null;
        }
        private void OnCmsMainOpening(object sender, CancelEventArgs e)
        {
            mi_delete.Visible = tvMain.SelectedNode != null;
        }

        private void mi_newbox_Click(object sender, EventArgs e)
        {
            Box box = new Box();
            box.Name = "RefineObject.Box" + (refineObjects.Count + 1);
            ObjectRefinementSubDict o = new ObjectRefinementSubDict();
            o.Geometry = box;
            refineObjects.Add(o);
            TreeNode node = new TreeNode(box.Name);
            tvMain.Nodes.Add(node);
            tvMain.SelectedNode = node;
        }

        private void mi_newcone_Click(object sender, EventArgs e)
        {
            Cone cone = new Cone();
            cone.Name = "RefineObject.Cone" + (refineObjects.Count + 1);
            ObjectRefinementSubDict o = new ObjectRefinementSubDict();
            o.Geometry = cone;
            refineObjects.Add(o);
            TreeNode node = new TreeNode(cone.Name);
            tvMain.Nodes.Add(node);
            tvMain.SelectedNode = node;
        }

        private void mi_newsphere_Click(object sender, EventArgs e)
        {
            Sphere sphere = new Sphere();
            sphere.Name = "RefineObject.Sphere" + (refineObjects.Count + 1);
            ObjectRefinementSubDict o = new ObjectRefinementSubDict();
            o.Geometry = sphere;
            refineObjects.Add(o);
            TreeNode node = new TreeNode(sphere.Name);
            tvMain.Nodes.Add(node);
            tvMain.SelectedNode = node;

        }

        private void OnPropretyChanged(object s, PropertyValueChangedEventArgs e)
        {

        }

        private void OnItemSelected(object sender, TreeViewEventArgs e)
        {
            pgMain.SelectedObject = FindObjectByNode(e.Node);
            pgMain.ExpandAllGridItems();
        }

        private void mi_delete_Click(object sender, EventArgs e)
        {
            ObjectRefinementSubDict o = FindObjectByNode(tvMain.SelectedNode);
            refineObjects.Remove(o);
            tvMain.Nodes.Remove(tvMain.SelectedNode);
            if (tvMain.Nodes.Count > 0)
            {
                tvMain.SelectedNode = tvMain.Nodes[0];
            }
            else   
                pgMain.SelectedObject = null;
        }
    }
}
