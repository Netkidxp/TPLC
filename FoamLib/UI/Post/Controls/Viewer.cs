using FoamLib.UI.Post.Display;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoamLib.UI.Post.Controls
{
    public class PickEventArge : EventArgs
    {
        public Unit PickedUnit { get; set; }
    }
    public class Viewer : RenderWindowControl
    {
        protected vtkAxesActor axesActor = vtkAxesActor.New();
        protected vtkOrientationMarkerWidget orientationWidget = vtkOrientationMarkerWidget.New();
        protected vtkPicker picker = vtkPicker.New();
        private vtkRenderer renderer = vtkRenderer.New();
        protected bool isPrepared = false;

        private List<Unit> unitList = new List<Unit>();

        public event EventHandler<PickEventArge> OnUnitPick;

        public Viewer() : base()
        {
            orientationWidget.SetOutlineColor(0.9300, 0.5700, 0.1300);
            orientationWidget.SetOrientationMarker(axesActor);
            orientationWidget.SetViewport(0.8, 0.0, 1.0, 0.3);
            Renderer.SetBackground(0.9, 0.9, 0.9);
        }
        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            axesActor.Dispose();
            orientationWidget.Dispose();
            picker.Dispose();
        }
        protected vtkRenderWindowInteractor Interactor
        {
            get
            {
                if (this.RenderWindow != null)
                    return this.RenderWindow.GetInteractor();
                else
                    return null;
            }
        }

        public vtkRenderer Renderer { get => renderer; }

        protected virtual void LeftButtonRelease(vtkObject o, vtkObjectEventArgs e)
        {
            picker.Pick(Interactor.GetEventPosition()[0], Interactor.GetEventPosition()[1], 0.0, Renderer);
            vtkActor actor = picker.GetActor();
            Unit u = FindUnit(actor);
            if (u != null)
            {
                PickEventArge pe = new PickEventArge();
                pe.PickedUnit = u;
                OnUnitPick?.Invoke(this, pe);
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

        public virtual void AddUnit(Unit unit)
        {
            if (unit.Name == "")
                return;
            RemoveUnit(unit.Name);
            unit.Renderer = Renderer;
            unitList.Add(unit);
            //Render();
        }

        public virtual void AddUnits(ICollection<Unit> units)
        {
            foreach( Unit u in units)
            {
                AddUnit(u);
            }
        }
        public virtual void RemoveUnit(string name)
        {
            Unit u = FindUnit(name);
            if (u != null)
            {
                unitList.Remove(u);
                u.UnsetRender();
                //Render();
            }  
        }
        public virtual void RemoveUnit(Unit unit)
        {
            if (unitList.Contains(unit))
            {
                unitList.Remove(unit);
                unit.UnsetRender();
                //Render();
            } 
        }
        public Unit FindUnit(string name)
        {
            foreach(Unit u in unitList)
            {
                if(u.Name == name)
                {
                    return u;
                }
            }
            return null;
        }
        public Unit FindUnit(vtkActor actor)
        {
            foreach (Unit u in unitList)
            {
                if (u.Actor == actor)
                {
                    return u;
                }
            }
            return null;
        }

        public void Render(bool resetCamera = false, bool initInteractor = false)
        {
            if (this.RenderWindow == null)
                return;
            if (resetCamera)
                Renderer.ResetCamera();
            if (initInteractor)
                Interactor.Initialize();
            Interactor.Render();
        }
        private delegate void DgRender(bool resetCamera, bool initInteractor);

        public void SafeRender(bool resetCamera = false, bool initInteractor = false)
        {
            DgRender dgr = new DgRender(Render);
            this.Invoke(dgr, new object[] { resetCamera, initInteractor });
        }
        protected virtual void PrepareVtkOnce()
        {
            if (!isPrepared && this.RenderWindow != null)
            {
                RenderWindow.AddRenderer(Renderer);
                Interactor.SetPicker(picker);
                orientationWidget.SetInteractor(Interactor);
                orientationWidget.SetEnabled(1);
                orientationWidget.InteractiveOn();
                Interactor.LeftButtonPressEvt += LeftButtonRelease;
                isPrepared = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PrepareVtkOnce();
        }

        public virtual void RemoveAllUnits()
        {
            List<Unit> us = new List<Unit>();
            us.AddRange(unitList);
            foreach(Unit u in us)
            {
                RemoveUnit(u);
            }
            
        }
    }
}
