using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;
using FoamLib.Model;

namespace FoamLib.Util
{
    public class ConfigIO
    {
        //const string ConfigFileName = @"config";
        FoamDictionaryFile file;
        private string FileName { get; set; }
        
        public ConfigIO(string fileName)
        {
            this.FileName = fileName;
        }
        private FoamDictionaryFile ConfigFile
        {
            get
            {
                if(file == null)
                {
                    if (!File.Exists(FileName))
                    {
                        FileStream fs = File.Create(FileName);
                        fs.Close();
                    }
                    file = new FoamDictionaryFile(FileName);
                    file.Read();
                }
                return file;
            }
        }
        private FoamDictionary Dict
        {
            get
            {
                return ConfigFile.Dictionary;
            }
        }
        public string GetValue(string path, string name)
        {
            FoamDictionary d = Dict.LookupByUrl(path);
            if(d.IsNull)
            {
                return "";
            }
            else
            {
                return d.Child(name).Data;
            }
        }
        public string GetValue(string path, string name, string defaultValue)
        {
            FoamDictionary d = Dict.LookupByUrl(path);
            if (d.IsNull)
            {
                return defaultValue;
            }
            else
            {
                return d.Child(name).Data;
            }
        }
        public List<EnvironmentItem> GetValues(string path)
        {
            List<EnvironmentItem> values = new List<EnvironmentItem>();
            FoamDictionary d = Dict.LookupByUrl(path);
            if (d.IsNull)
            {
                return values;
            }
            else
            {
                foreach(KeyValuePair<string,FoamDictionary> p in d)
                {
                    values.Add(new EnvironmentItem(p.Key, p.Value.Data));
                }
                return values;
            }
        }
        public List<EnvironmentItem> GetValues(string path, List<EnvironmentItem> defaultValues)
        {
            List<EnvironmentItem> values = new List<EnvironmentItem>();
            FoamDictionary d = Dict.LookupByUrl(path);
            if (d.IsNull)
            {
                return defaultValues;
            }
            else
            {
                foreach (KeyValuePair<string, FoamDictionary> p in d)
                {
                    values.Add(new EnvironmentItem(p.Key, p.Value.Data));
                }
                return values;
            }
        }
        public void SetValue(string path, string name, string value)
        {
            Dict.GetByUrl(path).SetChild(name, value);
            ConfigFile.Write();
        }
        public void SetValues(string path, List<EnvironmentItem> values)
        {
            FoamDictionary d = Dict.GetByUrl(path);
            d.Clear();
            foreach (EnvironmentItem p in values)
            {
                d.SetChild(p.Name, p.Value);
            }
            ConfigFile.Write();
        }
    }
}
