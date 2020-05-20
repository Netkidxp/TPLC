using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SolveControl : IFoamObject
    {
        double startTime;
        double endTime;
        double delteTime;
        double writeInterval;
        bool adjustTimeStep;
        double maxCo;
        double maxDeltaTime;
        uint nParallel;
        List<ProbeMonitor> monitors;
        List<ControlFunction> functions;
        public SolveControl()
        {
            this.startTime = 0;
            this.endTime = 100;
            this.delteTime = 1;
            this.writeInterval = 1;
            this.adjustTimeStep = false;
            this.maxCo = 1;
            this.maxDeltaTime = 1;
            this.nParallel = 1;
            monitors = new List<ProbeMonitor>();
            this.functions = new List<ControlFunction>();

        }

        public SolveControl(double startTime, double endTime, double delteTime, double writeInterval, bool adjustTimeStep, double maxCo, double maxDeltaTime, List<ControlFunction> functions)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.delteTime = delteTime;
            this.writeInterval = writeInterval;
            this.adjustTimeStep = adjustTimeStep;
            this.maxCo = maxCo;
            this.maxDeltaTime = maxDeltaTime;
            this.functions = functions;
        }

        [CategoryAttribute("Solve Control"), DisplayNameAttribute("开始时间")]
        public double StartTime { get => startTime; set => startTime = value; }

        [CategoryAttribute("Solve Control"), DisplayNameAttribute("结束时间")]
        public double EndTime { get => endTime; set => endTime = value; }

        [CategoryAttribute("Solve Control"), DisplayNameAttribute("步长")]
        public double DelteTime { get => delteTime; set => delteTime = value; }

        [CategoryAttribute("Solve Control"), DisplayNameAttribute("存储间隙")]
        public double WriteInterval { get => writeInterval; set => writeInterval = value; }

        [CategoryAttribute("Solve Control"), DisplayNameAttribute("自适应步长")]
        public bool AdjustTimeStep
        {
            get => adjustTimeStep;
            set => adjustTimeStep = value;
        }

        [CategoryAttribute("Solve Control"), DisplayNameAttribute("并行核心数量")]
        public uint NParallel { get => nParallel; set => nParallel = value; }


        [CategoryAttribute("Solve Control"), BrowsableAttribute(true), DisplayNameAttribute("最大库朗数")]
        public double MaxCo { get => maxCo; set => maxCo = value; }

        [CategoryAttribute("Solve Control"), BrowsableAttribute(true), DisplayNameAttribute("最大步长")]
        public double MaxDeltaTime { get => maxDeltaTime; set => maxDeltaTime = value; }

        [CategoryAttribute("Solve"), DisplayNameAttribute("监视列表")]
        public List<ProbeMonitor> Monitors { get => monitors; set => monitors = value; }

        [CategoryAttribute("Solve Control"), BrowsableAttribute(true), DisplayNameAttribute("附加函数列表")]
        public List<ControlFunction> Functions { get => functions; set => functions = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile controlFile = new FoamDictionaryFile(FoamConst.GetControlDictFileName(foamRootPathName), monitor);
            controlFile.Read();
            controlFile.Dictionary.SetChild("application", "ladleFoam");
            controlFile.Dictionary.SetChild("startFrom", "latestTime");
            controlFile.Dictionary.SetChild("startTime", startTime);
            controlFile.Dictionary.SetChild("endTime", endTime);
            controlFile.Dictionary.SetChild("deltaT", delteTime);
            controlFile.Dictionary.SetChild("writeInterval", writeInterval);
            controlFile.Dictionary.SetChild("adjustTimeStep", adjustTimeStep ? "yes" : "no");
            controlFile.Dictionary.SetChild("maxCo", maxCo);
            controlFile.Dictionary.SetChild("maxDeltaT", maxDeltaTime);
            FoamDictionary dfun = controlFile.Dictionary.GetByUrl("functions");
            foreach (ControlFunction fun in functions)
            {
                dfun.SetChild(fun.FunctionName,FoamDictionary.DecodeDictionary(fun.FunctionBody, false, monitor));
            }
            controlFile.Write();
            foreach (var m in Monitors)
            {
                m.Write(foamRootPathName, monitor);
            }
            FoamDictionaryFile decomposeParDictFile = new FoamDictionaryFile(FoamConst.GetDeComposeParDictFileName(foamRootPathName), monitor);
            decomposeParDictFile.Read();
            decomposeParDictFile.Dictionary.SetChild("numberOfSubdomains", nParallel);
            decomposeParDictFile.Dictionary.SetChild("method", "scotch");
            decomposeParDictFile.Dictionary.SetChild("scotchCoeffs", new FoamDictionary());
            decomposeParDictFile.Write();
            return true;
        }
    }
}
