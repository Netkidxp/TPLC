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
    public class LadleGeometry
    {
        public LadleGeometry()
        {
            BottomDiameterX = BottomDiameterZ = 1.0;
            TopDiameterX = TopDiameterZ = 1.4;
            Height = 1.5;
            Plugs = new List<PlugGeometry>();
        }

        [CategoryAttribute("Geometry"), DisplayNameAttribute("包底X方向直径")]
        public double BottomDiameterX { get; set; }

        [CategoryAttribute("Geometry"), DisplayNameAttribute("包底Z方向直径")]
        public double BottomDiameterZ { get; set; }

        [CategoryAttribute("Geometry"), DisplayNameAttribute("包顶X方向直径")]
        public double TopDiameterX { get; set; }

        [CategoryAttribute("Geometry"), DisplayNameAttribute("包顶Z方向直径")]
        public double TopDiameterZ { get; set; }

        [CategoryAttribute("Geometry"), DisplayNameAttribute("高度")]
        public double Height { get; set; }

        [CategoryAttribute("Geometry"), DisplayNameAttribute("透气砖设置")]
        public List<PlugGeometry> Plugs { get; set; }

    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PlugGeometry
    {
        public PlugGeometry()
        {
            LocationX = 0;
            LocationZ = 0;
            Diameter = 0.01;
        }

        public PlugGeometry(double locationX, double locationY, double diameter)
        {
            LocationX = locationX;
            LocationZ = locationY;
            Diameter = diameter;
        }

        [CategoryAttribute("Plug"), DisplayNameAttribute("透气砖坐标X")]
        public double LocationX { get; set; }
        [CategoryAttribute("Plug"), DisplayNameAttribute("透气砖坐标Z")]
        public double LocationZ { get; set; }
        [CategoryAttribute("Plug"), DisplayNameAttribute("透气面直径")]
        public double Diameter { get; set; }

        public override string ToString()
        {
            return string.Format("[{0},{1},{2}]",LocationX,LocationZ,Diameter);
        }
    }
}
