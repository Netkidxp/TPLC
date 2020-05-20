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
    public class FieldValues
    {
        double alpha;
        Vector v;
        double k;
        double epsilon;
        double t;

        public FieldValues()
        {
            alpha = 0.0;
            v = new Vector(0, 0, 0);
            k = 1e-5;
            epsilon = 1e-5;
            t = 300;
        }

        public FieldValues(double alpha, Vector v, double k, double epsilon, double t)
        {
            this.alpha = alpha;
            this.v = v;
            this.k = k;
            this.epsilon = epsilon;
            this.t = t;
        }
        [CategoryAttribute("Field Values"),Browsable(false)]
        public double Alpha { get => alpha; set => alpha = value; }

        [CategoryAttribute("Field Values"), DisplayNameAttribute("速度")]
        public Vector Velocity { get => v; set => v = value; }

        [CategoryAttribute("Field Values"), DisplayNameAttribute("湍动能")]
        public double K { get => k; set => k = value; }

        [CategoryAttribute("Field Values"), DisplayNameAttribute("湍动能耗散率")]
        public double Epsilon { get => epsilon; set => epsilon = value; }

        [CategoryAttribute("Field Values"), DisplayNameAttribute("温度")]
        public double Temperature { get => t; set => t = value; }
    }
}
