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
    public class InitlizeValue : IFoamObject
    {
        private Vector velocity = new Vector(0, 0, 0);
        private float pressure = 0;
        private float k = 0.01f;
        private float epsilon = 0.1f;

        [NotifyParentProperty(true)]
        public Vector Velocity { get => velocity; set => velocity = value; }

        [NotifyParentProperty(true)]
        public float Pressure { get => pressure; set => pressure = value; }

        [NotifyParentProperty(true)]
        public float K { get => k; set => k = value; }

        [NotifyParentProperty(true)]
        public float Epsilon { get => epsilon; set => epsilon = value; }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            try
            {
                FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, "p"), monitor);
                f.Read();
                f.Dictionary.SetChild("internalField", new UniformValue<float>(Pressure).ToString());
                f.Write();

                f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, "U"), monitor);
                f.Read();
                f.Dictionary.SetChild("internalField", new UniformValue<Vector>(Velocity).ToString());
                f.Write();

                f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, "k"), monitor);
                f.Read();
                f.Dictionary.SetChild("internalField", new UniformValue<float>(K).ToString());
                f.Write();

                f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, "epsilon"), monitor);
                f.Read();
                f.Dictionary.SetChild("internalField", new UniformValue<float>(Epsilon).ToString());
                f.Write();

                return true;
            }
            catch(Exception e)
            {
                if(monitor!=null)
                {
                    monitor.ErrorLine("write initlize value error : " + e.Message);
                }
                return false;
            }

        }
        public override string ToString()
        {
            //return "U:" + Velocity + " p:"+Pressure+" k:"+K + " e:" + Epsilon;
            return "";
        }
    }
}
