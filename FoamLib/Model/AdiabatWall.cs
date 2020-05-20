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
    public class AdiabatWall : Wall
    {
        public AdiabatWall(string patchName) : base(patchName)
        {
            this.wallType = WALL_TYPE.Adiabat;
        }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            base.Update(dicts, mon);
            
            return true;
        }
    }
}
