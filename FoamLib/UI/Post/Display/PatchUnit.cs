using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FoamLib.UI.Post.Display
{
    public class PatchUnit : DatasetUnit
    {
        public PatchUnit(string name, vtkDataSet dataset) : base(name, dataset)
        {
        }
    }
}
