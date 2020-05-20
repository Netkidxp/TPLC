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
    public class ResidualControl : IFoamObject
    {
        float p = 1e-5f;
        float u = 1e-5f;
        float k = 1e-5f;
        float e = 1e-5f;

        [NotifyParentProperty(true)]
        public float P { get => p; set => p = value; }

        [NotifyParentProperty(true)]
        public float U { get => u; set => u = value; }

        [NotifyParentProperty(true)]
        public float K { get => k; set => k = value; }

        [NotifyParentProperty(true)]
        public float E { get => e; set => e = value; }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            try
            {
                FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetFvSolutionFileName(foamRootPathName), monitor);
                f.Read();
                FoamDictionary d = f.Dictionary.GetByUrl("SIMPLE%residualControl");
                d.SetChild("p", P);
                d.SetChild("U", U);
                d.SetChild("k", K);
                d.SetChild("epsilon", E);
                f.Write();
                return true;
            }
            catch (Exception e)
            {
                if (monitor != null)
                {
                    monitor.ErrorLine("write residual control error : " + e.Message);
                }
                return false;
            }
        }
        public override string ToString()
        {
            //return "U:" + U + " p:" + P + " k:" + K + " e:" + E;
            return "";
        }
    }
}
