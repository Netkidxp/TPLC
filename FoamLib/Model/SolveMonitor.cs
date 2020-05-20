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
    public class SolveMonitor : IFoamObject
    {
        static int count = 0;
        public SolveMonitor()
        {
            Name = "monitor" + count++;
        }

        public SolveMonitor(string name)
        {
            Name = name;
        }

        [CategoryAttribute("Monitors"), DisplayNameAttribute("名称")]
        public string Name { get; set; }
        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            //throw new NotImplementedException();
            return true;
        }
    }
}
