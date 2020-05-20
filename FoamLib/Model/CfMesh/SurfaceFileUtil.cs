using FoamLib.IO;
using FoamLib.Util;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model.CfMesh
{
    public class SurfaceFileUtil
    {
        public static bool DoSurfaceFile(List<string> stlFileNames, IMonitor mon)
        {
            GlobalGeometryObject.SurfaceFileName = "";
            StlTool stlTool = new StlTool(mon);
            string tmp = Path.GetTempFileName() + ".stl";
            if (!stlTool.Merge(tmp, stlFileNames))
                return false;
            List<string> solids = stlTool.GetSolidNameList(stlFileNames);
            if (solids == null)
                return false;
            if (solids.Count == 0)
                return false;
            GlobalGeometryObject.SurfacePartList.Clear();
            GlobalGeometryObject.SurfacePartList.AddRange(solids);
            GlobalGeometryObject.SurfaceFileName = tmp;
            GlobalGeometryObject.Volume = CalculateVolumeFromStl(tmp);
            return true;
        }
        public static bool DoSurfaceFile(string stlFileName, IMonitor mon)
        {
            GlobalGeometryObject.SurfaceFileName = stlFileName;
            StlTool stlTool = new StlTool(mon);
            List<string> solids = stlTool.GetSolidNameList(stlFileName);
            if (solids == null)
                return false;
            if (solids.Count == 0)
                return false;
            GlobalGeometryObject.SurfacePartList.Clear();
            GlobalGeometryObject.SurfacePartList.AddRange(solids);
            GlobalGeometryObject.Volume = CalculateVolumeFromStl(stlFileName);
            return true;
        }
        public static double CalculateVolumeFromStl(string fileName)
        {
            vtkSTLReader reader = vtkSTLReader.New();
            reader.SetFileName(fileName);
            vtkMassProperties property = vtkMassProperties.New();
            property.SetInputConnection(reader.GetOutputPort());
            property.Update();
            double volume = property.GetVolume();
            property.Dispose();
            reader.Dispose();
            return volume;
        }
    }
}
