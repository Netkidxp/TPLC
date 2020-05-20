using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.UI.Post.Display;

namespace FoamLib.UI.Post.Util
{
    public class FoamUnitManager
    {
        private Dictionary<string, DatasetUnit> unitTable = new Dictionary<string, DatasetUnit>();
        private vtkRenderer renderer = null;
        private vtkRenderWindow renderWindow = null;
        private vtkOpenFOAMReader foamReader = vtkOpenFOAMReader.New();
        private vtkScalarBarWidget scalarBarWidget = vtkScalarBarWidget.New();

        private string activeUnitName = "";
        private bool scalarBarOn = false;

        private double pressureScaleCoefficient = 1.0;
        public vtkRenderWindow RenderWindow
        {
            get => renderWindow;
            set
            {
                renderWindow = value;
                if (renderWindow != null)
                {
                    renderWindow.AddRenderer(renderer);
                    scalarBarWidget.SetInteractor(renderWindow.GetInteractor());
                } 
            }
        }

        public vtkRenderer Renderer { get => renderer;}
        public string ActiveUnitName
        {
            get => activeUnitName;
            set
            {
                activeUnitName = value;
                if (activeUnitName == "")
                    return;
                DatasetUnit u = FindUnitWithName(activeUnitName);
                if(u!=null)
                {
                    scalarBarWidget.SetScalarBarActor(u.ScalarBarActor);
                }
            }
        }

        public bool ScalarBarOn
        {
            get => scalarBarOn;
            set
            {
                scalarBarOn = value;
                if (scalarBarOn)
                    scalarBarWidget.On();
                else
                    scalarBarWidget.Off();
            }
        }

        public double PressureScaleCoefficient { get => pressureScaleCoefficient; set => pressureScaleCoefficient = value; }

        public FoamUnitManager(vtkRenderWindow window)
        {
            renderer = vtkRenderer.New();
            renderer.SetBackground(0.9, 0.9, 0.9);
            this.RenderWindow = window;
        }

        public virtual void Render(bool resetCamera = false, bool initInteractor = false)
        {
            if (renderWindow == null)
                return;
            if (resetCamera)
                renderer.ResetCamera();
            if (initInteractor)
                renderWindow.GetInteractor().Initialize();
            renderWindow.GetInteractor().Render();
        }
        protected void Add(DatasetUnit unit)
        {
            unitTable.Add(unit.Name, unit);
            renderer.AddActor(unit.Actor);
        }
        public void AddCutUnit(CutUnit cu)
        {
            Add(cu);
        }
        public void Remove(string unitName)
        {
            Unit u = FindUnitWithName(unitName);
            if(u != null)
            {
                unitTable.Remove(unitName);
                u.UnsetRender();
            }
        }
        public DatasetUnit FindUnitWithName(string unitName)
        {
            if (Contain(unitName))
            {
                return unitTable[unitName];
            }
            else
            {
                return null;
            }
        }
        public InternalUnit GetInternalMeshUnit()
        {
            return (InternalUnit)FindUnitWithName("internalMesh");
        }
        public List<PatchUnit> GetAllPatchUnits()
        {
            List<PatchUnit> us = new List<PatchUnit>();
            foreach(DatasetUnit u in unitTable.Values)
            {
                if (u is PatchUnit)
                    us.Add((PatchUnit)u);
            }
            return us;
        }
        public List<CutUnit> GetAllCutUnit()
        {
            List<CutUnit> us = new List<CutUnit>();
            foreach (DatasetUnit u in unitTable.Values)
            {
                if (u is CutUnit)
                    us.Add((CutUnit)u);
            }
            return us;
        }
        public List<DatasetUnit> GetAll2DUnit()
        {
            List<DatasetUnit> us = new List<DatasetUnit>();
            foreach (DatasetUnit u in unitTable.Values)
            {
                if (u is CutUnit || u is PatchUnit)
                    us.Add(u);
            }
            return us;
        }
        public bool Contain(string unitName)
        {
            return unitTable.Keys.Contains(unitName);
        }
        public bool Contain(DatasetUnit unit)
        {
            return unitTable.Values.Contains(unit);
        }
        public static double[] GetFoamTimeValues(string vxt)
        {
            vtkOpenFOAMReader reader = vtkOpenFOAMReader.New();
            reader.SetFileName(vxt);
            reader.UpdateInformation();
            vtkDoubleArray da = reader.GetTimeValues();
            int size = da.GetSize();
            double[] res = new double[size];
            for(int i = 0; i<size; i++)
            {
                res[i] = da.GetValue(i);
            }
            return res;
        }
        
        public void Load(string vxt)
        {
            double[] times = GetFoamTimeValues(vxt);
            double lastTime = times.Last();
            Load(vxt, lastTime);
        }
        public void Load(string vxt,double time)
        {
            Clear();
            foamReader.SetFileName(vxt);
            //foamReader.Update();
            foamReader.UpdateInformation();
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
                    vtkDataSet d = vtkDataSet.SafeDownCast(block);
                    CorrectPointPressure(d, PressureScaleCoefficient);
                    InternalUnit u = new InternalUnit("internalMesh", d);
                    u.Visible = false;
                    Add(u);
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
                        vtkDataSet d = vtkDataSet.SafeDownCast(pblock);
                        CorrectPointPressure(d, PressureScaleCoefficient);
                        PatchUnit u = new PatchUnit(pname, d);
                        Add(u);
                    }
                }
            }
        }
        public CutUnit NewCutUnit()
        {
            DatasetUnit iu = GetInternalMeshUnit();
            if (iu == null || renderWindow == null)
                return null;
            CutUnit cu = new CutUnit("cut" + (GetAllCutUnit().Count + 1), iu, renderWindow.GetInteractor());
            Add(cu);
            return cu;
        }
        public void Test()
        {
            
        }
        public void Clear()
        {
            renderer.RemoveAllViewProps();
            foreach(DatasetUnit u in unitTable.Values)
            {
                u.Dispose();
            }
            unitTable.Clear();
        }
        public void SetUnitsVisibleByType(Type type,bool visible)
        {
            foreach(DatasetUnit u in unitTable.Values)
            {
                if(u.GetType() == type)
                {
                    u.Visible = visible;
                }
            }
        }
        private static void CorrectPointPressure(vtkDataSet dataset, double coefficient)
        {
            vtkDataArray pda = dataset.GetPointData().GetArray("p");
            for(int i = 0; i < pda.GetNumberOfTuples(); i++)
            {
                double v = pda.GetComponent(i, 0);
                pda.SetComponent(i, 0, v * coefficient);
            }
        }

        public double CalculatePressDrop(string outlet, string inlet)
        {
            Unit u1 = FindUnitWithName(inlet);
            Unit u2 = FindUnitWithName(outlet);
            if (u1 == null || u2 == null)
                return 0;
            if (u1 is DatasetUnit && u2 is DatasetUnit)
            {
                return (u2 as DatasetUnit).GetAveragePointValue("p", 0) - (u1 as DatasetUnit).GetAveragePointValue("p", 0);
            }
            else
                return 0;
        }
    }
}
