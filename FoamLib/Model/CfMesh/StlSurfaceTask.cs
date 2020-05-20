using FoamLib.IO;
using FoamLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model.CfMesh
{
    public class StlSurfaceTask : FoamTask<bool>
    {
        public bool DoSurfaceFile(List<string> stlFileNames, IMonitor mon)
        {
            GlobalGeometryObject.SurfaceFileName = "";
            StlTool stlTool = new StlTool(mon);
            string tmp = Path.GetTempFileName() + ".stl";
            if (!stlTool.Merge(tmp, stlFileNames))
                return false;
            List<string> solids = stlTool.GetSolidNameList(tmp);
            if (solids == null)
                return false;
            if (solids.Count == 0)
                return false;
            GlobalGeometryObject.SurfacePartList.Clear();
            GlobalGeometryObject.SurfacePartList.AddRange(solids);
            GlobalGeometryObject.SurfaceFileName = tmp;
            return true;
        }
    }
}
