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
    public class Vector
    {
        double u;
        double v;
        double w;

        public Vector(double[] data)
        {
            if(data.Length == 3)
            {
                u = data[0];
                v = data[1];
                w = data[2];
            }
            else
            {
                u = v = w = 0;
            }
        }
        public Vector(double u, double v, double w)
        {
            this.u = u;
            this.v = v;
            this.w = w;
        }
        public event EventHandler OnPropertyChanged;
        [NotifyParentProperty(true)]
        public double V1
        {
            get => u;
            set
            {
                u = value;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        [NotifyParentProperty(true)]
        public double V2
        {
            get => v;
            set
            {
                v = value;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        [NotifyParentProperty(true)]
        public double V3
        {
            get => w;
            set
            {
                w = value;
                OnPropertyChanged?.Invoke(this, new EventArgs());
            }
        }
        
        public override string ToString()
        {
            return String.Format("({0} {1} {2})",u,v,w);
        }
        public double[] ToBlock()
        {
            return new double[] { u, v, w };
        }
    }
}
