using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;

namespace FoamLib.Util
{
    public class FoamMeshVerifier
    {
        public static bool VerifyDirectory(string dir)
        {
            return Directory.Exists(FoamConst.GetConstantPath(dir)) &&
                Directory.Exists(FoamConst.GetPolyMeshPath(dir)) &&
                File.Exists(Path.Combine(FoamConst.GetPolyMeshPath(dir), "points"));
        }
        public static bool VerifyVxtPath(string vxt)
        {
            string dir = Path.GetDirectoryName(vxt);
            return VerifyDirectory(dir);
        }
    }
}
