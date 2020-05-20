using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FoamLib.IO;
using FoamLib.Model;

namespace FoamLib.Util
{
    public class CaseConvertor : FoamTask
    {
        private static string _ConvertCase(string caseFileName, string templeteFileName, string runDirName, IMonitor mon)
        {
            string res = Zip.UncomporessZip(templeteFileName, runDirName);
            if (res != "ok")
            {
                var err = string.Format("加载模板文件失败:\r\n  模板文件名:{0}\r\n  加载目标位置:{1}\r\n  错误描述:{2}", templeteFileName, runDirName, res);
                Err(mon, err);
                return err;
            }
            else
                Log(mon, string.Format("加载模板文件完成:\r\n  模板文件名:{0}", templeteFileName));
            string meshDir = FoamConst.GetPolyMeshPath(runDirName);
            if (!Directory.Exists(meshDir))
                Directory.CreateDirectory(meshDir);
            res = Zip.UncomporessZip(caseFileName, meshDir);
            if (res != "ok")
            {
                var err = string.Format("加载工程文件失败:\r\n  工程文件名:{0}\r\n  错误描述:{1}", caseFileName, res);
                Err(mon, err);
                return err;
            }
            else
                Log(mon, string.Format("加载工程文件完成:\r\n  工程文件名:{0}\r\n", caseFileName));
            Case cas = null;
            res = CaseReader.BinFileToCase(ref cas, FoamConst.GetDescFilePath(runDirName), mon);
            if (cas == null)
            {
                var err = string.Format("非法工程文件格式:\r\n  工程文件名:{0}", caseFileName);
                Err(mon, err);
                return err;
            }
            else
            {
                cas.Write(runDirName, mon);
                Log(mon, "工程结算环境配置完成");
                return "ok";
            }
        }
        private Task<string> ConvertCase(string caseFileName, string templeteFileName, string runDirName, IMonitor mon)
        {
            Task<string> task = Task<string>.Run(()=> _ConvertCase(caseFileName, templeteFileName, runDirName, mon));
            return task;
        }

        public async Task AsyncConvertCase(string caseFileName, string templeteFileName, string runDirName, IMonitor mon = null)
        {
            TaskEventArgs ea = new TaskEventArgs();
            ea.Objects.Add("CaseFileName", caseFileName);
            ea.Objects.Add("TempleteFileName", templeteFileName);
            ea.Objects.Add("RunDirName", runDirName);
            TaskStarted(ea);
            ea.Message = await ConvertCase(caseFileName, templeteFileName, runDirName, mon);
            TaskFinished(ea);
        }
    }
}
