using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public abstract class SerializerToolBase
    {
        protected string lastError = "";
        public string LastError { get => lastError; }
        public abstract bool ToFile(object obj, string fileName);
        public abstract object FromFile(string fileName);
    }
}
