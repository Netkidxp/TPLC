using FoamLib.IO;
using FoamLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tplc.Model
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SolveControl : IFoamObject
    {
        private uint maxStep = 100;

        [NotifyParentProperty(true),DisplayName("Maximum step count")]
        public uint MaxStep { get => maxStep; set => maxStep = value; }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            try
            {
                FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetControlDictFileName(foamRootPathName), monitor);
                f.Read();
                f.Dictionary.SetChild("endTime", MaxStep);
                f.Dictionary.SetChild("writeInterval", MaxStep);
                f.Write();
                return true;
            }
            catch (Exception e)
            {
                if (monitor != null)
                {
                    monitor.ErrorLine("write solve contorl error : " + e.Message);
                }
                return false;
            }
        }
        public override string ToString()
        {
            //return "MT:" + MaxStep;
            return "";
        }
    }
}
