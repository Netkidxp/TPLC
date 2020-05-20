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
    public class Plug : Boundary
    {
        public enum PLUG_TYPE
        {
            BasicPlug,
            SlitPlug,
            PorousPlug,
        }
        public Plug(string patchName) : base(patchName)
        {
            this.boundaryType = BOUNDARY_TYPE.Plug;
            this.plugType = PLUG_TYPE.BasicPlug;
            this.Alpha = 0.2;
            this.GasVelocity = new Vector(0,0,0);
            this.MeanSlitRadius = 0.001;
            this.Radius = 0.015;
            this.GasTemperature = 500;
        }
        [NonSerialized]
        public OnPlugTypeChange onPlugTypeChange = null;
        protected PLUG_TYPE plugType;
        [CategoryAttribute("Boundary"), DisplayNameAttribute("气相体积分数")]
        public virtual double Alpha { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("气体速度")]
        public virtual Vector GasVelocity { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("狭缝名义半径")]
        public virtual double MeanSlitRadius { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("气体温度")]
        public virtual double GasTemperature { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("透气砖等效半径")]
        public double Radius { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("透气砖类型")]
        public PLUG_TYPE PlugType { get => plugType;
            set
            {
                if (onPlugTypeChange != null)
                    onPlugTypeChange(this, this.PatchName, plugType, value); 
            }
        }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            base.Update(dicts, mon);
            dicts["alpha.gas"].SetChild(PatchName, CommonPatch.uniformFixedValue<double>(Alpha,mon));
            dicts["alphat.gas"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["alphat.steel"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["epsilon.gas"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField",mon));
            dicts["epsilon.steel"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField", mon));
            dicts["epsilonm"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField", mon));
            dicts["k.gas"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField", mon));
            dicts["k.steel"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField", mon));
            dicts["km"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField", mon));
            dicts["nut.gas"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["nut.steel"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["p"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["p_rgh"].SetChild(PatchName, CommonPatch.fixedFluxPressure(mon));
            dicts["T.gas"].SetChild(PatchName, CommonPatch.uniformFixedValue<double>(GasTemperature,mon));
            dicts["T.steel"].SetChild(PatchName, CommonPatch.fixedValue<string>("$internalField"));
            dicts["Theta"].SetChild(PatchName, CommonPatch.uniformFixedValue<double>(1.0e-7,mon));
            dicts["U.gas"].SetChild(PatchName, CommonPatch.uniformFixedValue<Vector>(GasVelocity,mon));
            dicts["U.steel"].SetChild(PatchName, CommonPatch.slip(mon));
            dicts["tracer"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            return true;
        }
    }
    public delegate void OnPlugTypeChange(Plug me, string patchName, Plug.PLUG_TYPE from, Plug.PLUG_TYPE to);
}
