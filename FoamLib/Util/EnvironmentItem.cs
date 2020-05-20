using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EnvironmentItem
    {
        private string n;
        private string v;
        static int i = 0;
        public EnvironmentItem()
        {
            n = "env"+(i++);
            v = "";
        }

        public EnvironmentItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get => n; set => n = value; }
        public string Value { get => v; set => v = value; }
    }
}
