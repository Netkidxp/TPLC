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
    public class CfMeshReadWriteTask : FoamTask<bool>
    {
        CfMeshReader reader = new CfMeshReader();

        public CfMeshReader Reader { get => reader; set => reader = value; }

        public bool DoRead(string mpk, string tarDir, IMonitor mon)
        {
            return Reader.Read(mpk, tarDir, mon);
        }
        private Task<bool> _DoRead(string mpk, string tarDir, IMonitor mon)
        {
            Task<bool> task = Task<bool>.Run(()=>DoRead(mpk,tarDir,mon));
            return task;
        }
        public async Task AsyncRead(string mpk, string tarDir, IMonitor mon)
        {
            TaskEventArgs arg = new TaskEventArgs();
            TaskStarted(arg);
            bool ret = await _DoRead(mpk, tarDir, mon);
            arg.Objects.Add("Operation", "Read");
            arg.Objects.Add("Result", ret);
            arg.Objects.Add("Dict", Reader.Dict);
            TaskFinished(arg);
        }
        public bool DoWrite(string tarDir, CfMeshDict dict, string mpk, IMonitor mon)
        {
            CfMeshWriter writer = new CfMeshWriter(tarDir, dict);
            return writer.Save(mpk, mon);
        }
        private Task<bool> _DoWrite(string tarDir, CfMeshDict dict, string mpk, IMonitor mon)
        {
            Task<bool> task = Task<bool>.Run(()=>DoWrite(tarDir,dict,mpk,mon));
            return task;
        }
        public async Task AsyncWrite(string tarDir, CfMeshDict dict, string mpk, IMonitor mon)
        {
            TaskEventArgs arg = new TaskEventArgs();
            TaskStarted(arg);
            bool ret = await _DoWrite(tarDir, dict, mpk, mon);
            arg.Objects.Add("Operation", "Write");
            arg.Objects.Add("Result", ret);
            TaskFinished(arg);
        }
    }
}
