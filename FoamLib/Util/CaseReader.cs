using FoamLib.IO;
using FoamLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class CaseReader : FoamTask
    {
        string caseFile = "";
        string caseRoot = "";
        Case caseObject = null;
        //string vxtFile = "";

        public string CaseFile { get => caseFile;}
        public string CaseRoot { get => caseRoot;}
        public string VxtFile { get
            {
                return FoamConst.GetVxtFilePath(caseRoot);
            }
        }
        internal Case CaseObject { get => caseObject; }

        public static string BinFileToCase(ref Case cas, String fileName, IMonitor mon = null)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                fs.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[fs.Length + 1];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();
                MemoryStream memory = new MemoryStream(data);
                BinaryFormatter bf = new BinaryFormatter();
                cas = (Case)bf.Deserialize(memory);
                if (cas == null)
                {
                    return "Illegal case file format";
                }
                else
                    return "ok";

            }
            catch (Exception e)
            {
                if (mon != null)
                    mon.ErrorLine(e.Message);
                return e.Message;
            }
        }

        private static string _OpenCase(ref Case cas, ref string root, string caseFilePath, IMonitor mon = null)
        {

            root = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var constant = FoamConst.GetConstantPath(root);
            var polymesh = FoamConst.GetPolyMeshPath(root);
            var vxt = FoamConst.GetVxtFilePath(root);
            var desc = FoamConst.GetDescFilePath(root);
            try
            {
                Directory.CreateDirectory(root);
                Directory.CreateDirectory(constant);
                Directory.CreateDirectory(polymesh);
                Zip.UncomporessZip(caseFilePath, polymesh);
                var res = BinFileToCase(ref cas, desc, mon);
                File.Create(vxt);
                return res;
            }
            catch(Exception e)
            {
                Err(mon, e.Message);
                return e.Message;
            }
        }

        private Task<string> OpenCase(string caseFilePath, IMonitor mon = null)
        {
            caseFile = caseFilePath;
            Task<string> task = Task<string>.Run(() =>_OpenCase(ref caseObject,ref caseRoot, caseFilePath,mon));
            return task;
        }

        public async Task AsyncOpenCase(string caseFilePath, IMonitor mon = null)
        {
            TaskEventArgs ea = new TaskEventArgs();
            ea.Objects.Add("Case", caseObject);
            ea.Objects.Add("CaseFilePath", caseFilePath);
            TaskStarted(ea);
            ea.Message = await OpenCase(caseFilePath,mon);
            ea.Objects.Add("CaseRoot", caseRoot);
            TaskFinished(ea);
        }
    }
}
