using FoamLib.IO;
using FoamLib.Model;
using FoamLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tplc.Model.Boundary;
using Tplc.UI;

namespace Tplc.Model
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TplcCase : IFoamObject
    {


        public bool Write(string foamRootPathName, IMonitor monitor)
        {
            bool res = true;
            try
            {
                Dictionary<string, FoamDictionary> boundaryFieldDict = new Dictionary<string, FoamDictionary>();
                Dictionary<string, FoamDictionaryFile> fieldFoamFile = new Dictionary<string, FoamDictionaryFile>();
                boundaryFieldDict.Clear();
                fieldFoamFile.Clear();
                foreach (string field in GlobalModelObject.fieldNames)
                {
                    FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, field), monitor);
                    f.Read();
                    fieldFoamFile.Add(field, f);
                    boundaryFieldDict.Add(field, f.Dictionary.GetByUrl("boundaryField"));
                }
                foreach (BoundaryBase b in Boundarys)
                {
                    PolyMesh mesh = new PolyMesh(FoamConst.GetVxtFilePath(foamRootPathName));
                    if (b is Inlet || b is Outlet)
                    {
                        mesh.SetBoundaryType(b.PatchName, PolyMesh.BoundaryType.patch);
                    }
                    else if(b is Tplc.Model.Boundary.Wall)
                    {
                        mesh.SetBoundaryType(b.PatchName, PolyMesh.BoundaryType.wall);
                    }
                    res = b.Update(boundaryFieldDict);
                    if(!res)
                    {
                        if (monitor != null)
                        {
                            monitor.ErrorLine("Write boundary to foam error, boundary: " + b.PatchName);
                        }
                        return false;
                    }
                }
                foreach (FoamDictionaryFile f in fieldFoamFile.Values)
                {
                    f.Write();
                }
                res = MaterialProperty.Write(foamRootPathName, monitor);
                if (!res)
                {
                    if (monitor != null)
                    {
                        monitor.ErrorLine("Write gas proprety to foam error !");
                    }
                    return false;
                }
                res = InitlizeValue.Write(foamRootPathName, monitor);
                if (!res)
                {
                    if (monitor != null)
                    {
                        monitor.ErrorLine("Write initlize value to foam error !");
                    }
                    return false;
                }
                res = ResidualContorl.Write(foamRootPathName, monitor);
                if (!res)
                {
                    if (monitor != null)
                    {
                        monitor.ErrorLine("Write residual control to foam error !");
                    }
                    return false;
                }
                res = SolveContorl.Write(foamRootPathName, monitor);
                if (!res)
                {
                    if (monitor != null)
                    {
                        monitor.ErrorLine("Write solve control to foam error !");
                    }
                    return false;
                }
                res = RelaxationFactors.Write(foamRootPathName, monitor);
                if (!res)
                {
                    if (monitor != null)
                    {
                        monitor.ErrorLine("Write Relaxation factors to foam error !");
                    }
                    return false;
                }
            }
            catch(Exception e)
            {
                if(monitor!=null)
                {
                    monitor.ErrorLine("Update case error : " + e.Message);
                }
                return false;
            }
            return true;

        }
        private List<BoundaryBase> boundarys = new List<BoundaryBase>();
        private MaterialProperty materialProperty = new MaterialProperty();
        private InitlizeValue initlizeValue = new InitlizeValue();
        private ResidualControl residualContorl = new ResidualControl();
        private SolveControl solveContorl = new SolveControl();
        private RelaxationFactors relaxationFactors = new RelaxationFactors();
        private CaseState state = new CaseState();

        [CategoryAttribute("Model"),DisplayName("2 Boundary conditions"),Editor(typeof(TplcBoundaryEditor),typeof(UITypeEditor))]
        public List<BoundaryBase> Boundarys { get => boundarys; set => boundarys = value; }

        [CategoryAttribute("Model"), DisplayName("1 Material proprety"), Editor(typeof(MaterialEditor),typeof(UITypeEditor))]
        public MaterialProperty MaterialProperty { get => materialProperty; set => materialProperty = value; }

        [CategoryAttribute("Model"), DisplayName("3 Initlizion")]
        public InitlizeValue InitlizeValue { get => initlizeValue; set => initlizeValue = value; }

        [CategoryAttribute("Model"), DisplayName("5 Residual control")]
        public ResidualControl ResidualContorl { get => residualContorl; set => residualContorl = value; }

        [CategoryAttribute("Model"), DisplayName("4 Relaxation Factors")]
        public RelaxationFactors RelaxationFactors { get => relaxationFactors; set => relaxationFactors = value; }

        [CategoryAttribute("Model"), DisplayName("6 Solve contorl")]
        public SolveControl SolveContorl { get => solveContorl; set => solveContorl = value; }

        [Browsable(false)]
        public CaseState State { get => state; set => state = value; }

        public override string ToString()
        {
            return "Case";
        }

    }

  
}
