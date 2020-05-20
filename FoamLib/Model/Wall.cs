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
    public class Wall : Boundary
    {
        public enum WALL_TYPE
        {
            
            Adiabat,
            Temperature,
            Power,
            Flux,
            Coefficent
        }
        [NonSerialized]
        public OnWallHeatTypeChange onHeatTypeChange = null;
        protected WALL_TYPE wallType;
        private double gasPrt;
        private double steelPrt;
        List<WallLayer> layers = new List<WallLayer>();
        public Wall(string patchName) : base(patchName)
        {
            this.boundaryType = BOUNDARY_TYPE.Wall;
            this.wallType = WALL_TYPE.Adiabat;
            this.gasPrt = 1.0;
            this.steelPrt = 1.0;
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("材料层列表")]
        public List<WallLayer> Layers { get => layers; set => layers = value; }

        [CategoryAttribute("Boundary"), DisplayNameAttribute("壁面类型")]
        public WALL_TYPE WallType{
            get => wallType;
            set
            {
                if (onHeatTypeChange != null)
                    onHeatTypeChange(this, this.PatchName, wallType, value);
            }
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("气体普朗特数")]
        public double GasPrt { get => gasPrt; set => gasPrt = value; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("钢液普朗特数")]
        public double SteelPrt { get => steelPrt; set => steelPrt = value; }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            base.Update(dicts,mon);
            dicts["alpha.gas"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            dicts["alphat.gas"].SetChild(PatchName, CommonPatch.compressible_alphaWallFunction(gasPrt, mon));
            dicts["alphat.steel"].SetChild(PatchName, CommonPatch.compressible_alphaWallFunction(steelPrt, mon));
            dicts["epsilon.gas"].SetChild(PatchName, CommonPatch.epsilonWallFunction(mon));
            dicts["epsilon.steel"].SetChild(PatchName, CommonPatch.epsilonWallFunction(mon));
            dicts["epsilonm"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            dicts["k.gas"].SetChild(PatchName, CommonPatch.kqRWallFunction(mon));
            dicts["k.steel"].SetChild(PatchName, CommonPatch.kqRWallFunction(mon));
            dicts["km"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            dicts["nut.gas"].SetChild(PatchName, CommonPatch.nutkWallFunction(mon));
            dicts["nut.steel"].SetChild(PatchName, CommonPatch.nutkWallFunction(mon));
            dicts["p"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["p_rgh"].SetChild(PatchName, CommonPatch.fixedFluxPressure(mon));
            dicts["T.gas"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            dicts["T.steel"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            dicts["Theta"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            dicts["U.gas"].SetChild(PatchName, CommonPatch.noSlip(mon));
            dicts["U.steel"].SetChild(PatchName, CommonPatch.noSlip(mon));
            dicts["tracer"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            return true;
        }
    }
    public delegate void OnWallHeatTypeChange(Wall me, string patchName, Wall.WALL_TYPE from, Wall.WALL_TYPE to);
}
