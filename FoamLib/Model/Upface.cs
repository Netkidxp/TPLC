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
    public class Upface : Boundary
    {
        public Upface(string patchName) : base(patchName)
        {
            this.boundaryType = BOUNDARY_TYPE.Upface;
        }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            base.Update(dicts, mon);
            dicts["alpha.gas"].SetChild(PatchName, CommonPatch.uniformInletOutlet<double>("phi.gas",1,1,mon));
            dicts["alphat.gas"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["alphat.steel"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["epsilon.gas"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phi.gas","$internalField","$internalField", mon));
            dicts["epsilon.steel"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phi.steel", "$internalField", "$internalField", mon));
            dicts["epsilonm"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phim", "$internalField", "$internalField", mon));
            dicts["k.gas"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phi.gas", "$internalField", "$internalField", mon));
            dicts["k.steel"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phi.steel", "$internalField", "$internalField", mon));
            dicts["km"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phim", "$internalField", "$internalField", mon));
            dicts["nut.gas"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["nut.steel"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["p"].SetChild(PatchName, CommonPatch.calculated(mon));
            dicts["p_rgh"].SetChild(PatchName, CommonPatch.prghPressure(mon));
            dicts["T.gas"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phi.gas", "$internalField", "$internalField", mon));
            dicts["T.steel"].SetChild(PatchName, CommonPatch.inletOutlet<string>("phi.steel", "$internalField", "$internalField", mon));
            dicts["Theta"].SetChild(PatchName, CommonPatch.uniformInletOutlet<double>("phim",1.0e-7,10e-7,mon));
            dicts["U.gas"].SetChild(PatchName, CommonPatch.pressureInletOutletVelocity("phi.gas",mon));
            dicts["U.steel"].SetChild(PatchName, CommonPatch.pressureInletOutletVelocity("phi.steel", mon));
            dicts["tracer"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
            return true;
        }
    }
}
