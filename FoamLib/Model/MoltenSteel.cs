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
    public class MoltenSteel :Phase
    {
        double r;
        double rho0;

        public MoltenSteel(double molWeight, double cp, double hf, double mu, double pr, double r, double rho0) : base(molWeight, cp, hf, mu, pr)
        {
            this.r = r;
            this.rho0 = rho0;
        }

        public MoltenSteel() : base(Q235.MolWeight,Q235.Cp,Q235.Hf,Q235.Mu,Q235.Pr)
        {
            this.r = Q235.R;
            this.rho0 = Q235.Rho0;
        }
        [CategoryAttribute("Steel"), DisplayNameAttribute("R")]
        public double R { get => r; set => r = value; }

        [CategoryAttribute("Steel"), DisplayNameAttribute("基准密度")]
        public double Rho0 { get => rho0; set => rho0 = value; }
        [Browsable(false)]
        public static MoltenSteel Q235
        {
            get => new MoltenSteel(56,650,0,0.006293,2.289,3000,7000);
        }

        public override bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile fPP = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "phaseProperties"), monitor);
            fPP.Read();
            FoamDictionary dPpGas = fPP.Dictionary.GetByUrl("steel");
            dPpGas.SetChild("diameterModel", "constant");
            dPpGas.GetByUrl("constantCoeffs").SetChild("d", 1e-4);
            dPpGas.SetChild("residualAlpha", "1e-6");
            fPP.Write();
            FoamDictionaryFile fTppGas = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "thermophysicalProperties.steel"), monitor);
            fTppGas.Read();
            FoamDictionary dMixture = fTppGas.Dictionary.Child("mixture");
            dMixture.Child("specie").SetChild("molWeight", MolWeight);
            dMixture.Child("equationOfState").SetChild("R", R);
            dMixture.Child("equationOfState").SetChild("rho0", Rho0);
            dMixture.Child("thermodynamics").SetChild("Cp", Cp);
            dMixture.Child("thermodynamics").SetChild("Hf", Hf);
            dMixture.Child("transport").SetChild("mu", Mu);
            dMixture.Child("transport").SetChild("Pr", Pr);

            fTppGas.Write();
            return true;
        }
    }
}
