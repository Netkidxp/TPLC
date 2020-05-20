using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class PatchReader
    {
        string boundaryFile = "";
        IMonitor monitor = null;
        public PatchReader(string vxtFileName, IMonitor mon = null)
        {
            this.boundaryFile = Path.Combine(FoamConst.GetPolyMeshPath(FoamConst.GetCaseRootFromVxt(vxtFileName)), "boundary");
            monitor = mon;
        }
        List<KeyValuePair<string,string>> GetPatchs()
        {
            List<KeyValuePair<string, string>> patches = new List<KeyValuePair<string, string>>();
            FoamDictionaryFile f = new FoamDictionaryFile(boundaryFile, monitor);
            f.Read();
            FoamDictionary d = f.Dictionary.LookupByUrl("");
            return patches;
        }
    }
}
