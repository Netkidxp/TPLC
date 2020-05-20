using FoamLib.IO;
using FoamLib.UI.Post.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Util
{
    public abstract class UnitReader
    {
        protected List<Unit> unitList = new List<Unit>();
        protected UnitReader()
        {
        }
        public List<Unit> UnitList { get => unitList;}
        public abstract bool Read(string fileName, IMonitor mon = null);
    }
}
