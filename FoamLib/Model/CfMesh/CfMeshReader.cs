using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Util;
namespace FoamLib.Model.CfMesh
{
    public class CfMeshReader
    {
        CfMeshDict dict = null;
        List<string> partFileNames = new List<string>();
        public CfMeshDict Dict { get => dict; set => dict = value; }
        public IMonitor Monitor { get => monitor; set => monitor = value; }
        public List<string> PartFileNames { get => partFileNames; }

        IMonitor monitor;

        public CfMeshReader(IMonitor monitor)
        {
            this.monitor = monitor;
        }

        public bool Read(string fileName, string targetDir)
        {
            bool res = true;
            try
            {
                string msg = Zip.UncomporessZip(fileName, targetDir);
                res = msg == "ok";
                if (msg != "ok" && monitor != null)
                    monitor.ErrorLine("Uncompress mesh file error: " + msg);
                BinSerializerTool bs = new BinSerializerTool();
                string dfn = Path.Combine(targetDir, CfMeshWriter.DictFileName);
                if (File.Exists(dfn))
                {
                    dict = (CfMeshDict)bs.FromFile(dfn);
                    if(dict==null && monitor != null)
                    {
                        monitor.ErrorLine("UnSerialize CfMesh dict from file error: " + bs.LastError);
                    }
                    res = dict != null;
                    string surfaceFileName = FoamConst.GetSurfaceFileName(targetDir);
                    if (File.Exists(surfaceFileName))
                    {
                        GlobalGeometryObject.SurfaceFileName = surfaceFileName;
                        GlobalGeometryObject.SurfacePartList.Clear();
                        StlTool stlTool = new StlTool(monitor);
                        partFileNames.Clear();
                        List<string> stls = stlTool.Decompose(surfaceFileName);
                        if(stls == null)
                        {
                            if (monitor != null)
                                monitor.ErrorLine("Decompose stl file error, stl file is: " + surfaceFileName);
                            res = false;
                        }
                        partFileNames.AddRange(stls);
                        foreach(string fn in stls)
                        {
                            GlobalGeometryObject.SurfacePartList.Add(Path.GetFileNameWithoutExtension(fn));
                        }
                    }
                }
            }
            catch(Exception e)
            {
                if (monitor != null)
                    monitor.ErrorLine("Read Cfmesh file error: " + e.Message);
                res = false;
            }
            return res;
        }
    }
}
