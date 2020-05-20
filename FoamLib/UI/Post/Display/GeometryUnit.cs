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
    public class GeometryUnit : Unit
    {
        public GeometryUnit(string name) : base(name)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
