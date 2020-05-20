using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FoamLib.IO;


namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Case : IFoamObject
    {
        LadleGeometry geometry;
        Phases phases;
        PhaseInterfacial phaseInterfacial;
        Field field;
        Mixing mixing;
        OperationCondition operationCondition;
        Pimple pimple;
        SolveControl control;
        OtherOptions otherOptions;
        
        public static string[] fieldNames = {
            "alpha.gas",
            "alphat.gas",
            "alphat.steel",
            "epsilon.gas",
            "epsilon.steel",
            "epsilonm",
            "k.gas",
            "k.steel",
            "km",
            "nut.gas",
            "nut.steel",
            "p",
            "p_rgh",
            "T.gas",
            "T.steel",
            "Theta",
            "U.gas",
            "U.steel",
            "tracer"
        };
        public Case()
        {
            geometry = new LadleGeometry();
            Phases = new Phases(Gas.Ar, MoltenSteel.Q235);
            phaseInterfacial = new PhaseInterfacial();
            Field = new Field();
            mixing = new Mixing();
            operationCondition = new OperationCondition();
            Pimple = new Pimple();
            Control = new SolveControl();
            otherOptions = new OtherOptions();
        }
        [CategoryAttribute("几何结构"), DescriptionAttribute("钢包结构"), DisplayNameAttribute("钢包结构")]
        public LadleGeometry Geometry { get => geometry; set => geometry = value; }

        [CategoryAttribute("模型"), DescriptionAttribute("相"), DisplayNameAttribute("相")]
        public Phases Phases { get => phases; set => phases = value; }

        [CategoryAttribute("模型"), DescriptionAttribute("相间作用"),DisplayNameAttribute("相间作用")]
        public PhaseInterfacial PhaseInterfacial { get => phaseInterfacial; set => phaseInterfacial = value; }

        [CategoryAttribute("模型"), DescriptionAttribute("场"), DisplayNameAttribute("场")]
        public Field Field { get => field; set => field = value; }

        [CategoryAttribute("模型"), DescriptionAttribute("混匀效率"), DisplayNameAttribute("混匀效率")]
        public Mixing Mixing { get => mixing; set => mixing = value; }

        [CategoryAttribute("模型"), DescriptionAttribute("操作条件"), DisplayNameAttribute("操作条件")]
        public OperationCondition OperationCondition { get => operationCondition; set => operationCondition = value; }

        [CategoryAttribute("求解"), DescriptionAttribute("Pimple参数"), DisplayNameAttribute("Pimple参数")]
        public Pimple Pimple { get => pimple; set => pimple = value; }

        [CategoryAttribute("求解"), DescriptionAttribute("求解控制"), DisplayNameAttribute("求解控制")]
        public SolveControl Control { get => control; set => control = value; }

        [CategoryAttribute("其他"), DescriptionAttribute("其他参数"), DisplayNameAttribute("其他参数")]
        public OtherOptions OtherOptions { get => otherOptions; set => otherOptions = value; }



        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            bool res = true;
            res = res && phases.Write(foamRootPathName, null);
            res = res && phaseInterfacial.Write(foamRootPathName, null);
            res = res && field.Write(foamRootPathName, monitor);
            res = res && mixing.Write(foamRootPathName, monitor);
            res = res && operationCondition.Write(foamRootPathName, monitor);
            res = res && pimple.Write(foamRootPathName, monitor);
            res = res && control.Write(foamRootPathName, monitor);
            res = res && otherOptions.Write(foamRootPathName, monitor);
            return res;
        }
    }
}
