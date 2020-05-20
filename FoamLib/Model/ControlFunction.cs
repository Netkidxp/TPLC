using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.UI;

namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ControlFunction : IFoamObject
    {
        string functionName;
        string functionBody;

        public ControlFunction()
        {
            functionName = "function1";
            functionBody = "";
        }

        [CategoryAttribute("Control Function"), DisplayNameAttribute("函数名称")]
        public string FunctionName { get => functionName; set => functionName = value; }

        [CategoryAttribute("Control Function"), EditorAttribute(typeof(MultilineTextEditor), typeof(System.Drawing.Design.UITypeEditor)), DisplayNameAttribute("函数体")]
        public string FunctionBody { get => functionBody; set => functionBody = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetControlDictFileName(foamRootPathName), monitor);
            f.Read();
            FoamDictionary d = f.Dictionary.GetByUrl("functions");
            d.SetChild(functionName, "{\n" + functionBody + "\n}");
            f.Write();
            return true;
        }
    }
}
