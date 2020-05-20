using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoamLib.IO;

namespace FoamLib.Util
{
    public class CcmMeshConvertor : MeshConvertorBase
    {
        public CcmMeshConvertor(FoamRunner runner, IMonitor monitor) : base(runner, monitor)
        {
        }

        public override bool ChooseInput()
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.ccm|*.ccm|*.*|*.*";
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
            runner.AsyncLocalConvertCcmMesh(input, meshRoot);
        }
    }
}
