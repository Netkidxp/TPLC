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
    public abstract class AnalyticGeometry
    {
        private static int i = 0;
        private string typeName;
        public AnalyticGeometry()
        {
            Name = "geometry" + (i++);
        }
        public string Name { get; set; }
        public static string[] TypeNames
        {
            get
            {
                return new string[] { "Box", "Sphere", "Cone" };
            }
        }
        [Browsable(false)]
        public string TypeName { get => typeName;}
        protected void RegisterType(string name)
        {
            typeName = name;
        }
        public abstract void ToFoamDictionary(FoamDictionary fd);
    }
}
