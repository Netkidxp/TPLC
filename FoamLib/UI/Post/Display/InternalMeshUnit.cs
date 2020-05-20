using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FoamLib.UI.Post.Display
{
    public class InternalMeshUnit : MeshUnit
    {
        public InternalMeshUnit(string name, vtkDataSet dataset) : base(name, dataset)
        {
        }
    }
}
