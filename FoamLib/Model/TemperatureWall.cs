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
    public class TemperatureWall : Wall
    {
        public TemperatureWall(string patchName) : base(patchName)
        {
            this.wallType = WALL_TYPE.Temperature;
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("温度")]
        public double Temperature { get; set; }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            var res = base.Update(dicts, mon);
            var d1 = dicts["T.gas"].SetChild(PatchName, CommonPatch.temperatureWall(Temperature, mon));
            var d2 = dicts["T.steel"].SetChild(PatchName, CommonPatch.temperatureWall(Temperature, mon));
            return res && !d1.IsNull && !d2.IsNull;
        }
    }
    
}
