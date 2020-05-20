using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.UI.Post.Model;
namespace FoamLib.UI.Post.Controls
{
    public class UnitTreeNode : TreeNode
    {
        private DatasetUnitProperty property = null;

        public UnitTreeNode()
        {

        }
        public UnitTreeNode(DatasetUnitProperty property,int imageindex = 0)
        {
            this.property = property;
            this.ImageIndex = imageindex;
            if(this.property!=null)
            {
                this.property.OnPropertyChanged += OnPropretyChanged;
                UpdateProprerty();
            }
                
        }
        public void OnPropretyChanged(object sender, EventArgs e)
        {
            UpdateProprerty();
        }
        public void UpdateProprerty()
        {
            this.Text = property.Name + "." + property.FieldName;
            this.Checked = property.Visible;
        }
        public DatasetUnitProperty Property
        {
            get => property;
            set
            {
                if (this.property != null)
                    this.property.OnPropertyChanged -= OnPropretyChanged;
                this.property = value;
                if (this.property != null)
                {
                    this.property.OnPropertyChanged += OnPropretyChanged;
                    UpdateProprerty();
                }
            }
        }
        
    }
    public class PostTreeView : TreeView
    {
        private PostDesc postDesc = null;
        //private UnitTreeNode nodeInternalMesh = new UnitTreeNode();
        private TreeNode nodePatchList = new TreeNode("Patchs");
        private TreeNode nodeCutList = new TreeNode("Cuts");
        private TreeNode nodePressureDrop = new TreeNode("Pressure Drop");
        private int indexCut = 0;
        private int indexPatch = 0;
        public PostTreeView()
        {
            //nodeInternalMesh.Text = "Internal Mesh";
            //this.Nodes.Add(nodeInternalMesh);
            this.Nodes.Add(nodePatchList);
            this.Nodes.Add(nodeCutList);
            this.Nodes.Add(nodePressureDrop);
        }
        public PostDesc PostDesc
        {
            get => postDesc;
            set
            {
                postDesc = value;
                SetPostDesc(postDesc);
            }
        }
        //public int IndexInternalMesh { get => NodeInternalMesh.ImageIndex; set => NodeInternalMesh.ImageIndex = value; }
        public int IndexPatchList { get => NodePatchList.ImageIndex; set => NodePatchList.ImageIndex = value; }
        public int IndexPatch
        {
            get
            {
                return indexPatch;
            }
            set
            {
                indexPatch = value;
                foreach (TreeNode n in NodePatchList.Nodes)
                {
                    n.ImageIndex = value;
                }
            }
        }
        public int IndexCutter
        {
            get
            {
                return indexCut;
            }
            set
            {
                indexCut = value;
                foreach (TreeNode n in NodeCutList.Nodes)
                {
                    n.ImageIndex = value;
                }
            }
        }
        public int IndexCutList { get => NodeCutList.ImageIndex; set => NodeCutList.ImageIndex = value; }
        //public UnitTreeNode NodeInternalMesh { get => nodeInternalMesh; }
        public TreeNode NodePatchList { get => nodePatchList; }
        public TreeNode NodeCutList { get => nodeCutList; }

        private void SetPostDesc(PostDesc desc)
        {
            this.Nodes.Clear();
            nodePatchList.Nodes.Clear();
            nodeCutList.Nodes.Clear();

            if (desc == null)
            {
                //nodeInternalMesh = new UnitTreeNode();
            }
            else
            {
                //nodeInternalMesh = new UnitTreeNode(desc.InternalMeshProperty);
                foreach (DatasetUnitProperty up in desc.PatchProperty)
                {
                    NodePatchList.Nodes.Add(new UnitTreeNode(up,IndexPatch));
                }
                foreach (CutterUnitProprety cup in desc.CutUnitProperty)
                {
                    NodeCutList.Nodes.Add(new UnitTreeNode(cup,IndexCutter));
                }
            }
            //this.Nodes.Add(nodeInternalMesh);
            this.Nodes.Add(nodePatchList);
            this.Nodes.Add(nodeCutList);
            this.Nodes.Add(nodePressureDrop);
        }
    }
}
