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
    public class Cone : AnalyticGeometry
    {
        public Cone()
        {
            RegisterType("Cone");
            Point0 = new Vector(0, 0, 0);
            Point1 = new Vector(1, 0, 0);
            Radius0 = Radius1 = 1.0f;
        }
        [NotifyParentProperty(true)]
        public Vector Point0 { get; set; }

        [NotifyParentProperty(true)]
        public Vector Point1 { get; set; }

        [NotifyParentProperty(true)]
        public float Radius0 { get; set; }

        [NotifyParentProperty(true)]
        public float Radius1 { get; set; }

        public override string ToString()
        {
            return "Cone";
        }
        public override void ToFoamDictionary(FoamDictionary fd)
        {
            fd.SetChild("type", "cone");
            fd.SetChild("p0", Point0.ToString());
            fd.SetChild("p1", Point1.ToString());
            fd.SetChild("radius0", Radius0);
            fd.SetChild("radius1", Radius1);
        }
    }
}
