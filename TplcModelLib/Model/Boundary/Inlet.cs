using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;

namespace Tplc.Model.Boundary
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Inlet : BoundaryBase
    {
        float velocity;
        float k;
        float epsilon;

        public Inlet(string patchName, float velocity, float k, float epsilon) : base(patchName)
        {
            this.velocity = velocity;
            this.k = k;
            this.epsilon = epsilon;
        }
        [DisplayName("Velocity")]
        public float Velocity { get => velocity; set => velocity = value; }

        [DisplayName("Turbulence kinetic energy")]
        public float K { get => k; set => k = value; }

        [DisplayName("Turbulent dissipation rate")]
        public float Epsilon { get => epsilon; set => epsilon = value; }

        public override string ToString()
        {
            return "Inlet";
        }

        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            try
            {
                dicts["U"].SetChild(PatchName, CommonPatch.surfaceNormalFixedValue<float>(velocity * -1, mon));
                dicts["p"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
                dicts["k"].SetChild(PatchName, CommonPatch.uniformFixedValue<float>(K, mon));
                dicts["epsilon"].SetChild(PatchName, CommonPatch.uniformFixedValue<float>(Epsilon, mon));
                dicts["nut"].SetChild(PatchName, CommonPatch.calculated(mon));
                return true;
            }
            catch(Exception e)
            {
                mon.ErrorLine(e.Message);
                return false;
            }
        }
    }
}
