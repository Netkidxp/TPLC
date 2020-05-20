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
    public class HeatFluxWall : Wall
    {
        public HeatFluxWall(string patchName) : base(patchName)
        {
            this.WallType = WALL_TYPE.Flux;
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("热流率")]
        public double HeatFlux { get; set; }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            var res = base.Update(dicts, mon);
            var d1 = dicts["T.gas"].SetChild(PatchName, CommonPatch.fluxWall(Layers, HeatFlux, mon));
            var d2 = dicts["T.steel"].SetChild(PatchName, CommonPatch.fluxWall(Layers, HeatFlux, mon));
            return res && !d1.IsNull && !d2.IsNull;
        }
    }
}
