using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model.CfMesh
{
    public static class GlobalGeometryObject
    {
        public static List<string> SurfacePartList = new List<string>();
        //public static Dictionary<string, AnalyticGeometry> AnalyticGeometryList  = new Dictionary<string, AnalyticGeometry>();
        public static string SurfaceFileName = "";
        public static double Volume = 0;
    }
}
