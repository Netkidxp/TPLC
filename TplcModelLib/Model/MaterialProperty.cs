using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.Model;

namespace Tplc.Model
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MaterialProperty : IFoamObject
    {
        
        private float viscosity = 1.79e-5f;
        private float density = 1.293f;
        [Browsable(false)]
        public float Nu { get => Viscosity/Density;}
        public float Viscosity { get => viscosity; set => viscosity = value; }
        public float Density { get => density; set => density = value; }

        private const string DIM = "[0 2 -1 0 0 0 0]";

        public MaterialProperty()
        {
        }

        public MaterialProperty(float density,float viscosity)
        {
            Viscosity = viscosity;
            Density = density;
        }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            try
            {
                FoamDictionaryFile fdf = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "transportProperties"), monitor);
                fdf.Read();
                fdf.Dictionary.SetChild("nu", DIM + " " + Nu);
                fdf.Write();
                return true;
            }
            catch(Exception e)
            {
                monitor.ErrorLine(e.Message);
                return false;
            }
        }

        public override string ToString()
        {
            //return "Den:"+density + " Vis:" + viscosity;
            return "";
        }
    }
}
