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
    public class PowerWall : Wall
    {
        public PowerWall(string patchName) : base(patchName)
        {
            this.wallType = WALL_TYPE.Power;
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("热功率")]
        public double Power { get; set; }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            var res = base.Update(dicts, mon);
            var d1 = dicts["T.gas"].SetChild(PatchName, CommonPatch.powerWall(Layers,Power,mon));
            var d2 = dicts["T.steel"].SetChild(PatchName, CommonPatch.powerWall(Layers, Power, mon));
            return res && !d1.IsNull && !d2.IsNull;
        }
    }
}
