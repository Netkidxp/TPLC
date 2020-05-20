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
    public class WallLayer
    {
        public WallLayer()
        {
            Name = "层";
            Thickness = 0.01;
            Conductivity = 3.2;
        }

        [CategoryAttribute("Boundary"), DisplayNameAttribute("名称")]
        public string Name { get; set; }

        [CategoryAttribute("Boundary"), DisplayNameAttribute("厚度")]
        public double Thickness { get; set; }

        [CategoryAttribute("Boundary"), DisplayNameAttribute("导热系数")]
        public double Conductivity { get; set; }
    }
}
