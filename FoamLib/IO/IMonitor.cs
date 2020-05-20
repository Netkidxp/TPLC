using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.IO
{
    public interface IMonitor
    {
        void ErrorLine(string err);
        void LogLine(string log);
        void Progress(float point);
    }
}
