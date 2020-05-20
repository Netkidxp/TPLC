using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Util;
using System.IO;

namespace FoamLib.Model.CfMesh
{
    public class CfMeshWriter
    {
        public static string DictFileName = "cfmesh.dict";
        public IMonitor Monitor { get => monitor; set => monitor = value; }

        IMonitor monitor;

        public CfMeshWriter(IMonitor monitor)
        {
            this.monitor = monitor;
        }

        public bool Save(CfMeshDict dict, string meshRoot, string fileName)
        {
            bool res = true;
            try
            {
                if(meshRoot == "")
                {
                    meshRoot = Path.Combine(Path.GetTempPath(), "cfmesh" + Guid.NewGuid().ToString());
                    Directory.CreateDirectory(meshRoot);
                }
                if (dict != null)
                {
                    string stl = FoamConst.GetSurfaceFileName(meshRoot);
                    if (stl != GlobalGeometryObject.SurfaceFileName)
                        File.Copy(GlobalGeometryObject.SurfaceFileName, FoamConst.GetSurfaceFileName(meshRoot), true);
                    BinSerializerTool bs = new BinSerializerTool();
                    res = bs.ToFile(dict, Path.Combine(meshRoot, DictFileName));
                    if(!res&&monitor!=null)
                    {
                        monitor.ErrorLine("Serializer CfMesh dict to file error: " + bs.LastError);
                    }
                }
                string message = Zip.CompressDirectory(meshRoot, fileName);
                res &= message == "ok";
                if (message != "ok" && monitor != null)
                    monitor.ErrorLine("Compress directory error: " + message);
            }
            catch(Exception e)
            {
                if (monitor != null)
                    monitor.ErrorLine("Save mesh file error: " + e.Message);
                res = false;
            }
            return res;
        }
    }
}
