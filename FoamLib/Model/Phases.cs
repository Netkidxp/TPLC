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
    public class Phases : IFoamObject
    {
        Gas gas;
        MoltenSteel steel;

        public Phases()
        {
            gas = Gas.Ar;
            steel = MoltenSteel.Q235;
        }

        public Phases(Gas gas, MoltenSteel steel)
        {
            this.gas = gas;
            this.steel = steel;
        }

        [CategoryAttribute("相"), DisplayNameAttribute("气体")]
        public Gas Gas { get => gas; set => gas = value; }
        [CategoryAttribute("相"), DisplayNameAttribute("钢液")]
        public MoltenSteel Steel { get => steel; set => steel = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            return this.gas.Write(foamRootPathName, monitor) && this.steel.Write(foamRootPathName, monitor);
        }
    }
}
