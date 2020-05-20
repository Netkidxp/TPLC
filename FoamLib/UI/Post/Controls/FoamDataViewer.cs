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
using System.IO;
using FoamLib.UI.Post.Util;

namespace FoamLib.UI.Post.Controls
{

    public partial class FoamDataViewer : RenderWindowControl
    {
        protected vtkAxesActor axesActor = vtkAxesActor.New();
        protected vtkOrientationMarkerWidget orientationWidget = vtkOrientationMarkerWidget.New();
        protected vtkPicker picker = vtkPicker.New();
        
        protected bool isPrepared = false;
        FoamUnitManager unitManager = null;

        public FoamUnitManager UnitManager { get => unitManager;}

        public event EventHandler<EventArgs> OnReadMeshStarted;
        public event EventHandler<EventArgs> OnReadMeshFinished;
        public event EventHandler<EventArgs> OnPatchSelected;


        public FoamDataViewer()
        {
            InitializeComponent();
            unitManager = new FoamUnitManager(this.RenderWindow);
            orientationWidget.SetOutlineColor(0.9300, 0.5700, 0.1300);
            orientationWidget.SetOrientationMarker(axesActor);
            orientationWidget.SetViewport(0.8, 0.0, 1.0, 0.3);
        }
        
        protected virtual void LeftButtonDown(vtkObject o, vtkObjectEventArgs e)
        {
            /*
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
            */
        }
        
        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            axesActor.Dispose();
            orientationWidget.Dispose();
            picker.Dispose();
            System.GC.Collect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PrepareVtkOnce();
        }
        protected virtual Task UpdateMeshData(string vxt)
        {
            Task task = Task.Run(() =>
            {
                UnitManager.Load(vxt);
            });
            return task;
        }

        protected virtual async Task AsyncUpdateMeshData(string vxt)
        {
            if(OnReadMeshStarted!=null)
                OnReadMeshStarted(this, new EventArgs());
            await UpdateMeshData(vxt);
            Render(true,true);
            if(OnReadMeshFinished!=null)
                OnReadMeshFinished(this, new EventArgs());
        }

        public virtual void Render(bool resetCamera = false, bool initInteractor = false)
        {
            UnitManager.Render(resetCamera, initInteractor);
        }

        public virtual void SetMeshFileName(string vxt)
        {
            if (isPrepared && File.Exists(vxt))
                AsyncUpdateMeshData(vxt);
        }

        protected virtual void PrepareVtkOnce()
        {
            if(!isPrepared && this.RenderWindow != null)
            {
                UnitManager.RenderWindow = this.RenderWindow;
                this.RenderWindow.GetInteractor().SetPicker(picker);
                orientationWidget.SetInteractor(this.RenderWindow.GetInteractor());
                orientationWidget.SetEnabled(1);
                orientationWidget.InteractiveOn();
                this.RenderWindow.GetInteractor().LeftButtonPressEvt += LeftButtonDown;
                isPrepared = true;
            }
        }

        public void SavePngImage(string fileName)
        {

            vtkWindowToImageFilter wif = vtkWindowToImageFilter.New();
            wif.SetInput(this.RenderWindow);
            wif.Update();
            vtkPNGWriter writer = new vtkPNGWriter();
            writer.SetFileName(fileName);
            writer.SetInputConnection(wif.GetOutputPort());
            writer.Write();
            writer.Dispose();
            wif.Dispose();
        }
        public void SaveJpegImage(string fileName)
        {
            vtkWindowToImageFilter wif = vtkWindowToImageFilter.New();
            wif.SetInput(this.RenderWindow);
            wif.Update();
            vtkJPEGWriter writer = new vtkJPEGWriter();
            writer.SetFileName(fileName);
            writer.SetInputConnection(wif.GetOutputPort());
            writer.Write();
            writer.Dispose();
            wif.Dispose();
        }
    }
}
