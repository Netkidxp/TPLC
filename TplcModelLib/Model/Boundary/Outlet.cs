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
    public class Outlet : BoundaryBase
    {
        public Outlet(string patchName) : base(patchName)
        {
        }
        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            //throw new NotImplementedException();
            try
            {
                dicts["U"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
                dicts["p"].SetChild(PatchName, CommonPatch.uniformFixedValue<double>(0.0, mon));
                dicts["k"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
                dicts["epsilon"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
                dicts["nut"].SetChild(PatchName, CommonPatch.calculated(mon));
                return true;
            }
            catch (Exception e)
            {
                mon.ErrorLine(e.Message);
                return false;
            }
        }
        public override string ToString()
        {
            return "Outlet";
        }
    }
}
