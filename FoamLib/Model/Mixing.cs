using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Mixing : IFoamObject
    {
        public Mixing()
        {
            SolveTracer = false;
            Sc = 0.01;
            Injects = new List<Inject>();
        }

        public Mixing(bool solveTracer, double sc, List<Inject> injects)
        {
            SolveTracer = solveTracer;
            Sc = sc;
            Injects = injects;
        }

        [CategoryAttribute("混匀效率"), DisplayNameAttribute("是否计算示踪剂")]
        public bool SolveTracer { get; set; }

        [CategoryAttribute("混匀效率"), DisplayNameAttribute("施密特数")]
        public double Sc { get; set; }

        [CategoryAttribute("混匀效率"), DisplayNameAttribute("喂丝点")]
        public List<Inject> Injects { get; set; }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            var f = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "tracerProperties"), monitor);
            f.Read();
            var d = f.Dictionary;
            d.SetChild("Sc", "Sc [0 0 0 0 0 0 0] " + Sc.ToString());
            d.SetChild("tracerPhaseName", "steel");
            f.Write();
            f = new FoamDictionaryFile(FoamConst.GetFvSolutionFileName(foamRootPathName), monitor);
            f.Read();
            d = f.Dictionary.Child("PIMPLE");
            d.SetChild("solveTracer", SolveTracer ? "yes" : "no");
            f.Write();
            foreach(var inj in Injects)
            {
                inj.Write(foamRootPathName, monitor);
            }
            return true;
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Inject : IFoamObject
    {
        static int count = 0;
        public Inject()
        {
            Name = "inject" + count++;
            Active = false;
            Location = new Vector(0, 0, 0);
            StartTime = 0.0;
            Duration = 1.0;
        }

        public Inject(bool active, Vector location, double startTime, double duration)
        {
            Name = "inject" + count++;
            Active = active;
            Location = location;
            StartTime = startTime;
            Duration = duration;
        }
        [CategoryAttribute("喂丝设置"), DisplayNameAttribute("名称"), ReadOnly(true)]
        public string Name { get; set; }
        [CategoryAttribute("喂丝设置"), DisplayNameAttribute("激活")]
        public bool Active { get; set; }
        [CategoryAttribute("喂丝设置"), DisplayNameAttribute("喂丝位置")]
        public Vector Location { get; set; }
        [CategoryAttribute("喂丝设置"), DisplayNameAttribute("开始时间")]
        public double StartTime { get; set; }
        [CategoryAttribute("喂丝设置"), DisplayNameAttribute("喂丝时长")]
        public double Duration { get; set; }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetFvOptionsPath(foamRootPathName), monitor);
            f.Read();
            var d = f.Dictionary.GetByUrl("options").AddChild(Name);
            d.SetChild("type", "scalarSemiImplicitSource");
            d.SetChild("timeStart", StartTime);
            d.SetChild("duration", Duration);
            d.SetChild("selectionMode", "points");
            FoamDictionary points = new FoamDictionary(true, monitor);
            points.SetChild("0", Location.ToString());
            d.SetChild("points", points);
            d.SetChild("volumeMode", "absolute");
            FoamDictionary injectionRateSuSp = new FoamDictionary(monitor);
            injectionRateSuSp.SetChild("tracer", "(1 0)");
            d.SetChild("injectionRateSuSp", injectionRateSuSp);
            f.Write();
            return true;
        }
    }

}
