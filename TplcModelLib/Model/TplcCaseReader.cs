using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Util;
using FoamLib.Model.CfMesh;


namespace Tplc.Model
{
    public class TplcCaseReader
    {
        TplcCase caseDict = null;
        string meshFileName = "";
        string caseRunRoot = "";
        public IMonitor Monitor { get => monitor; set => monitor = value; }
        public TplcCase CaseDict { get => caseDict; set => caseDict = value; }
        public string MeshFileName { get => meshFileName; }
        public string CaseRunRoot { get => caseRunRoot; }

        IMonitor monitor;

        public TplcCaseReader(IMonitor monitor)
        {
            this.monitor = monitor;
        }

        public bool Read(string fileName)
        {
            try
            {
                meshFileName = "";
                caseRunRoot = "";
                string tarDir = Path.Combine(Path.GetTempPath(), "read" + Guid.NewGuid().ToString());
                string rs = Zip.UncomporessZip(fileName, tarDir);
                if(rs!="ok")
                {
                    if (monitor != null)
                        monitor.ErrorLine("Uncompress Case file error: " + rs);
                    return false;
                }
                BinSerializerTool bs = new BinSerializerTool();
                caseDict = (TplcCase)bs.FromFile(Path.Combine(tarDir, TplcCaseWriter.CaseDictFileName));
                if(caseDict == null)
                {
                    if (monitor != null)
                        monitor.ErrorLine("File to case error: " + bs.LastError);
                    return false;
                }
                string meshfile = Path.Combine(tarDir, TplcCaseWriter.MeshPkgFileName);
                if (caseDict.State.IsMeshPrepared && File.Exists(meshfile))
                    meshFileName = meshfile;
                string casepkg = Path.Combine(tarDir, TplcCaseWriter.CasePkgFileName);
                if (caseDict.State.IsCaseInitlized && File.Exists(casepkg))
                {
                    string caserun = Path.Combine(Path.GetTempPath(), "case" + Guid.NewGuid().ToString());
                    rs = Zip.UncomporessZip(casepkg, caserun);
                    if (rs != "ok")
                    {
                        if (monitor != null)
                            monitor.ErrorLine("Uncompress data file error: " + rs);
                        return false;
                    }
                    caseRunRoot = caserun;
                }
                return true;
            }
            catch(Exception e)
            {
                if (monitor != null)
                    monitor.ErrorLine("Read Case file error: " + e.Message);
                return false;
            }
        }
    }
}
