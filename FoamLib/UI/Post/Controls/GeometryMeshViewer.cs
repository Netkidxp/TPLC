using FoamLib.UI.Post.Display;
using FoamLib.UI.Post.Util;
using FoamLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Controls
{
    public class GeometryMeshViewer : Viewer
    {
        List<StlUnit> stlUnits = new List<StlUnit>();
        List<PatchMeshUnit> patchMeshUnits = new List<PatchMeshUnit>();
        List<GeometryUnit> geometryUnits = new List<GeometryUnit>();
        InternalMeshUnit internalMeshUnit = null;

        bool patchUnitVisible = true;
        bool stlUnitVisible = true;
        bool geometryUnitVisible = true;
        ColorPool colorPool = new ColorPool();
        DisplayMode mode = DisplayMode.SurfaceWithEdge;


        public void ResetColorPool()
        {
            colorPool.Reset();
        }

        public bool PatchUnitVisible
        {
            get => patchUnitVisible;
            set
            {
                patchUnitVisible = value;
                foreach (Unit u in PatchMeshUnits)
                {
                    u.Visible = patchUnitVisible;
                }
            }
        }

        public bool StlUnitVisible
        {
            get => stlUnitVisible;
            set 
            {
                stlUnitVisible = value;
                foreach(Unit u in StlUnits)
                {
                    u.Visible = stlUnitVisible;
                }
            }
        }
        
        

        public DisplayMode MeshDisplayMode
        {
            get => mode;
            set
            {
                mode = value;
                foreach (Unit u in PatchMeshUnits)
                {
                    u.Mode = mode;
                }
            }
        }

        public bool GeometryUnitVisible
        {
            get => geometryUnitVisible;
            set
            {
                geometryUnitVisible = value;
                foreach(Unit u in GeometryUnits)
                {
                    u.Visible = geometryUnitVisible;
                }
            }
        }

        public List<StlUnit> StlUnits { get => stlUnits; }
        public List<PatchMeshUnit> PatchMeshUnits { get => patchMeshUnits; }
        public List<GeometryUnit> GeometryUnits { get => geometryUnits; }
        public InternalMeshUnit InternalMeshUnit { get => internalMeshUnit; }

        public override void AddUnit(Unit unit)
        {
            
            if(unit is PatchMeshUnit)
            {
                unit.Visible = patchUnitVisible;
                unit.Mode = mode;
                unit.Color = colorPool.Next();
            }
            if(unit is InternalMeshUnit)
            {
                unit.Visible = false;
            }
            if(unit is StlUnit)
            {
                unit.Visible = stlUnitVisible;
                unit.Mode = DisplayMode.Surface;
                unit.Color = System.Drawing.Color.Gray;
            }
            base.AddUnit(unit);
            if (unit is StlUnit)
                stlUnits.Add(unit as StlUnit);
            else if (unit is PatchMeshUnit)
                patchMeshUnits.Add(unit as PatchMeshUnit);
            else if (unit is BoxUnit)
                geometryUnits.Add(unit as BoxUnit);
            else if(unit is SphereUnit)
                geometryUnits.Add(unit as SphereUnit);
            else if (unit is InternalUnit)
                internalMeshUnit = unit as InternalMeshUnit;
            
        }

        public override void RemoveUnit(Unit unit)
        {
            base.RemoveUnit(unit);
            if (unit is StlUnit)
                stlUnits.Remove(unit as StlUnit);
            else if (unit is PatchUnit)
                patchMeshUnits.Remove(unit as PatchMeshUnit);
            else if (unit is BoxUnit)
                geometryUnits.Remove(unit as BoxUnit);
            else if (unit is InternalUnit)
                internalMeshUnit = null;
        }

        public void ClearGeometryUnits()
        {
            List<GeometryUnit> us = new List<GeometryUnit>();
            us.AddRange(GeometryUnits);
            foreach(GeometryUnit u in us)
            {
                RemoveUnit(u);
            }
        }
    }
}
