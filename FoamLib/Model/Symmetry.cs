using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    class Symmetry : Boundary
    {
        public Symmetry(string patchName) : base(patchName)
        {
            this.boundaryType = BOUNDARY_TYPE.Symmetry;
        }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            base.Update(dicts);
            foreach(FoamDictionary d in dicts.Values)
            {
                d.SetChild(PatchName, CommonPatch.symmetry(mon));
            }
            return true;
        }
    }
}
