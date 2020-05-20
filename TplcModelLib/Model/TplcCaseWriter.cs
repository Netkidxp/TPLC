using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.Model.CfMesh;
using FoamLib.Util;

namespace Tplc.Model
{
    public class TplcCaseWriter
    {
        public static string CaseDictFileName = "case.dict";
        public static string MeshDictFileName = "mesh.dict";
        public static string MeshPkgFileName = "mesh.cms";
        public static string CasePkgFileName = "case.pkg";
        public IMonitor Monitor { get => monitor; set => monitor = value; }
        IMonitor monitor;
        CfMeshWriter meshWriter = null;

        public TplcCaseWriter(IMonitor monitor)
        {
            this.monitor = monitor;
            meshWriter = new CfMeshWriter(monitor);
        }

        public bool Save(CfMeshDict meshDict, TplcCase caseDict, string meshRoot, string caseRoot,string fileName)
        {
            try
            {
                string tarDir = Path.Combine(Path.GetTempPath(), "save" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(tarDir);
                BinSerializerTool bs = new BinSerializerTool();
                bool res = bs.ToFile(caseDict, Path.Combine(tarDir,CaseDictFileName));
                if (!res)
                {
                    if (monitor != null)
                        monitor.ErrorLine("Case dict to file error: " + bs.LastError);
                    return false;
                }
                if (caseDict.State.IsMeshPrepared)
                {
                    res = meshWriter.Save(meshDict, meshRoot, Path.Combine(tarDir, MeshPkgFileName));
                    if (!res)
                        return false;
                }
                if(caseDict.State.IsCaseInitlized)
                {
                    string sres = Zip.CompressDirectory(caseRoot, Path.Combine(tarDir, CasePkgFileName));
                    if(sres!="ok")
                    {
                        if(monitor!=null)
                            monitor.ErrorLine("Compress data to pkg error: " + sres);
                        return false;
                    }
                }
                string rs = Zip.CompressDirectory(tarDir, fileName);
                if (rs != "ok")
                {
                    if (monitor != null)
                        monitor.ErrorLine("Compress case data file error: " + rs);
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                if(monitor!=null)
                {
                    if (monitor != null)
                        monitor.ErrorLine("Save case file error: " + ex.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
