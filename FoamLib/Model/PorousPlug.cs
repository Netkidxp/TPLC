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
    public class PorousPlug : Plug
    {
        public PorousPlug(string patchName) : base(patchName)
        {
            this.plugType = PLUG_TYPE.PorousPlug;
        }
    }
}
