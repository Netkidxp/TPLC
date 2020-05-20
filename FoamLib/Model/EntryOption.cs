using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;


namespace FoamLib.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EntryOption : IFoamObject
    {
        string fileName;
        string entryName;
        string entryValue;
        static int count = 0;
        public EntryOption()
        {
            fileName = "";
            entryName = "entry" + count++;
            entryValue = "";
        }

        public EntryOption(string fileName, string entryName, string entryValue)
        {
            this.fileName = fileName;
            this.entryName = entryName;
            this.entryValue = entryValue;
        }
        [CategoryAttribute("Entry Option"), DisplayNameAttribute("文件")]
        public string FileName { get => fileName; set => fileName = value; }

        [CategoryAttribute("Entry Option"), DisplayNameAttribute("键名称")]
        public string EntryName { get => entryName; set => entryName = value; }

        [CategoryAttribute("Entry Option"), DisplayNameAttribute("键值值")]
        public string EntryValue { get => entryValue; set => entryValue = value; }

        public virtual bool Write(string foamRootPathName, IMonitor monitor)
        {
            string fName = Path.Combine(foamRootPathName, fileName);
            if(!File.Exists(fName))
                return false;
            FoamDictionaryFile f = new FoamDictionaryFile(fName, monitor);
            f.Read();
            f.Dictionary.SetChild(entryName, entryValue);
            f.Write();
            return true;

        }
    }
}
