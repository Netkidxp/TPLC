using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Model
{
    [Serializable]
    public class PostDesc
    {
        private string caseFileName = "";
        //private UnitProperty internalMeshProperty = new UnitProperty();
        private List<DatasetUnitProperty> patchProperty = new List<DatasetUnitProperty>();
        private List<CutterUnitProprety> cutUnitProperty = new List<CutterUnitProprety>();

        //public UnitProperty InternalMeshProperty { get => internalMeshProperty; set => internalMeshProperty = value; }
        public List<DatasetUnitProperty> PatchProperty { get => patchProperty; set => patchProperty = value; }
        public List<CutterUnitProprety> CutUnitProperty { get => cutUnitProperty; set => cutUnitProperty = value; }
        public string CaseFileName { get => caseFileName; set => caseFileName = value; }
    }
}
