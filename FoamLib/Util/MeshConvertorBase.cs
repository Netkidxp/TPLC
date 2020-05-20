using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    abstract public class MeshConvertorBase
    {
        protected FoamRunner runner = null;
        protected IMonitor monitor = null;
        protected string input = "";

        public MeshConvertorBase(FoamRunner runner, IMonitor monitor)
        {
            this.runner = runner;
            this.monitor = monitor;
        }

        public abstract bool ChooseInput();
        public abstract void StartConvert(string meshRoot);
    }
}
