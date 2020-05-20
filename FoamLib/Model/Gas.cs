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
    public class Gas : Phase
    {
        double d0;
        double p0;
        
        public Gas(double molWeight, double cp, double hf, double mu, double pr, double d0, double p0) : base(molWeight, cp, hf, mu, pr)
        {
            this.d0 = d0;
            this.p0 = p0;
        }
        public Gas() : base(Ar.MolWeight,Ar.Cp,Ar.Hf,Ar.Mu,Ar.Pr)
        {
            this.d0 = Ar.D0;
            this.p0 = Ar.P0;
        }
        [Browsable(false)]
        public static Gas Ar
        {
            get => new Gas(40, 520, 0, 2.244e-5, 0.65, 0.006, 1e5);
        }
        [CategoryAttribute("Gas"), DisplayNameAttribute("基准密度")]
        public double D0 { get => d0; set => d0 = value; }
        [CategoryAttribute("Gas"), DisplayNameAttribute("参考压力")]
        public double P0 { get => p0; set => p0 = value; }

        public override bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile fPP = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "phaseProperties"), monitor);
            fPP.Read();
            FoamDictionary dPpGas = fPP.Dictionary.GetByUrl("gas");
            dPpGas.SetChild("diameterModel", "isothermal");
            dPpGas.GetByUrl("isothermalCoeffs").SetChild("d0", D0);
            dPpGas.GetByUrl("isothermalCoeffs").SetChild("p0", P0);
            dPpGas.SetChild("residualAlpha", "1e-6");
            fPP.Write();
            FoamDictionaryFile fTppGas = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "thermophysicalProperties.gas"), monitor);
            fTppGas.Read();
            FoamDictionary dMixture = fTppGas.Dictionary.Child("mixture");
            dMixture.Child("specie").SetChild("molWeight", MolWeight);
            dMixture.Child("thermodynamics").SetChild("Cp", Cp);
            dMixture.Child("thermodynamics").SetChild("Hf", Hf);
            dMixture.Child("transport").SetChild("mu", Mu);
            dMixture.Child("transport").SetChild("Pr", Pr);

            fTppGas.Write();
            return true;
        }
    }
    
}
