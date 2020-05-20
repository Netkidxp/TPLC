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
    public class RelaxationFactors : IFoamObject
    {
        float p = 0.3f;
        float u = 0.5f;
        float k = 0.5f;
        float e = 0.5f;

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
                FoamDictionary df = f.Dictionary.GetByUrl("relaxationFactors%fields");
                FoamDictionary de = f.Dictionary.GetByUrl("relaxationFactors%equations");
                df.SetChild("p", P);
                de.SetChild("U", U);
                de.SetChild("k", K);
                de.SetChild("epsilon", E);
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
