using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class FoamRunner : Runner
    {
        public FoamRunner(GlobalConfig foamConfig) : base(foamConfig)
        {

        }
        private void AsyncLocalRun(string application,string arguments,string workDirecory,uint processorNumber)
        {
            if (processorNumber < 2)
            {
                AsyncRun(application, arguments, workDirecory);
            }
            else
            {
                var app = Path.Combine(foamConfig.MpiExePath, "mpiexec.exe");
                var arg = string.Format("-n {0} {1} {2}", processorNumber, application, arguments);
                AsyncRun(app, arg, workDirecory);
            }
        }
        public void AsyncLocalSolve(string solver, string cas, uint np)
        {
            string arg = "-case " + "\"" + cas +"\"";
            if (np >= 2)
                arg += " -parallel";
            name = "solve";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath,"bin",solver), arg, cas, np);
        }
        public void AsyncLocalDecomposePar(string cas)
        {
            name = "decomposePar";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "deComposePar.exe"), "-case \"" + cas + "\"", cas, 0);
        }
        public void AsyncLocalReconstructPar(string cas)
        {
            name = "reconstructPar";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "reConstructPar.exe"), "-case \"" + cas + "\"", cas, 0);
        }
        public void AsyncLocalReconstructParMesh(string cas)
        {
            name = "reconstructParMesh";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "reConstructParMesh.exe"), "-case \"" + cas + "\"", cas, 0);
        }
        public void AsyncLocalSetFields(string cas)
        {
            name = "setField";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "setFields.exe"), "-case \"" + cas + "\"", cas, 0);
        }
        public void AsyncLocalCfMesh(string app, string cas)
        {
            name = app;
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", app+".exe"), "-case \"" + cas + "\"", cas, 0);
        }
        public void AsyncLocalConvertFluentMesh(string input, string cas)
        {
            name = "fluentMeshToFoam";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "fluentMeshToFoam.exe"), "-case \"" + cas + "\" " + input, cas, 0);
        }
        public void AsyncLocalConvertCcmMesh(string input, string cas)
        {
            name = "star4ToFoam";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "star4ToFoam.exe"), "-case \"" + cas + "\" " + input, cas, 0);
        }
        public void AsyncLocalSurfaceAutoPatch(string cas, string input, string output, float angle)
        {
            name = "surfaceAutoPatch";
            AsyncLocalRun(Path.Combine(foamConfig.OpenfoamRootPath, "bin", "surfaceAutoPatch.exe"), "\"" + input.ToLower() + "\" \"" + output.ToLower() + "\" " + angle.ToString(), cas, 0);
        }
    }
}
