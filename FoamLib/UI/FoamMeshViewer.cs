using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kitware.VTK;
using FoamLib.Util;
namespace FoamLib.UI
{

    public partial class FoamMeshViewer : RenderWindowControl
    {
        public enum MeshRepresentation
        {
            Point,
            Wireframe,
            Surface,
            SurfaceWithEdge
        }
        public enum DataFileType
        {
            Mesh,
            STL
        }

        public class SelectPatchEventArgs : EventArgs
        {
            public string PatchName
            {
                get; set;
            }
        }

        public class DataFileEventArgs : EventArgs
        {
            public DataFileEventArgs()
            {
            }

            public DataFileEventArgs(string fileName, DataFileType fileType)
            {
                FileName = fileName;
                FileType = fileType;
            }

            public string FileName
            {
                get;set;
            }
            public DataFileType FileType
            {
                get;set;
            }
        }
        protected ColorPool colorPool = new ColorPool();
        protected const String InternalMeshActoName = "INTERNAL_MESH_ACTOR";
        protected Dictionary<string, vtkActor> actorList;
        protected List<String> highLightPatchNames;
        protected vtkRenderer renderer = null;
        protected vtkAxesActor axesActor = null;
        protected vtkOpenFOAMReader foamReader = null;
        protected vtkSTLReader stlReader = null;
        protected vtkOrientationMarkerWidget orientationWidget = null;
        protected vtkPicker picker = null;
        protected bool isPrepared = false;
        protected MeshRepresentation representation = MeshRepresentation.Surface;
        protected Color meshColor = Color.Gray;
        protected Color hightLightColor = Color.Red;
        
        public MeshRepresentation Representation
        {
            get => representation;
            set
            {
                representation = value;
                //if (!actorList.ContainsKey(InternalMeshActoName))
                    //return;
                if(value == MeshRepresentation.Point)
                {
                    //actorList[InternalMeshActoName].GetProperty().SetRepresentationToPoints();
                    foreach (vtkActor a in actorList.Values)
                    {
                        a.GetProperty().SetEdgeVisibility(0);
                        a.GetProperty().SetRepresentationToPoints();
                    }
                }
                else if(value == MeshRepresentation.Wireframe)
                {
                    //actorList[InternalMeshActoName].GetProperty().SetRepresentationToWireframe();
                    foreach (vtkActor a in actorList.Values)
                    {
                        a.GetProperty().SetEdgeVisibility(0);
                        a.GetProperty().SetRepresentationToWireframe();
                    }
                }
                else if(value == MeshRepresentation.Surface)
                {
                    //actorList[InternalMeshActoName].GetProperty().SetRepresentationToSurface();
                    foreach (vtkActor a in actorList.Values)
                    {
                        a.GetProperty().SetEdgeVisibility(0);
                        a.GetProperty().SetRepresentationToSurface();
                    }
                }
                else if(value == MeshRepresentation.SurfaceWithEdge)
                {
                    foreach (vtkActor a in actorList.Values)
                    {
                        a.GetProperty().SetEdgeVisibility(1);
                        a.GetProperty().SetRepresentationToSurface();
                    }
                }
                Render(false,false);
            }
        }

        public Color MeshColor
        {
            get => meshColor;
            set
            {
                meshColor = value;
                if(!actorList.ContainsKey(InternalMeshActoName))
                    return;
                actorList[InternalMeshActoName].GetProperty().SetColor(value.R / 255.0, value.G / 255.0, value.B / 255.0);
                Render(false, false);
            }
        }

        public Color HightLightColor
        {
            get => hightLightColor;
            set
            {
                hightLightColor = value;
                foreach (String s in highLightPatchNames)
                {
                    if (actorList.ContainsKey(s))
                        actorList[s].GetProperty().SetColor(hightLightColor.R / 255, hightLightColor.G / 255, hightLightColor.B / 255);
                }
            }
        }

        public bool ShowPatchName
        {
            get;set;
        }

        public void SetHighLightPatch(List<string> names)
        {
            foreach (String s in highLightPatchNames)
            {
                if (actorList.ContainsKey(s))
                    actorList[s].GetProperty().SetColor(meshColor.R / 255.0, meshColor.G / 255.0, meshColor.B / 255.0);
            }
            foreach (String s in names)
            {
                if (actorList.ContainsKey(s))
                    actorList[s].GetProperty().SetColor(hightLightColor.R/255.0, hightLightColor.G / 255.0, hightLightColor.B / 255.0);
            }
            highLightPatchNames = names;
            Render(false, false);
        }

        public event EventHandler<DataFileEventArgs> OnReadMeshStarted;

        public event EventHandler<DataFileEventArgs> OnReadMeshFinished;

        public event EventHandler<SelectPatchEventArgs> OnPatchSelected;

        public FoamMeshViewer()
        {
            InitializeComponent();
            actorList = new Dictionary<string, vtkActor>();
            highLightPatchNames = new List<string>();
            axesActor = vtkAxesActor.New();
            foamReader = vtkOpenFOAMReader.New();
            stlReader = vtkSTLReader.New();
            orientationWidget = vtkOrientationMarkerWidget.New();
            picker = vtkPicker.New();
            orientationWidget.SetOutlineColor(0.9300, 0.5700, 0.1300);
            orientationWidget.SetOrientationMarker(axesActor);
            orientationWidget.SetViewport(0.8, 0.0, 1.0, 0.3);
            ShowPatchName = true;
        }

        protected virtual void LeftButtonDown(vtkObject o, vtkObjectEventArgs e)
        {
            picker.Pick(this.RenderWindow.GetInteractor().GetEventPosition()[0],this.RenderWindow.GetInteractor().GetEventPosition()[1],0.0,renderer);
            vtkActor actor = picker.GetActor();
            if (actor != null)
            {
                foreach (KeyValuePair<string, vtkActor> ap in actorList)
                {
                    if (ap.Value == actor && ap.Key != InternalMeshActoName)
                    {
                        SelectPatchEventArgs ea = new SelectPatchEventArgs();
                        ea.PatchName = ap.Key;
                        //SetHighLightPatch(new List<string>() { ap.Key });
                        if(OnPatchSelected!=null)
                            OnPatchSelected(this, ea);
                    }
                }
            }
        }

        protected virtual void ClearActors()
        {
            if(renderer!=null)
            {
                renderer.RemoveAllViewProps();
            }
            foreach(vtkActor a in actorList.Values)
            {
                a.Dispose();
            }
            actorList.Clear();
        }

        protected virtual void FillActorList()
        {
            vtkMultiBlockDataSet rootMbds = foamReader.GetOutput();
            for(uint i=0;i<rootMbds.GetNumberOfBlocks();i++)
            {
                vtkDataObject block = rootMbds.GetBlock(i);
                if (block == null)
                    continue;
                
                String bname = rootMbds.GetMetaData(i).Get(vtkCompositeDataSet.NAME());
                if(bname == "internalMesh")
                {
                    /*
                    vtkDataSetMapper mapper = vtkDataSetMapper.New();
                    mapper.SetInputDataObject(block);
                    mapper.ScalarVisibilityOff();
                    vtkActor a = vtkActor.New();
                    a.SetMapper(mapper);
                    mapper.Dispose();
                    mapper = null;
                    a.GetProperty().SetColor(meshColor.R / 255.0, meshColor.G / 255.0, meshColor.B / 255.0);
                    if (representation == MeshRepresentation.Point)
                        {
                            a.GetProperty().SetEdgeVisibility(0);
                            a.GetProperty().SetRepresentationToPoints();
                        }
                        else if (representation == MeshRepresentation.Wireframe)
                        {
                            a.GetProperty().SetEdgeVisibility(0);
                            a.GetProperty().SetRepresentationToWireframe();
                        }
                        else if(representation == MeshRepresentation.Surface)
                        {
                            a.GetProperty().SetEdgeVisibility(0);
                            a.GetProperty().SetRepresentationToSurface();
                        }
                        else if(representation == MeshRepresentation.SurfaceWithEdge)
                        {
                            a.GetProperty().SetEdgeVisibility(1);
                            a.GetProperty().SetRepresentationToSurface();
                        }
                    actorList.Add(InternalMeshActoName, a);
                    if (renderer != null)
                        renderer.AddActor(a);

                    */
                }
                else if(bname == "Patches")
                {
                    vtkMultiBlockDataSet patchesMbds = vtkMultiBlockDataSet.SafeDownCast(block);
                    colorPool.Reset();
                    int textLeft = 10;
                    for(uint j=0;j<patchesMbds.GetNumberOfBlocks();j++)
                    {
                        vtkDataObject pblock = patchesMbds.GetBlock(j);
                        if (pblock == null)
                            continue;
                        String pname = patchesMbds.GetMetaData(j).Get(vtkCompositeDataSet.NAME());
                        vtkDataSetMapper mapper = vtkDataSetMapper.New();
                        mapper.SetInputDataObject(pblock);
                        mapper.ScalarVisibilityOff();
                        vtkActor a = vtkActor.New();
                        a.SetMapper(mapper);
                        mapper.Dispose();
                        mapper = null;
                        a.SetVisibility(1);
                        Color c = colorPool.Next();
                        a.GetProperty().SetColor(c.R / 255.0, c.G / 255.0, c.B / 255.0);
                        
                        if (representation == MeshRepresentation.Point)
                        {
                            a.GetProperty().SetEdgeVisibility(0);
                            a.GetProperty().SetRepresentationToPoints();
                        }
                        else if (representation == MeshRepresentation.Wireframe)
                        {
                            a.GetProperty().SetEdgeVisibility(0);
                            a.GetProperty().SetRepresentationToWireframe();
                        }
                        else if(representation == MeshRepresentation.Surface)
                        {
                            a.GetProperty().SetEdgeVisibility(0);
                            a.GetProperty().SetRepresentationToSurface();
                        }
                        else if(representation == MeshRepresentation.SurfaceWithEdge)
                        {
                            a.GetProperty().SetEdgeVisibility(1);
                            a.GetProperty().SetRepresentationToSurface();
                        }
                        
                        actorList.Add(pname, a);
                        if (renderer != null)
                            renderer.AddActor(a);
                        if (ShowPatchName)
                        {
                            vtkTextActor ta = vtkTextActor.New();
                            ta.SetInput(pname);
                            ta.GetTextProperty().SetColor(c.R / 255.0, c.G / 255.0, c.B / 255.0);
                            ta.GetTextProperty().SetFontSize(20);
                            ta.SetDisplayPosition(textLeft, 10);
                            textLeft += (pname.Length) * 20;
                            renderer.AddActor(ta);
                        }
                    }
                }
            }
            
        }

        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            ClearActors();
            if (axesActor != null)
                axesActor.Dispose();
            if (foamReader != null)
                foamReader.Dispose();
            if (orientationWidget != null)
                orientationWidget.Dispose();
            if (picker != null)
                picker.Dispose();
            System.GC.Collect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PrepareVtkOnce();
        }

        protected virtual Task UpdateMeshData(string mesh)
        {
            Task task = Task.Run(() =>
            {
                foamReader.SetFileName(mesh);
                foamReader.SetTimeValue(0);
                foamReader.ReadZonesOn();
                foamReader.UpdateInformation();
                int np = foamReader.GetNumberOfPatchArrays();
                for (int i = 0; i < np; i++)
                {
                    String patchName = foamReader.GetPatchArrayName(i);
                    if(patchName!="internalMesh")
                        foamReader.SetPatchArrayStatus(patchName, 1);
                }
                foamReader.Update();
                FillActorList();
            });
            return task;
        }

        protected virtual async Task AsyncUpdateMeshData(string mesh)
        {
            if(OnReadMeshStarted!=null)
                OnReadMeshStarted(this, new DataFileEventArgs(mesh,DataFileType.Mesh));
            await UpdateMeshData(mesh);
            Render(true,true);
            if(OnReadMeshFinished!=null)
                OnReadMeshFinished(this, new DataFileEventArgs(mesh, DataFileType.Mesh));
        }

        protected virtual Task UpdateStlData(string stl)
        {
            Task task = Task.Run(() =>
            {
                stlReader.SetFileName(stl);
                vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
                mapper.SetInputConnection(stlReader.GetOutputPort());
                vtkActor a = vtkActor.New();
                a.SetMapper(mapper);
                renderer.AddActor(a);
                actorList.Add("stl",a);
            });
            return task;
        }

        protected virtual async Task AsyncUpdateStlData(string stl)
        {
            if(OnReadMeshStarted!=null)
                OnReadMeshStarted(this, new DataFileEventArgs(stl, DataFileType.STL));
            await UpdateStlData(stl);
            Render(true, true);
            if(OnReadMeshFinished!=null)
                OnReadMeshFinished(this, new DataFileEventArgs(stl, DataFileType.STL));
        }

        public virtual void Render(bool resetCamera = false, bool initInteractor = false)
        {
            if (renderer == null || this.RenderWindow == null)
                return;
            if (resetCamera)
                renderer.ResetCamera();
            if(initInteractor)
                this.RenderWindow.GetInteractor().Initialize();
            this.RenderWindow.GetInteractor().Render();
        }

        public virtual void SetMeshFileName(string meshFileName)
        {
            ClearActors();
            if (isPrepared && meshFileName != "")
                AsyncUpdateMeshData(meshFileName);
        }

        public virtual void SetStlFileName(string stlFileName)
        {
            ClearActors();
            if (isPrepared && stlFileName != "")
                AsyncUpdateStlData(stlFileName);
        }
        public virtual void Clear()
        {
            ClearActors();
        }
        protected virtual void PrepareVtkOnce()
        {
            if(!isPrepared && this.RenderWindow != null)
            {
                renderer = (vtkRenderer)(this.RenderWindow.GetRenderers().GetFirstRenderer());
                renderer.SetBackground(0.9, 0.9, 0.9);
                this.RenderWindow.GetInteractor().SetPicker(picker);
                orientationWidget.SetInteractor(this.RenderWindow.GetInteractor());
                orientationWidget.SetEnabled(1);
                orientationWidget.InteractiveOn();
                this.RenderWindow.GetInteractor().LeftButtonPressEvt += LeftButtonDown;
                isPrepared = true;
            }
        }
    }
}
