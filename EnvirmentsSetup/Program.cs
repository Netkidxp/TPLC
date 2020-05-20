using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvirmentsSetup
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string exePath = System.Windows.Forms.Application.ExecutablePath;
            string appRootDir = Path.GetDirectoryName(Path.GetDirectoryName(exePath));
            string foamRootDir = Path.Combine(appRootDir, "OpenFOAM-5.x");
            string vtkRootDir = Path.Combine(appRootDir, "vtk-lib");
            string mingw64RootDir = Path.Combine(appRootDir, "mingw64-lib");
            string msmpiRootDir = Path.Combine(appRootDir, "msmpi");
            SysEnvironment.SetPathAfter(vtkRootDir);
        }
    }
}
