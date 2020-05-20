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
    public class Boundary : IBoundaryObject
    {

        public enum BOUNDARY_TYPE
        {
            Wall,
            Upface,
            Plug,
            Symmetry
        }
        string patchName = "";
        protected BOUNDARY_TYPE boundaryType;
        [NonSerialized]
        public OnBoundaryTypeChange onTypeChange = null;
        public Boundary(string patchName)
        {
            this.patchName = patchName;
            this.boundaryType = BOUNDARY_TYPE.Wall;
        }

        [CategoryAttribute("Boundary"), DisplayNameAttribute("边界类型")]
        public virtual BOUNDARY_TYPE Type {
            get => boundaryType;
            set{
                if(onTypeChange!=null)
                {
                    onTypeChange(this, patchName, boundaryType, value);
                }
            }}

        [CategoryAttribute("Boundary"), DisplayNameAttribute("边界名称")]
        public string PatchName { get => patchName;}

        public virtual bool Update(Dictionary<string, FoamDictionary> dicts, IMonitor mon = null)
        {
            return true;
        }
    }
    public delegate void OnBoundaryTypeChange(Boundary me, string patchName, Boundary.BOUNDARY_TYPE from, Boundary.BOUNDARY_TYPE to);
}

