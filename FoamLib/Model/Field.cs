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
    public class Field : IFoamObject
    {
        List<Boundary> boundarys;
        readonly Dictionary<string, FoamDictionary> boundaryFieldDict = new Dictionary<string, FoamDictionary>();
        readonly Dictionary<string, FoamDictionaryFile> fieldFoamFile = new Dictionary<string, FoamDictionaryFile>();
        InternalField internalField = new InternalField();
        public Field()
        {
            boundarys = new List<Boundary>();
        }

        [CategoryAttribute("Field"), DisplayNameAttribute("边界条件列表")]
        public List<Boundary> Boundarys { get => boundarys; set => boundarys = value; }
 
        [Browsable(false)]
        public Dictionary<string, FoamDictionary> BoundaryFieldDict => boundaryFieldDict;

        [CategoryAttribute("Field"), DisplayNameAttribute("内部场")]
        public InternalField InternalField { get => internalField; set => internalField = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            boundaryFieldDict.Clear();
            fieldFoamFile.Clear();
            foreach (string field in Case.fieldNames)
            {
                FoamDictionaryFile f = new FoamDictionaryFile(FoamConst.GetZeroFieldFileName(foamRootPathName, field), monitor);
                f.Read();
                fieldFoamFile.Add(field, f);
                boundaryFieldDict.Add(field, f.Dictionary.GetByUrl("boundaryField"));
            }
            foreach (Boundary b in boundarys)
            {
                b.Update(boundaryFieldDict);
            }
            foreach(FoamDictionaryFile f in fieldFoamFile.Values)
            {
                f.Write(); 
            }
            internalField.Write(foamRootPathName, monitor);
            return true;
        }
    }
}
