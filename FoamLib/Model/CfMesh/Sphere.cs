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
    public class Sphere : AnalyticGeometry
    {
        public Sphere()
        {
            RegisterType("Sphere");
            Center = new Vector(0, 0, 0);
            Radius = 1.0f;
        }
        [NotifyParentProperty(true)]
        public Vector Center { get; set; }

        [NotifyParentProperty(true)]
        public float Radius { get; set; }

        public override string ToString()
        {
            return "Sphere";
        }
        public override void ToFoamDictionary(FoamDictionary fd)
        {
            fd.SetChild("type", "sphere");
            fd.SetChild("centre",Center.ToString());
            fd.SetChild("radius", Radius);
        }
    }
}
