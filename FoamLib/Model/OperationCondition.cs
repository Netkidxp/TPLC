using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;



namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OperationCondition : IFoamObject
    {
        Vector gravity;
        //double liquidLevel;
        //public Vector meshLB, meshRT;
        public OperationCondition()
        {
            gravity = new Vector(0, -9.81, 0);
            //liquidLevel = 1.0;
            //meshLB = new Vector(0, 0, 0);
            //meshRT = new Vector(1, 1, 1);
        }
        [CategoryAttribute("Operation Condition"), DisplayNameAttribute("重力")]
        public Vector Gravity { get => gravity; set => gravity = value; }
        /*
        [CategoryAttribute("Operation Condition")]
        public double LiquidLevel { get => liquidLevel; set => liquidLevel = value; }

        public void SetMeshLB(Vector v)
        {
            meshLB = v;
        }
        public void SetMeshRT(Vector v)
        {
            meshRT = v;
        }
        */
        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile fG = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName,"g"),monitor);
            fG.Read();
            fG.Dictionary.SetChild("value", gravity.ToString());
            fG.Write();
            /*
            FoamDictionaryFile fSFD = new FoamDictionaryFile(FoamConst.GetSetFieldDictFileName(foamRootPathName), monitor);
            fSFD.Read();
            Vector v1 = new Vector(Math.Min(meshLB.U,meshRT.U), Math.Min(meshLB.V, meshRT.V), Math.Min(meshLB.W, meshRT.W));
            Vector v2 = new Vector(Math.Max(meshLB.U, meshRT.U), Math.Max(meshLB.V, meshRT.V), Math.Max(meshLB.W, meshRT.W));
            if(gravity.U!=0)
            {
                if (gravity.U < 0)
                    v2.U = v1.U + liquidLevel;
                else
                    v1.U = v2.U - liquidLevel;

                v1.V = v1.V - 0.5;
                v2.V = v2.V + 0.5;
                v1.W = v1.W - 0.5;
                v2.W = v2.W + 0.5;
            }
            else if(gravity.V!=0)
            {
                if (gravity.V < 0)
                    v2.V = v1.V + liquidLevel;
                else
                    v1.V = v2.V - liquidLevel;

                v1.U = v1.U - 0.5;
                v2.U = v2.U + 0.5;
                v1.W = v1.W - 0.5;
                v2.W = v2.W + 0.5;
            }
            else
            {
                if (gravity.W < 0)
                    v2.W = v1.W + liquidLevel;
                else
                    v1.W = v2.W - liquidLevel;

                v1.U = v1.U - 0.5;
                v2.U = v2.U + 0.5;
                v1.V = v1.V - 0.5;
                v2.V = v2.V + 0.5;
            }
            fSFD.Dictionary.Child("defaultFieldValues").SetChild("volScalarFieldValue", "alpha.air 1");
            FoamDictionary dBTC = new FoamDictionary(monitor);
            dBTC.SetChild("box", v1.ToString() + " " + v2.ToString());
            dBTC.AddChild("fieldValues").SetChild("volScalarFieldValue", "alpha.air 0");
            fSFD.Dictionary.SetChild("regions", dBTC);
            fSFD.Write();
            */
            return true;
        }
    }
}
