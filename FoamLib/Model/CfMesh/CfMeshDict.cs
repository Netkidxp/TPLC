using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.UI;
using Newtonsoft.Json;

namespace FoamLib.Model.CfMesh
{
    public enum _MeshType
    {
        Hexahedron,
        Tetrahedron,
        Polyhedron
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CfMeshDict : IFoamObject
    {
        public CfMeshDict()
        {
            //Geometry = new List<string>();
            MaxCellSize = 1.0f;
            BoundaryCellSize = 0.5f;
            MinCellSize = 0.1f;
            EnforceGeometryConstraints = false;
            MeshType = _MeshType.Polyhedron;
            LocalRefine = new List<LocalRefinementSubDict>();
            ObjectRefine = new List<ObjectRefinementSubDict>();
            SurfaceRefine = new List<SurfaceRefinementSubDict>();
            PatchConfig = new List<PatchConfigSubDict>();
            GlobalBoundaryLayerParameters = new BoundaryLayerParameters();
            PatchBoundaryLayerParameters = new List<PatchBoundaryLayerSubDict>();
        }

        public CfMeshDict(float maxCellSize, float boundaryCellSize, float minCellSize) : this()
        {
            MaxCellSize = maxCellSize;
            BoundaryCellSize = boundaryCellSize;
            MinCellSize = minCellSize;
        }

        public bool Write(string foamRootPathName,IMonitor monitor=null)
        {
            try
            {
                string dictFileName = FoamConst.GetCfMeshDictFileName(foamRootPathName);
                FoamDictionaryFile f = new FoamDictionaryFile(dictFileName, monitor);
                f.Read();
                f.Dictionary.SetChild("surfaceFile", "\"surface.stl\"");
                f.Dictionary.SetChild("maxCellSize", MaxCellSize);
                f.Dictionary.SetChild("minCellSize", MinCellSize);
                f.Dictionary.SetChild("boundaryCellSize", BoundaryCellSize);
                f.Dictionary.SetChild("enforceGeometryConstraints", EnforceGeometryConstraints ? "1" : "0");
                FoamDictionary dLocalRefine = f.Dictionary.GetByUrl("localRefinement");
                dLocalRefine.Clear();
                foreach(LocalRefinementSubDict l in LocalRefine)
                {
                    l.ToFoamDictionary(dLocalRefine);
                }
                FoamDictionary dObjectRefine = f.Dictionary.GetByUrl("objectRefinements");
                dObjectRefine.Clear();
                foreach(ObjectRefinementSubDict o in ObjectRefine)
                {
                    o.ToFoamDictionary(dObjectRefine);
                }
                FoamDictionary dSurfaceRefine = f.Dictionary.GetByUrl("surfaceMeshRefinement");
                dSurfaceRefine.Clear();
                foreach(SurfaceRefinementSubDict s in SurfaceRefine)
                {
                    s.ToFoamDictionary(dSurfaceRefine);
                }
                FoamDictionary dPatchName = f.Dictionary.GetByUrl("renameBoundary");
                dPatchName.Clear();
                dPatchName.SetChild("defaultName", "walls");
                dPatchName.SetChild("defaultType", "wall");
                FoamDictionary dNewPatchNames = dPatchName.GetByUrl("newPatchNames");
                dNewPatchNames.Clear();
                foreach(PatchConfigSubDict p in PatchConfig)
                {
                    p.ToFoamDictionary(dNewPatchNames);
                }
                FoamDictionary dBoundaryLayers = f.Dictionary.GetByUrl("boundaryLayers");
                dBoundaryLayers.Clear();
                if(GlobalBoundaryLayerParameters.NLayers !=0 )
                {
                    GlobalBoundaryLayerParameters.ToFoamDictionary(dBoundaryLayers);
                }
                FoamDictionary dPatchBoundaryLayers = dBoundaryLayers.GetByUrl("patchBoundaryLayers");
                dPatchBoundaryLayers.Clear();
                foreach(PatchBoundaryLayerSubDict p in PatchBoundaryLayerParameters)
                {
                    p.ToFoamDictionary(dPatchBoundaryLayers);
                }
                f.Write(dictFileName);
                return true;
            }
            catch(Exception e)
            {
                if (monitor != null)
                    monitor.ErrorLine(e.Message);
                return false;
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class LocalRefinementSubDict
        {
            public LocalRefinementSubDict()
            {
                PartNames = new List<string>();
                Parameters = new RefinementParameters();
            }

            [CategoryAttribute("Local"), DisplayName("Part names"),Editor(typeof(SurfacePartChooseEditor),typeof(UITypeEditor))]
            public List<string> PartNames { get; set; }

            [CategoryAttribute("Local"), DisplayName("Refinement parameters")]
            public RefinementParameters Parameters { get; set; }

            public void ToFoamDictionary(FoamDictionary dLocalRefine)
            {
                foreach (string pn in PartNames)
                {
                    FoamDictionary dpn = dLocalRefine.GetByUrl(pn);
                    Parameters.ToFoamDictionary(dpn);
                }
            }
            public override string ToString()
            {
                string ts = "[ ";
                foreach (var ps in PartNames)
                    ts += ps + " ";
                return ts+"]";
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ObjectRefinementSubDict
        {
            public ObjectRefinementSubDict()
            {
                Geometry = new Box();
                Parameters = new RefinementParameters();
            }

            [CategoryAttribute("Object"), DisplayName("Analytic geometry"), /*Editor(typeof(AnalyticGeometryEditor), typeof(UITypeEditor)),*/ NotifyParentProperty(true)]
            public AnalyticGeometry Geometry { get; set; }

            [CategoryAttribute("Object"), DisplayName("Refinement parameters")]
            public RefinementParameters Parameters { get; set; }

            public void ToFoamDictionary(FoamDictionary dObjectRefine)
            {
                FoamDictionary dor = dObjectRefine.GetByUrl(Geometry.Name);
                Geometry.ToFoamDictionary(dor);
                Parameters.ToFoamDictionary(dor);
            }
            public override string ToString()
            {
                return Geometry.Name + "-" + Geometry.TypeName;
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class SurfaceRefinementSubDict
        {
            static int i = 0;
            public SurfaceRefinementSubDict()
            {
                Parameters = new RefinementParameters();
                SurfaceFile = "";
                RegionName = "region" + (i++);
            }

            [CategoryAttribute("Surface"), DisplayName("Region name")]
            public string RegionName { get; set; }

            [CategoryAttribute("Surface"), DisplayName("Surface file"), Editor(typeof(SingleFileChooseEditor), typeof(UITypeEditor))]
            public string SurfaceFile { get; set; }

            [CategoryAttribute("Surface"), DisplayName("Refinement parameters")]
            public RefinementParameters Parameters { get; set; }

            public void ToFoamDictionary(FoamDictionary dSurfaceRefine)
            {
                FoamDictionary ds = dSurfaceRefine.GetByUrl(RegionName);
                ds.SetChild("surfaceFile", SurfaceFile);
                Parameters.ToFoamDictionary(ds);
            }

            public override string ToString()
            {
                return SurfaceFile;
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class RefinementParameters
        {
            public RefinementParameters()
            {
                AdditionalRefinementLevels = 0;
                RefinementThickness = 0;
                CellSize = 1;
            }

            [DisplayName("Additional refinement levels")]
            public uint AdditionalRefinementLevels { get; set; }

            [DisplayName("Refinement thickness")]
            public float RefinementThickness { get; set; }

            [DisplayName("Cell size")]
            public float CellSize { get; set; }

            public void ToFoamDictionary(FoamDictionary fo)
            {
                fo.SetChild("additionalRefinementLevels", AdditionalRefinementLevels);
                fo.SetChild("refinementThickness", RefinementThickness);
                fo.SetChild("cellSize", CellSize);
            }
            public override string ToString()
            {
                return "AF:" + AdditionalRefinementLevels + ", RT:" + RefinementThickness + ", CS:" + CellSize;
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class PatchConfigSubDict
        {
            private static int i = 0;
            public PatchConfigSubDict()
            {
                NewName = "patch" + (i++);
                PartName = "";
            }

            [DisplayName("Part name"),TypeConverter(typeof(SurfacePartNameConverter))]
            public string PartName { get; set; }

            [DisplayName("New name")]
            public string NewName { get; set; }

            public void ToFoamDictionary(FoamDictionary fp)
            {
                FoamDictionary fpn = fp.GetByUrl(PartName);
                fpn.SetChild("newName", NewName);
                fpn.SetChild("type", "patch");
            }
            public override string ToString()
            {
                return PartName + " => " + NewName;
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class BoundaryLayerParameters
        {
            public BoundaryLayerParameters()
            {
                NLayers = 0;
                ThicknessRatio = 1.2f;
                MaxFirstLayerThickness = 0.001f;
            }

            [DisplayName("Number of layers")]
            public uint NLayers { get; set; }

            [DisplayName("Thickness ratio")]
            public float ThicknessRatio { get; set; }

            [DisplayName("Max first layer thickness")]
            public float MaxFirstLayerThickness { get; set; }

            public void ToFoamDictionary(FoamDictionary fp)
            {
                fp.SetChild("nLayers", NLayers);
                fp.SetChild("thicknessRatio", ThicknessRatio);
                fp.SetChild("maxFirstLayerThickness", MaxFirstLayerThickness);
            }
            public override string ToString()
            {
                //return "NL:" + NLayers+ ", TR:" + ThicknessRatio+", MT:"+MaxFirstLayerThickness;
                return "";
            }
        }

        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class PatchBoundaryLayerSubDict
        {
            public PatchBoundaryLayerSubDict()
            {
                Parameters = new BoundaryLayerParameters();
                PartName = "";
            }
            public override string ToString()
            {
                return PartName;
            }

            [DisplayName("Part name"), TypeConverter(typeof(SurfacePartNameConverter))]
            public string PartName { get; set; }
            
            [DisplayName("Parameters")]
            public BoundaryLayerParameters Parameters { get; set; }

            public void ToFoamDictionary(FoamDictionary fp)
            {
                FoamDictionary d = fp.GetByUrl(PartName);
                Parameters.ToFoamDictionary(d);
            }
        }


        //[CategoryAttribute("1 Global"), DisplayName("Geometry files"), Editor(typeof(FileChooseEditor),typeof(UITypeEditor))]
        // public List<string> Geometry { get; set; }
        [CategoryAttribute("1 Global"), DisplayName("1 Mesh Type")]
        public _MeshType MeshType { get; set; }

        [CategoryAttribute("1 Global"), DisplayName("2 Maximum cell size")]
        public float MaxCellSize { get; set; }

        [CategoryAttribute("1 Global"), DisplayName("3 Boundary cell size")]
        public float BoundaryCellSize { get; set; }

        [CategoryAttribute("1 Global"), DisplayName("4 Minimum cell size")]
        public float MinCellSize { get; set; }

        [CategoryAttribute("1 Global"), DisplayName("5 Enforce Geometry Constraints"), Browsable(false)]
        public bool EnforceGeometryConstraints { get; set; }

        [CategoryAttribute("2 Patch"), DisplayName("1 Patch config")]
        public List<PatchConfigSubDict> PatchConfig { get; set; }

        [CategoryAttribute("3 Refinement"), DisplayName("2 Local")]
        public List<LocalRefinementSubDict> LocalRefine { get; set; }

        [CategoryAttribute("3 Refinement"), DisplayName("3 Object"), Editor(typeof(RefineObjectListEdit),typeof(UITypeEditor))]
        public List<ObjectRefinementSubDict> ObjectRefine { get; set; }

        [CategoryAttribute("3 Refinement"), DisplayName("4 Surface")]
        public List<SurfaceRefinementSubDict> SurfaceRefine { get; set; }

        [CategoryAttribute("4 Boundary layer"), DisplayName("1 Global")]
        public BoundaryLayerParameters GlobalBoundaryLayerParameters { get; set; }

        [CategoryAttribute("4 Boundary layer"), DisplayName("2 Patch")]
        public List<PatchBoundaryLayerSubDict> PatchBoundaryLayerParameters { get; set; }
    }
}
