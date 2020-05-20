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
    public class Wall : BoundaryBase
    {
        public Wall(string patchName) : base(patchName)
        {

        }
        public override bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            try
            {
                dicts["U"].SetChild(PatchName, CommonPatch.slip(mon));
                dicts["p"].SetChild(PatchName, CommonPatch.zeroGradient(mon));
                dicts["k"].SetChild(PatchName, CommonPatch.kqRWallFunction(mon));
                dicts["epsilon"].SetChild(PatchName, CommonPatch.epsilonWallFunction(mon));
                dicts["nut"].SetChild(PatchName, CommonPatch.nutkWallFunction(mon));
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
            return "Wall";
        }
    }
}
