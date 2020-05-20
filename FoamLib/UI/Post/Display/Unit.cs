using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Display
{
    public enum DisplayMode
    {
        Point,
        Wireframe,
        Surface,
        SurfaceWithEdge
    }
    public class Unit
    {
        protected vtkActor actor = vtkActor.New();
        protected DisplayMode mode = DisplayMode.Surface;
        protected string name = "";
        private vtkRenderer renderer = null;
        public Unit(){}
        public Unit(string name)
        {
            this.name = name;
        }
        public vtkActor Actor { get => actor; }
        public DisplayMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                if (mode == DisplayMode.Point)
                {
                    actor.GetProperty().SetEdgeVisibility(0);
                    actor.GetProperty().SetRepresentationToPoints();
                }
                else if (mode == DisplayMode.Wireframe)
                {
                    actor.GetProperty().SetEdgeVisibility(0);
                    actor.GetProperty().SetRepresentationToWireframe();
                }
                else if (mode == DisplayMode.Surface)
                {
                    actor.GetProperty().SetEdgeVisibility(0);
                    actor.GetProperty().SetRepresentationToSurface();
                }
                else if (mode == DisplayMode.SurfaceWithEdge)
                {
                    actor.GetProperty().SetEdgeVisibility(1);
                    actor.GetProperty().SetRepresentationToSurface();
                }
                //Render();
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }
        public double Opacity
        {
            get => actor.GetProperty().GetOpacity();
            set
            {
                actor.GetProperty().SetOpacity(value);
                //Render();
            }
        }
        public virtual bool Visible
        {
            get => actor.GetVisibility() == 1;
            set
            {
                actor.SetVisibility(value ? 1 : 0);
                //Render();
            }
        }
        public virtual vtkRenderer Renderer
        {
            get => renderer;
            set
            {
                renderer = value;
                renderer.AddActor(actor);
            }
        }
        public virtual void Render()
        {
            if (renderer != null && renderer.GetRenderWindow() != null)
                renderer.GetRenderWindow().GetInteractor().Render();
        }
        public virtual void UnsetRender()
        {
            if (renderer != null)
                renderer.RemoveActor(actor);
        }
        public virtual void Dispose()
        {
            if (actor != null)
            {
                actor.Dispose();
            }
        }
        public Color Color
        {
            get
            {
                double[] ci = actor.GetProperty().GetColor();
                return Color.FromArgb((int)(256 * ci[0]), (int)(256 * ci[1]), (int)(256 * ci[2]));
            }
            set
            {
                actor.GetProperty().SetColor((double)(value.R) / 256.0, (double)(value.G) / 256.0, (double)(value.B) / 256.0);
                //Render();
            }
        }
    }
}
