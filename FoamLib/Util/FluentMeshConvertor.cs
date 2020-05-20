using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.IO;

namespace FoamLib.Util
{
    public class FluentMeshConvertor : MeshConvertorBase
    {
        public FluentMeshConvertor(FoamRunner runner, IMonitor monitor) : base(runner, monitor)
        {
        }

        public override bool ChooseInput()
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.msh|*.msh|*.*|*.*";
            d.CheckFileExists = true;
            if (d.ShowDialog() == DialogResult.OK)
            {
                input = d.FileName;
                return true;
            }
            else
                return false;
        }

        public override void StartConvert(string meshRoot)
        {
            runner.AsyncLocalConvertFluentMesh(input, meshRoot);
        }
    }
}
