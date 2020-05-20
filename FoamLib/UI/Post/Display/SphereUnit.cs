using FoamLib.Model;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Display
{
    public class SphereUnit : GeometryUnit
    {
        vtkSphereSource source = vtkSphereSource.New();
        vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
        Vector center = new Vector(0, 0, 0);
        double radius = 1;
        public SphereUnit(string name) : base(name)
        {
            UpdateSphere();
            mapper.SetInputConnection(source.GetOutputPort());
            actor.SetMapper(mapper);
            Center.OnPropertyChanged += OnCenterChanged;
        }

        public Vector Center
        {
            get => center;
            set
            {
                center = value;
                center.OnPropertyChanged += OnCenterChanged;
                UpdateSphere();
            }
        }
        public double Radius
        {
            get => radius;
            set
            {
                radius = value;
                UpdateSphere();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            source.Dispose();
            mapper.Dispose();
        }

        private void OnCenterChanged(object sender, EventArgs e)
        {
            UpdateSphere();
        }
        
        private void UpdateSphere()
        {
            source.SetCenter(center.V1, center.V2, center.V3);
            source.SetRadius(radius);
            Render();
        }
    }
}
