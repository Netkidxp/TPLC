using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    public interface IFoamObject
    {
        bool Write(String foamRootPathName, IMonitor monitor);
    }
}
