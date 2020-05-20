using FoamLib.IO;
using FoamLib.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
namespace FoamLib.Util
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public sealed class GlobalConfig
    {
        private uint maxLogBufferSize = 60000;
        private string openfoamRootPath = "";
        private string mpiExePath = "";
        private string mpiName = "";
        private string pathItem = "";
        private uint nProcessor = 1;
        private List<EnvironmentItem> otherEnvironments = new List<EnvironmentItem>();
        private Color meshColor = Color.Gray;
        private Color highLightMeshColor = Color.Green;

        [Category("Common"), DisplayName("Max log buffer size")]
        public uint MaxLogBufferSize { get => maxLogBufferSize; set => maxLogBufferSize = value; }

        [Category("Common"), DisplayName("OpenFOAM Root Path"), Editor(typeof(DictionaryChooseEditor),typeof(UITypeEditor))]
        public string OpenfoamRootPath { get => openfoamRootPath; set => openfoamRootPath = value; }

        [Category("Common"), DisplayName("MPI Name")]
        public string MpiName { get => mpiName; set => mpiName = value; }

        [Category("Common"), DisplayName("Path Items")]
        public string PathItems { get => pathItem; set => pathItem = value; }

        [Category("Common"), DisplayName("Core Number")]
        public uint NProcessor { get => nProcessor; set => nProcessor = value; }
        
        [Category("Common"), DisplayName("Other Environments")]
        public List<EnvironmentItem> OtherEnvironments { get => otherEnvironments; set => otherEnvironments = value; }

        [Category("Graphic"), DisplayName("Mesh Color")]
        public Color MeshColor { get => meshColor; set => meshColor = value; }

        [Category("Graphic"), DisplayName("Highlight Mesh Color")]
        public Color HighLightMeshColor { get => highLightMeshColor; set => highLightMeshColor = value; }

        [Category("Common"), DisplayName("MPI Execuate File Root Path"), Editor(typeof(DictionaryChooseEditor), typeof(UITypeEditor))]
        public string MpiExePath { get => mpiExePath; set => mpiExePath = value; }

        public static GlobalConfig Read(string fileName)
        {
            return (GlobalConfig)new BinSerializerTool().FromFile(fileName);
        }
        public void Write(string fileName)
        {
            new BinSerializerTool().ToFile(this, fileName);
        }

    }
}
