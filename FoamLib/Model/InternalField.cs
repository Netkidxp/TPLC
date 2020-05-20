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
    public class InternalField : IFoamObject
    {
        FieldValues gasInternalField = new FieldValues(0,new Vector(0,0,0),3.75e-5,1e-5,300.0);
        FieldValues steelInternalField = new FieldValues(1, new Vector(0, 0, 0), 3.75e-5, 1.5e-4, 1873.15);
        Vector steelInitlizePoint1 = new Vector(0, 0, 0);
        Vector steelInitlizePoint2 = new Vector(1, 1, 1);

        readonly Dictionary<string, FoamDictionary> internalFieldDict = new Dictionary<string, FoamDictionary>();
        readonly Dictionary<string, FoamDictionaryFile> fieldFoamFile = new Dictionary<string, FoamDictionaryFile>();

        [CategoryAttribute("Field"), DisplayNameAttribute("气体")]
        public FieldValues Gas { get => gasInternalField; set => gasInternalField = value; }

        [CategoryAttribute("Field"), DisplayNameAttribute("钢液")]
        public FieldValues Steel { get => steelInternalField; set => steelInternalField = value; }

        [CategoryAttribute("Field"), DisplayNameAttribute("钢液初始化坐标1")]
        public Vector SteelInitlizePoint1 { get => steelInitlizePoint1; set => steelInitlizePoint1 = value; }

        [CategoryAttribute("Field"), DisplayNameAttribute("钢液初始化坐标2")]
        public Vector SteelInitlizePoint2 { get => steelInitlizePoint2; set => steelInitlizePoint2 = value; }

        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            internalFieldDict.Clear();
            fieldFoamFile.Clear();
            foreach (string field in Case.fieldNames)
            {
                FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, field), monitor);
                f.Read();
                fieldFoamFile.Add(field, f);
                internalFieldDict.Add(field, f.Dictionary.GetByUrl("internalField"));
            }
            internalFieldDict["alpha.gas"].Data = "uniform " + gasInternalField.Alpha;
            internalFieldDict["alphat.gas"].Data = "uniform " + 0;
            internalFieldDict["alphat.steel"].Data = "uniform " + 0;
            internalFieldDict["epsilon.gas"].Data = "uniform " + gasInternalField.Epsilon;
            internalFieldDict["epsilon.steel"].Data = "uniform " + steelInternalField.Epsilon;
            internalFieldDict["epsilonm"].Data = "uniform " + steelInternalField.Epsilon;
            internalFieldDict["k.gas"].Data = "uniform " + gasInternalField.K;
            internalFieldDict["k.steel"].Data = "uniform " + steelInternalField.K;
            internalFieldDict["km"].Data = "uniform " + steelInternalField.K;
            internalFieldDict["nut.gas"].Data = "uniform " + 1e-8;
            internalFieldDict["nut.steel"].Data = "uniform " + 1e-8;
            internalFieldDict["p"].Data = "uniform " + 1e5;
            internalFieldDict["p_rgh"].Data = "uniform " + 1e5;
            internalFieldDict["T.gas"].Data = "uniform " + gasInternalField.Temperature;
            internalFieldDict["T.steel"].Data = "uniform " + steelInternalField.Temperature;
            internalFieldDict["U.gas"].Data = "uniform " + gasInternalField.Velocity;
            internalFieldDict["U.steel"].Data = "uniform " + steelInternalField.Velocity;
            internalFieldDict["Theta"].Data = "uniform " + 0;

            foreach (FoamDictionaryFile f in fieldFoamFile.Values)
            {
                f.Write();
            }

            FoamDictionaryFile fSFD = new FoamDictionaryFile(FoamConst.GetSetFieldDictFileName(foamRootPathName), monitor);
            fSFD.Read();
            FoamDictionary dDefaultFieldValues = new FoamDictionary(true, monitor);
            dDefaultFieldValues.SetChild("0", "volScalarFieldValue");
            dDefaultFieldValues.SetChild("1", "alpha.gas");
            dDefaultFieldValues.SetChild("2", 1);
            fSFD.Dictionary.SetChild("defaultFieldValues", dDefaultFieldValues);

            FoamDictionary dFieldValues = new FoamDictionary(true,monitor);
            dFieldValues.SetChild("0", "volScalarFieldValue");
            dFieldValues.SetChild("1", "alpha.gas");
            dFieldValues.SetChild("2", 0);

            FoamDictionary dBoxToCell = new FoamDictionary(monitor);
            dBoxToCell.SetChild("box", steelInitlizePoint1.ToString() + " " + steelInitlizePoint2.ToString());
            dBoxToCell.SetChild("fieldValues", dFieldValues);

            FoamDictionary dRegions = new FoamDictionary(true, monitor);
            dRegions.SetChild("boxToCell", dBoxToCell);
            fSFD.Dictionary.SetChild("regions", dRegions);

            fSFD.Write();

            return true;
        }
    }
}
