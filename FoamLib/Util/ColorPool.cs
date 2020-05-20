using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class ColorPool : List<Color>
    {
        int index = 0;
        public ColorPool(params Color[] pars)
        {
            foreach(var c in pars)
            {
                this.Add(c);
            }
        }
        public ColorPool()
        {
            this.AddRange(new List<Color>()
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.GreenYellow,
                Color.BlueViolet,
                Color.CadetBlue,
                Color.DarkBlue,
                Color.DeepPink,
                Color.DimGray,
                Color.DarkSlateBlue,
                Color.IndianRed,
                Color.MediumSpringGreen,
                Color.Salmon,
                Color.Teal
            });
        }
        public Color Next()
        {
            if (this.Count == 0)
                return Color.Black;
            if (index >= this.Count)
            {
                index = 0;
            }
            return this[index++];
        }
        public void Reset()
        {
            index = 0;
        }
    }

}
