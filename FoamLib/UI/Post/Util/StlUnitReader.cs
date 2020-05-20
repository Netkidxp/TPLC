using FoamLib.IO;
using FoamLib.UI.Post.Display;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.UI.Post.Util
{
    public class StlUnitReader : UnitReader
    {
        public override bool Read(string fileName, IMonitor mon = null)
        {
            try
            {
                unitList.Clear();
                vtkSTLReader reader = vtkSTLReader.New();
                vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
                reader.SetFileName(fileName);
                mapper.SetInputConnection(reader.GetOutputPort());
                StlUnit unit = new StlUnit("Stl." + Path.GetFileNameWithoutExtension(fileName));
                unit.Actor.SetMapper(mapper);
                unitList.Add(unit);
                reader.Dispose();
                mapper.Dispose();
                return true;
            }
            catch (Exception e)
            {
                if (mon != null)
                    mon.ErrorLine(e.Message);
                return false;
            }
        }
    }
}
