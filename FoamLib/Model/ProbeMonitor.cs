using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.UI;

namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProbeMonitor : SolveMonitor
    {
        public ProbeMonitor() : base()
        {
            Location = new Vector(0, 0, 0);
            Fields = new List<string>();
        }

        public ProbeMonitor(string name) : base(name)
        {
            Location = new Vector(0, 0, 0);
            Fields = new List<string>();
        }
        public ProbeMonitor(string name, Vector location, List<string> fields) : base(name)
        {
            Location = location;
            Fields = fields;
        }

        [CategoryAttribute("Monitors"), DisplayNameAttribute("监视坐标")]
        public Vector Location { get; set; }

        [CategoryAttribute("Monitors"), EditorAttribute(typeof(FieldChooseEditor), typeof(System.Drawing.Design.UITypeEditor)), DisplayNameAttribute("场")]
        public List<string> Fields { get; set; }

        public override bool Write(string foamRootPathName, IMonitor monitor)
        {
            base.Write(foamRootPathName, monitor);
            FoamDictionaryFile controlFile = new FoamDictionaryFile(FoamConst.GetControlDictFileName(foamRootPathName), monitor);
            controlFile.Read();
            FoamDictionary dfun = controlFile.Dictionary.GetByUrl("functions");
            FoamDictionary m = new FoamDictionary(monitor);
            m.SetChild("type", "probes");
            m.SetChild("functionObjectLibs", "(\"libsampling.dll\")");
            m.SetChild("probeLocations", string.Format("({0})", Location.ToString()));
            FoamDictionary fields = new FoamDictionary(true, monitor);
            foreach(var f in Fields)
            {
                fields.SetChild(f, f);
            }
            m.SetChild("fields", fields);
            dfun.SetChild(Name, m);
            controlFile.Write();
            return true;
        }
    }
}
