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
    public class SlitPlug : Plug
    {
        public SlitPlug(string patchName) : base(patchName)
        {
            this.plugType = PLUG_TYPE.SlitPlug;
            this.SlitLength = 0.03;
            this.SlitWidth = 0.0002;
            this.SlitCount = 10;
        }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("狭缝数量")]
        public uint SlitCount { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("狭缝长度")]
        public double SlitLength { get; set; }
        [CategoryAttribute("Boundary"), DisplayNameAttribute("狭缝宽度")]
        public double SlitWidth { get; set; }
        [CategoryAttribute("Boundary"), ReadOnlyAttribute(true), DisplayNameAttribute("狭缝面积占比")]
        public override double Alpha {
            get
            {
                return (SlitCount * SlitLength * SlitWidth)/(Math.PI * Radius * Radius);
            }
        }
        [CategoryAttribute("Boundary"), ReadOnlyAttribute(true), DisplayNameAttribute("狭缝名义半径")]
        public override double MeanSlitRadius {
            get
            {
                return Math.Sqrt(SlitWidth * SlitLength / Math.PI);
            }     
        }
    }
}
