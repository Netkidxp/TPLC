using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PhaseInterfacial : IFoamObject
    {
        double sigma;
        double aspectRatio;
        DRAG_TYPE drag;
        VIRTUAL_MASS_TYPE virtualMass;
        HEAT_TRANSFER_TYPE heatTransfer;
        LIFT_TYPE lift;
        double pMin;

        public PhaseInterfacial()
        {
            sigma = 0.07;
            aspectRatio = 1.0;
            drag = DRAG_TYPE.SchillerNaumann;
            virtualMass = VIRTUAL_MASS_TYPE.ConstantCoefficient;
            heatTransfer = HEAT_TRANSFER_TYPE.RanzMarshall;
            lift = LIFT_TYPE.NoLift;
            pMin = 10000;
        }

        public PhaseInterfacial(double sigma, double aspectRatio, DRAG_TYPE drag, VIRTUAL_MASS_TYPE virtualMass, HEAT_TRANSFER_TYPE heatTransfer, LIFT_TYPE lift, double pMin)
        {
            this.sigma = sigma;
            this.aspectRatio = aspectRatio;
            this.drag = drag;
            this.virtualMass = virtualMass;
            this.heatTransfer = heatTransfer;
            this.lift = lift;
            this.pMin = pMin;
        }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("Sigma")]
        public double Sigma { get => sigma; set => sigma = value; }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("纵横比")]
        public double AspectRatio { get => aspectRatio; set => aspectRatio = value; }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("拽力模型")]
        public DRAG_TYPE Drag { get => drag; set => drag = value; }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("虚拟质量")]
        public VIRTUAL_MASS_TYPE VirtualMass { get => virtualMass; set => virtualMass = value; }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("热交换")]
        public HEAT_TRANSFER_TYPE HeatTransfer { get => heatTransfer; set => heatTransfer = value; }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("升力模型")]
        public LIFT_TYPE Lift { get => lift; set => lift = value; }
        [CategoryAttribute("PhaseInterfacial"), DisplayNameAttribute("pMin")]
        public double PMin { get => pMin; set => pMin = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            bool res = true;
            FoamDictionaryFile fPhaseProperties = new FoamDictionaryFile(FoamConst.GetConstantChildPathName(foamRootPathName, "phaseProperties"), monitor);
            fPhaseProperties.Read();
            fPhaseProperties.Dictionary.Child("sigma").Child("1").Data = Sigma.ToString();
            fPhaseProperties.Dictionary.Child("aspectRatio").Child("(gas in steel)").Child("E0").Data = AspectRatio.ToString();
            fPhaseProperties.Dictionary.Child("aspectRatio").Child("(steel in gas)").Child("E0").Data = AspectRatio.ToString();
            fPhaseProperties.Write();
            res = res && WriteDrag(foamRootPathName, monitor);
            res = res && WriteVirtualMass(foamRootPathName, monitor);
            res = res && WriteHeatTransfer(foamRootPathName, monitor);
            res = res && WriteLift(foamRootPathName, monitor);
            return res;
        }
        protected virtual bool WriteDrag(string foamRootPathName, IMonitor monitor)
        {

            return true;
        }
        protected virtual bool WriteVirtualMass(string foamRootPathName, IMonitor monitor)
        {
            return true;
        }
        protected virtual bool WriteHeatTransfer(string foamRootPathName, IMonitor monitor)
        {
            return true;
        }
        protected virtual bool WriteLift(string foamRootPathName, IMonitor monitor)
        {
            return true;
        }
    }
    public enum DRAG_TYPE
    {
        SchillerNaumann,
        GidaspowSchillerNaumann,
        TomiyamaCorrelated,
        WenYu
    }
    public enum VIRTUAL_MASS_TYPE
    {
        NoVirtualMass,
        ConstantCoefficient,
        Lamb
    }
    public enum HEAT_TRANSFER_TYPE
    {
        RanzMarshall,
        SphericalHeatTransfer
    }
    public enum LIFT_TYPE
    {
        NoLift,
        ConstantLiftCoefficient,
        LegendreMagnaudet,
        Moraga,
        TomiyamaLift
    }
}
