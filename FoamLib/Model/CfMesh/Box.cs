using FoamLib.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model.CfMesh
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Box : AnalyticGeometry
    {
        public Box()
        {
            RegisterType("Box");
            Center = new Vector(0, 0, 0);
            LengthX = LengthY = LengthZ = 1.0f;
        }
        [NotifyParentProperty(true)]
        public Vector Center { get; set; }

        [NotifyParentProperty(true)]
        public float LengthX { get; set; }

        [NotifyParentProperty(true)]
        public float LengthY { get; set; }

        [NotifyParentProperty(true)]
        public float LengthZ { get; set; }

        public override void ToFoamDictionary(FoamDictionary fd)
        {
            fd.SetChild("type","box");
            fd.SetChild("centre", Center.ToString());
            fd.SetChild("lengthX", LengthX);
            fd.SetChild("lengthY", LengthY);
            fd.SetChild("lengthZ", LengthZ);
        }

        public override string ToString()
        {
            return "Box";
        }
    }
}
