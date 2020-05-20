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

namespace FoamLib.UI
{

    public partial class GeometryViewer : RenderWindowControl
    {
        
        vtkRenderer renderer = null;
        vtkAxesActor axesActor = null;
        vtkSTLReader reader = null;
        vtkActor actor = null;
        vtkOrientationMarkerWidget orientationWidget = null;
        bool isPrepared = false;
        Color geometryColor = Color.Black;
        public Color GeometryColor
        {
            get => geometryColor;
            set
            {
                geometryColor = value;
                actor.GetProperty().SetColor(GeometryColor.R / 255.0, GeometryColor.G / 255.0, GeometryColor.B / 255.0);
                Render(false, false);
            }
        }

        public GeometryViewer()
        {
            InitializeComponent();
            axesActor = vtkAxesActor.New();
            orientationWidget = vtkOrientationMarkerWidget.New();
            reader = vtkSTLReader.New();
            actor = vtkActor.New();
            orientationWidget.SetOutlineColor(0.9300, 0.5700, 0.1300);
            orientationWidget.SetOrientationMarker(axesActor);
            orientationWidget.SetViewport(0.8, 0.0, 1.0, 0.3);
        }

        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            if (axesActor != null)
                axesActor.Dispose();
            if (orientationWidget != null)
                orientationWidget.Dispose();
            if (reader != null)
                reader.Dispose();
            System.GC.Collect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PrepareVtkOnce();
        }

        public void Render(bool resetCamera = false, bool initInteractor = false)
        {
            if (renderer == null || this.RenderWindow == null)
                return;
            if (resetCamera)
                renderer.ResetCamera();
            if(initInteractor)
                this.RenderWindow.GetInteractor().Initialize();
            this.RenderWindow.GetInteractor().Render();
        }
        
        public void SetStlFileName(string fileName)
        {
            reader.SetFileName(fileName);
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(GeometryColor.R/255.0,GeometryColor.G/255.0, GeometryColor.B/255.0);
        }

        protected void PrepareVtkOnce()
        {
            if(!isPrepared && this.RenderWindow != null)
            {
                renderer = (vtkRenderer)(this.RenderWindow.GetRenderers().GetFirstRenderer());
                renderer.SetBackground(0.9, 0.9, 0.9);
                renderer.AddActor(actor);
                orientationWidget.SetInteractor(this.RenderWindow.GetInteractor());
                orientationWidget.SetEnabled(1);
                orientationWidget.InteractiveOn();
                Render(true,true);
                isPrepared = true;
            }
        }
    }
}
