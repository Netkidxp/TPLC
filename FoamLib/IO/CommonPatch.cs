using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.Model;
namespace FoamLib.IO
{
    public static class CommonPatch
    {
        public static FoamDictionary typePatchField(string typeName, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            d.SetChild("type", typeName);
            return d;
        }
        public static FoamDictionary typeValuePatchField<T>(string typeName, T value, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            d.SetChild("type", typeName);
            d.SetChild("value", value.ToString());
            return d;
        }
        public static FoamDictionary internalFieldValuePatchField(string typeName, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            d.SetChild("type", typeName);
            d.SetChild("value", "$internalField");
            return d;
        }
        public static FoamDictionary zeroGradient(IMonitor mon = null)
        {
            return typePatchField("zeroGradient");
        }
        public static FoamDictionary slip(IMonitor mon = null)
        {
            return typePatchField("slip", mon);
        }
        public static FoamDictionary noSlip(IMonitor mon = null)
        {
            return typePatchField("noSlip", mon);
        }
        public static FoamDictionary symmetry(IMonitor mon = null)
        {
            return typePatchField("symmetry", mon);
        }
        public static FoamDictionary uniformFixedValue<T>(T value, IMonitor mon = null)
        {
            return typeValuePatchField<UniformValue<T>>("fixedValue", new UniformValue<T>(value), mon);
        }
        public static FoamDictionary fixedValue<T>(T value, IMonitor mon = null)
        {
            return typeValuePatchField<T>("fixedValue", value, mon);
        }
        public static FoamDictionary uniformInletOutlet<T>(string phi, T inletValue, T value, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            d.SetChild("type", "inletOutlet");
            d.SetChild("phi", phi);
            d.SetChild("inletValue", "uniform " + inletValue);
            d.SetChild("value", "uniform " + value);
            return d;
        }
        public static FoamDictionary inletOutlet<T>(string phi, T inletValue, T value, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            d.SetChild("type", "inletOutlet");
            d.SetChild("phi", phi);
            d.SetChild("inletValue", inletValue.ToString());
            d.SetChild("value", value.ToString());
            return d;
        }
        public static FoamDictionary calculated(IMonitor mon = null)
        {
            return internalFieldValuePatchField("calculated", mon);
        }
        public static FoamDictionary compressible_alphaWallFunction(double prt, IMonitor mon = null)
        {
            FoamDictionary d = internalFieldValuePatchField("compressible::alphatWallFunction");
            d.SetChild("Prt", prt.ToString());
            return d;
        }
        public static FoamDictionary epsilonWallFunction(IMonitor mon = null)
        {
            return internalFieldValuePatchField("epsilonWallFunction", mon);
        }
        public static FoamDictionary kqRWallFunction(IMonitor mon = null)
        {
            return internalFieldValuePatchField("kqRWallFunction", mon);
        }
        public static FoamDictionary nutkWallFunction(IMonitor mon = null)
        {
            return internalFieldValuePatchField("nutkWallFunction", mon);
        }
        public static FoamDictionary fixedFluxPressure(IMonitor mon = null)
        {
            return internalFieldValuePatchField("fixedFluxPressure", mon);
        }
        public static FoamDictionary prghPressure(IMonitor mon = null)
        {
            FoamDictionary d = internalFieldValuePatchField("prghPressure");
            d.SetChild("p", "$internalField");
            return d;
        }
        public static FoamDictionary pressureInletOutletVelocity(string phi, IMonitor mon = null)
        {
            FoamDictionary d = internalFieldValuePatchField("pressureInletOutletVelocity", mon);
            d.SetChild("phi", phi);
            return d;
        }

        public static FoamDictionary temperatureWall(double temperature, IMonitor mon = null)
        {
            return uniformFixedValue<double>(temperature, mon);
        }

        public static string thicknessLayersStr(List<WallLayer> wallLayers)
        {
            var thicknessLayersStr = "(";
            foreach (var l in wallLayers)
            {
                thicknessLayersStr += l.Thickness + " ";
            }
            if (thicknessLayersStr.EndsWith(" "))
            {
                thicknessLayersStr = thicknessLayersStr.Substring(0, thicknessLayersStr.Length - 1) + ")";
            }
            else
                thicknessLayersStr += ")";
            return thicknessLayersStr;
        }

        public static string kappaLayersStr(List<WallLayer> wallLayers)
        {
            var kappaLayersStr = "(";
            foreach (var l in wallLayers)
            {
                kappaLayersStr += l.Conductivity + " ";
            }
            if (kappaLayersStr.EndsWith(" "))
            {
                kappaLayersStr = kappaLayersStr.Substring(0, kappaLayersStr.Length - 1) + ")";
            }
            else
                kappaLayersStr += ")";
            return kappaLayersStr;
        }

        public static FoamDictionary powerWall(List<WallLayer> wallLayers, double power, IMonitor mon = null)
        {
            FoamDictionary d = typePatchField("externalWallHeatFluxTemperature", mon);
            d.SetChild("mode", "power");
            d.SetChild("Q", "uniform " + power);
            
            d.SetChild("thicknessLayers", thicknessLayersStr(wallLayers));
            d.SetChild("kappaLayers", kappaLayersStr(wallLayers));
            d.SetChild("kappaMethod", "fluidThermo");
            d.SetChild("value", "$internalField");
            return d;
        }

        public static FoamDictionary fluxWall(List<WallLayer> wallLayers, double flux, IMonitor mon = null)
        {
            FoamDictionary d = typePatchField("externalWallHeatFluxTemperature", mon);
            d.SetChild("mode", "flux");
            d.SetChild("q", "uniform " + flux);

            d.SetChild("thicknessLayers", thicknessLayersStr(wallLayers));
            d.SetChild("kappaLayers", kappaLayersStr(wallLayers));
            d.SetChild("kappaMethod", "fluidThermo");
            d.SetChild("value", "$internalField");
            return d;
        }

        public static FoamDictionary coefficientWall(List<WallLayer> wallLayers, double h, double Ta, IMonitor mon = null)
        {
            FoamDictionary d = typePatchField("externalWallHeatFluxTemperature", mon);
            d.SetChild("model", "coefficient");
            d.SetChild("h", "uniform " + h);
            d.SetChild("Ta", "uniform " + Ta);
            d.SetChild("thicknessLayers", thicknessLayersStr(wallLayers));
            d.SetChild("kappaLayers", kappaLayersStr(wallLayers));
            d.SetChild("kappaMethod", "fluidThermo");
            d.SetChild("value", "$internalField");
            return d;
        }
        public static FoamDictionary surfaceNormalFixedValue<T>(T v, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            d.SetChild("type", "surfaceNormalFixedValue");
            d.SetChild("refValue",new UniformValue<T>(v).ToString());
            return d;
        }
    }
}
