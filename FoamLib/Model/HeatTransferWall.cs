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
    public class HeatTransferWall : Wall
    {
        public HeatTransferWall(string patchName) : base(patchName)
        {
            this.wallType = WALL_TYPE.Coefficent;
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("有效散热系数")]
        public double HeatTransferCoefficent { get; set; }

        [CategoryAttribute("Boundary"), DisplayNameAttribute("环境温度")]
        public double AmbientTemperature { get; set; }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            var res = base.Update(dicts, mon);
            var d1 = dicts["T.gas"].SetChild(PatchName, CommonPatch.coefficientWall(Layers, HeatTransferCoefficent, AmbientTemperature, mon));
            var d2 = dicts["T.steel"].SetChild(PatchName, CommonPatch.coefficientWall(Layers, HeatTransferCoefficent, AmbientTemperature, mon));
            return res && !d1.IsNull && !d2.IsNull;
        }
    }
}
