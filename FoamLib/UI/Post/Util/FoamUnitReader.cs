using FoamLib.IO;
using FoamLib.UI.Post.Display;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Util
{
    public class FoamUnitReader : UnitReader
    {
        List<Unit> patchUnitList = new List<Unit>();
        List<Unit> internalUnitList = new List<Unit>();

        public List<Unit> PatchUnitList { get => patchUnitList; }
        public List<Unit> InternalUnitList { get => internalUnitList; }

        public static double[] GetFoamTimeValues(string vxt)
        {
            vtkOpenFOAMReader reader = vtkOpenFOAMReader.New();
            reader.SetFileName(vxt);
            reader.UpdateInformation();
            vtkDoubleArray da = reader.GetTimeValues();
            int size = da.GetSize();
            double[] res = new double[size];
            for (int i = 0; i < size; i++)
            {
                res[i] = da.GetValue(i);
            }
            reader.Dispose();
            return res;
        }
        public bool ReadOnlyMesh(string fileName, IMonitor mon = null)
        {
            try
            {
                unitList.Clear();
                vtkOpenFOAMReader foamReader = vtkOpenFOAMReader.New();
                foamReader.SetFileName(fileName);
                foamReader.ReadZonesOn();
                foamReader.UpdateInformation();
                int np = foamReader.GetNumberOfPatchArrays();
                for (int i = 0; i < np; i++)
                {
                    String patchName = foamReader.GetPatchArrayName(i);
                    foamReader.SetPatchArrayStatus(patchName, patchName == "internalMesh" ? 0 : 1);
                }
                foamReader.Update();
                vtkMultiBlockDataSet rootMbds = foamReader.GetOutput();
                for (uint i = 0; i < rootMbds.GetNumberOfBlocks(); i++)
                {
                    vtkDataObject block = rootMbds.GetBlock(i);
                    if (block == null)
                        continue;
                    String bname = rootMbds.GetMetaData(i).Get(vtkCompositeDataSet.NAME());
                    if (bname == "internalMesh")
                    {
                        InternalMeshUnit u = new InternalMeshUnit("internalMesh", vtkDataSet.SafeDownCast(block));
                        u.Visible = false;
                        unitList.Add(u);
                        internalUnitList.Add(u);
                    }
                    else if (bname == "Patches")
                    {
                        vtkMultiBlockDataSet patchesMbds = vtkMultiBlockDataSet.SafeDownCast(block);
                        for (uint j = 0; j < patchesMbds.GetNumberOfBlocks(); j++)
                        {
                            vtkDataObject pblock = patchesMbds.GetBlock(j);
                            if (pblock == null)
                                continue;
                            String pname = patchesMbds.GetMetaData(j).Get(vtkCompositeDataSet.NAME());
                            PatchMeshUnit u = new PatchMeshUnit("Patch." + pname, vtkDataSet.SafeDownCast(pblock));
                            unitList.Add(u);
                            patchUnitList.Add(u);
                        }
                    }
                }
                foamReader.Dispose();
                return true;
            }
            catch (Exception e)
            {
                if (mon != null)
                    mon.ErrorLine(e.Message);
                return false;
            }
        }
        public bool Read(string fileName, double time, IMonitor mon = null)
        {
            try
            {
                unitList.Clear();
                vtkOpenFOAMReader foamReader = vtkOpenFOAMReader.New();
                foamReader.SetFileName(fileName);
                foamReader.Update();
                foamReader.SetTimeValue(time);
                foamReader.CreateCellToPointOn();
                foamReader.ReadZonesOn();
                int np = foamReader.GetNumberOfPatchArrays();
                for (int i = 0; i < np; i++)
                {
                    String patchName = foamReader.GetPatchArrayName(i);
                    foamReader.SetPatchArrayStatus(patchName, 1);
                }
                foamReader.Update();
                vtkMultiBlockDataSet rootMbds = foamReader.GetOutput();
                for (uint i = 0; i < rootMbds.GetNumberOfBlocks(); i++)
                {
                    vtkDataObject block = rootMbds.GetBlock(i);
                    if (block == null)
                        continue;
                    String bname = rootMbds.GetMetaData(i).Get(vtkCompositeDataSet.NAME());
                    if (bname == "internalMesh")
                    {
                        InternalUnit u = new InternalUnit("internalMesh", vtkDataSet.SafeDownCast(block));
                        u.Visible = false;
                        unitList.Add(u);
                        internalUnitList.Add(u);
                    }
                    else if (bname == "Patches")
                    {
                        vtkMultiBlockDataSet patchesMbds = vtkMultiBlockDataSet.SafeDownCast(block);
                        for (uint j = 0; j < patchesMbds.GetNumberOfBlocks(); j++)
                        {
                            vtkDataObject pblock = patchesMbds.GetBlock(j);
                            if (pblock == null)
                                continue;
                            String pname = patchesMbds.GetMetaData(j).Get(vtkCompositeDataSet.NAME());
                            PatchUnit u = new PatchUnit("Patch." + pname, vtkDataSet.SafeDownCast(pblock));
                            unitList.Add(u);
                            patchUnitList.Add(u);
                        }
                    }
                }
                foamReader.Dispose();
                return true;
            }
            catch(Exception e)
            {
                if (mon != null)
                    mon.ErrorLine(e.Message);
                return false;
            }
        }

        public override bool Read(string fileName, IMonitor mon = null)
        {
            double[] times = GetFoamTimeValues(fileName);
            double lastTime = times.Last();
            return Read(fileName, lastTime, mon);
        }
    }
}
