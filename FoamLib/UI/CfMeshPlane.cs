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
using FoamLib.Model;
using FoamLib.IO;
using FoamLib.Util;
using System.IO;

namespace FoamLib.UI
{
    public partial class CfMeshPlane : UserControl
    {

        public class GenerateMeshEventArgs : EventArgs
        {
            
            public GenerateMeshEventArgs()
            {
            }

            public GenerateMeshEventArgs(CfMeshDict dict)
            {
                Dict = dict;
            }

            public CfMeshDict Dict { get; set; }
        }

        public class SurfaceFilesSelectedEventArgs : EventArgs
        {
            public SurfaceFilesSelectedEventArgs()
            {
            }

            public SurfaceFilesSelectedEventArgs(List<string> surfaceFileNames)
            {
                SurfaceFileNames = surfaceFileNames;
            }

            public List<string> SurfaceFileNames { get; set; }
        }

        public event EventHandler<GenerateMeshEventArgs> OnGenerateMesh;
        public event EventHandler<SurfaceFilesSelectedEventArgs> OnSurfaceFilesSelected;
        private CfMeshDict dict;
        public CfMeshDict Dict
        {
            get
            {
                return dict;
            }
            set
            {
                dict = value;
                pg_main.SelectedObject = dict;
            }
        }

        public CfMeshPlane()
        {
            InitializeComponent();
            Dict = new CfMeshDict();
        }

        private void OnPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if(e.ChangedItem.Label=="Geometry files")
            {
                //if (OnSurfaceFilesSelected != null)
                    //OnSurfaceFilesSelected(this, new SurfaceFilesSelectedEventArgs(Dict.Geometry));
            }
        }
        private void bu_generate_Click(object sender, EventArgs e)
        {
            if(OnGenerateMesh!=null)
            {
                OnGenerateMesh(this, new GenerateMeshEventArgs(Dict));
            }    
        }
        public void EnableAllItmes()
        {

        }
    }
}
