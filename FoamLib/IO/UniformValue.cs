using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.IO
{
    public class UniformValue<T>
    {
        T value;
        public UniformValue(T value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "uniform " + value.ToString();
        }
    }
}
