using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Display
{
    public class MeshUnit : Unit
    {
        protected vtkDataSetMapper mapper = vtkDataSetMapper.New();
        protected vtkDataSet dataset = null;
        public vtkMapper Mapper { get => mapper; }
        public MeshUnit(string name, vtkDataSet dataset)
        {
            this.Name = name;
            this.Dataset = dataset;
        }
        public virtual vtkDataSet Dataset
        {
            get => dataset;
            set
            {
                dataset = value;
                mapper.SetInputData(dataset);
                mapper.ScalarVisibilityOff();
                actor.SetMapper(mapper);
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
                Render();
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            if (mapper != null)
                mapper.Dispose();
        }
    }
}
