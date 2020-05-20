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
    public class Phase : IFoamObject
    {
        double molWeight;
        double cp;
        double hf;
        double mu;
        double pr;

        public Phase(double molWeight, double cp, double hf, double mu, double pr)
        {
            this.molWeight = molWeight;
            this.cp = cp;
            this.hf = hf;
            this.mu = mu;
            this.pr = pr;
        }
        [CategoryAttribute("Phase"), DisplayNameAttribute("摩尔质量")]
        public double MolWeight { get => molWeight; set => molWeight = value; }
        [CategoryAttribute("Phase"), DisplayNameAttribute("等压比热容")]
        public double Cp { get => cp; set => cp = value; }
        [CategoryAttribute("Phase")]
        public double Hf { get => hf; set => hf = value; }
        [CategoryAttribute("Phase"), DisplayNameAttribute("动力粘度")]
        public double Mu { get => mu; set => mu = value; }
        [CategoryAttribute("Phase"), DisplayNameAttribute("普朗特数")]
        public double Pr { get => pr; set => pr = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            return true;
        }
    }
}
