using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.Model;

namespace FoamLib.Util
{
    public class CaseWriter : FoamTask
    {
        public static string CaseToBinFile(Case cas, String fileName, IMonitor mon = null)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream memory = new MemoryStream();
                bf.Serialize(memory, cas);
                byte[] bytes = memory.GetBuffer();
                memory.Close();
                FileStream fs = new FileStream(fileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Close();
                return "ok";
            }
            catch (Exception e)
            {
                Err(mon, e.Message);
                return e.Message;
            }
        }

        

        private static string _SaveCase(Case cas, string meshVxtFilePath, string saveFilePath, IMonitor mon = null)
        {
            string root = Path.GetDirectoryName(meshVxtFilePath);
            string mesh = FoamConst.GetPolyMeshPath(root);
            string desc = FoamConst.GetDescFilePath(root);
            string res = CaseToBinFile(cas,desc,mon);
            if(res!="ok")
                return res;
           return Zip.CompressDirectory(mesh, saveFilePath);
        }

        private Task<string> SaveCase(Case cas, string meshVxtFilePath, string saveFilePath, IMonitor mon = null)
        {
            Task<string> task = Task<string>.Run(() => _SaveCase(cas, meshVxtFilePath, saveFilePath, mon));
            return task;
        }

        public async Task AsyncSaveCase(Case cas, string meshVxtFilePath, string saveFilePath, IMonitor mon = null)
        {
            TaskEventArgs ea = new TaskEventArgs();
            ea.Objects.Add("Case", cas);
            ea.Objects.Add("MeshVxtFilePath", meshVxtFilePath);
            ea.Objects.Add("SaveFilePath", saveFilePath);
            TaskStarted(ea);
            ea.Message = await SaveCase(cas, meshVxtFilePath, saveFilePath, mon);

            TaskFinished(ea);
        }
    }
}
