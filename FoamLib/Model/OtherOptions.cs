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
    public class OtherOptions : IFoamObject
    {
        List<EntryOption> entryOptions;
        String readme;

        public OtherOptions()
        {
            this.entryOptions = new List<EntryOption>();
            readme = "";
        }

        public OtherOptions(List<EntryOption> entryOptions, string readme)
        {
            this.entryOptions = entryOptions;
            this.readme = readme;
        }

        [CategoryAttribute("Others"), DisplayNameAttribute("附加键")]
        public List<EntryOption> EntryOptions { get => entryOptions; set => entryOptions = value; }

        [CategoryAttribute("Others"), DisplayNameAttribute("注释")]
        public string Readme { get => readme; set => readme = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            foreach(EntryOption eo in entryOptions)
            {
                eo.Write(foamRootPathName, monitor);
            }
            return true;
        }
    }
}
