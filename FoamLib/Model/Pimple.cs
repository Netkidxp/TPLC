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
    public class Pimple : IFoamObject
    {
        uint        nOuterCorrectors;
        uint        nCorrectors;
        uint        nNonOrthogonalCorrectors;
        bool        solveEnergy;

        public Pimple()
        {
            nOuterCorrectors = 3;
            nCorrectors = 1;
            nNonOrthogonalCorrectors = 0;
            solveEnergy = true;
        }

        public Pimple(uint nOutCorrectors, uint nCorrectors, uint nNonorthogonalCorrectors, bool solveEnergy)
        {
            this.nOuterCorrectors = nOutCorrectors;
            this.nCorrectors = nCorrectors;
            this.nNonOrthogonalCorrectors = nNonorthogonalCorrectors;
            this.solveEnergy = solveEnergy;
        }

        [CategoryAttribute("Pimple Option")]
        public uint NOuterCorrectors { get => nOuterCorrectors; set => nOuterCorrectors = value; }

        [CategoryAttribute("Pimple Option")]
        public uint NCorrectors { get => nCorrectors; set => nCorrectors = value; }

        [CategoryAttribute("Pimple Option")]
        public uint NNonOrthogonalCorrectors { get => nNonOrthogonalCorrectors; set => nNonOrthogonalCorrectors = value; }

        [CategoryAttribute("Pimple Option"), DisplayNameAttribute("求解能量")]
        public bool SolveEnergy { get => solveEnergy; set => solveEnergy = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetFvSolutionFileName(foamRootPathName), monitor);
            f.Read();
            FoamDictionary d = f.Dictionary.Child("PIMPLE");
            d.SetChild("nOuterCorrectors", nOuterCorrectors);
            d.SetChild("nCorrectors", nCorrectors);
            d.SetChild("nNonOrthogonalCorrectors", nNonOrthogonalCorrectors);
            d.SetChild("solveEnergy", solveEnergy ? "yes" : "no");
            f.Write();
            return true;

        }
    }
}
