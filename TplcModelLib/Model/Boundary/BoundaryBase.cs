using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.Model;
using FoamLib.UI;
namespace Tplc.Model.Boundary
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class BoundaryBase : IBoundaryObject
    {
        private string patchName;

        protected BoundaryBase(string patchName)
        {
            this.patchName = patchName;
        }

        [DisplayName("Patch name"),ReadOnly(true)]
        public string PatchName
        {
            get
            {
                return patchName;
            }
            set
            {
                patchName = value;
            }
        }

        public abstract bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null);
    }
}
