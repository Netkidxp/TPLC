using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model
{
    public static class GlobalModelObject
    {
        public static List<string> patchNames = new List<string>();
        public static List<string> fieldNames = new List<string>() { "p", "U", "k", "epsilon", "nut"};
    }
}
