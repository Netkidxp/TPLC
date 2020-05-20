using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class Runner
    {
        protected GlobalConfig foamConfig;
        protected bool log = false;
        protected bool err = false;
        protected string name = "";
        protected EventHandler onExit = null;
        protected DataReceivedEventHandler onOutput = null;
        protected DataReceivedEventHandler onError = null;
        public EventHandler OnExit
        {
            get
            {
                return onExit;
            }
            set
            {
                if (onExit != null)
                    p.Exited -= onExit;
                onExit = value;
                if (value != null)
                    p.Exited += value;
            }
        }
        public DataReceivedEventHandler OnOutput
        {
            get
            {
                return onOutput;
            }
            set
            {
                if (onOutput != null)
                    p.OutputDataReceived -= onOutput;
                onOutput = value;
                if (value != null)
                    p.OutputDataReceived += value;
            }
        }
        public DataReceivedEventHandler OnError
        {
            get
            {
                return onError;
            }
            set
            {
                if (onError != null)
                    p.ErrorDataReceived -= onError;
                onError = value;
                if (value != null)
                    p.ErrorDataReceived += value;
            }
        }
        public Process Process { get => p; }
        public int ExitCode { get => p.ExitCode; }
        protected Process p = null;
        protected bool isRunning = false;
        public bool IsRunning { get => isRunning; }
        public bool IsWriteLog
        {
            get => log;
            set
            {
                log = value;
            }
        }
        public string Name { get => name; }
        public bool IsWriteErr { get => err; set => err = value; }
        public string LogFileName
        {
            get
            {
                return Path.Combine(p.StartInfo.WorkingDirectory, name + ".log");
            }
        }
        public string ErrFileName
        {
            get
            {
                return Path.Combine(p.StartInfo.WorkingDirectory, name + ".err");
            }
        }
        public Runner(GlobalConfig foamConfig)
        {
            this.foamConfig = foamConfig;
            p = new Process();
            p.EnableRaisingEvents = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += OnRunnerOutput;
            p.ErrorDataReceived += OnRunnerError;
            p.StartInfo.StandardErrorEncoding = new UTF8Encoding(false);
            p.StartInfo.StandardOutputEncoding = new UTF8Encoding(false);
            p.Exited += OnRunnerExit;

            string orgPathStr = "";
            if (p.StartInfo.Environment.ContainsKey("PATH"))
            {
                orgPathStr = p.StartInfo.Environment["PATH"];
            }
            orgPathStr += ";" + foamConfig.PathItems;
            if (!string.IsNullOrWhiteSpace(foamConfig.MpiName))
                p.StartInfo.Environment.Add("WM_MPLIB", foamConfig.MpiName);
            p.StartInfo.Environment.Add("PATH", orgPathStr);
            if (!string.IsNullOrWhiteSpace(foamConfig.OpenfoamRootPath))
                p.StartInfo.Environment.Add("WM_PROJECT_DIR", foamConfig.OpenfoamRootPath);
            foreach (var oe in foamConfig.OtherEnvironments)
            {
                if (string.IsNullOrWhiteSpace(oe.Name) || string.IsNullOrWhiteSpace(oe.Value))
                    continue;
                p.StartInfo.Environment.Add(oe.Name, oe.Value);
            }
        }
        public void Abort()
        {
            if (isRunning)
            {
                p.Kill();
            }

        }
        protected void OnRunnerOutput(object sender, DataReceivedEventArgs args)
        {
            if (log)
            {
                StreamWriter w = new StreamWriter(LogFileName, true, new UTF8Encoding(false));
                w.Write(args.Data + "\r\n");
                w.Close();
            }
        }
        protected void OnRunnerError(object sender, DataReceivedEventArgs args)
        {
            if (err)
            {
                StreamWriter w = new StreamWriter(ErrFileName, true, new UTF8Encoding(false));
                w.Write(args.Data + "\r\n");
                w.Close();
            }
        }
        protected void OnRunnerExit(object sender, EventArgs args)
        {
            p.CancelErrorRead();
            p.CancelOutputRead();
            isRunning = false;
        }
        public void AsyncRun(string application, string arguments, string workDirecory)
        {

            p.StartInfo.FileName = application;
            p.StartInfo.Arguments = arguments;
            p.StartInfo.WorkingDirectory = workDirecory;
            if (File.Exists(LogFileName))
                File.Delete(LogFileName);
            if (File.Exists(ErrFileName))
                File.Delete(ErrFileName);
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            isRunning = true;
        }
        public void Run(string application, string arguments, string workDirecory)
        {
            p.StartInfo.FileName = application;
            p.StartInfo.Arguments = arguments;
            p.StartInfo.WorkingDirectory = workDirecory;
            if (File.Exists(LogFileName))
                File.Delete(LogFileName);
            if (File.Exists(ErrFileName))
                File.Delete(ErrFileName);
            isRunning = true;
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            isRunning = false;
        }
    }
}
