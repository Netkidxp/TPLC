using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    public interface IBoundaryObject
    {
        bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null);
    }
}
