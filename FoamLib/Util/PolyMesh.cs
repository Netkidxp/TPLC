using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{

    public class PolyMesh
    {
        string vxtFileName = "";
        public PolyMesh(string vxt)
        {
            vxtFileName = vxt;
        }
        public List<string> GetAllPatchNames()
        {
            List<string> names = new List<string>();
            FoamDictionaryListFile f = new FoamDictionaryListFile(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName));
            f.Read();
            if(!f.Dictionary.IsNull)
            {
                foreach (KeyValuePair<string, FoamDictionary> k in f.Dictionary)
                    names.Add(k.Key);
            }
            return names;
        }
        public List<string> GetPatchNamesByType(string typeName)
        {
            List<string> names = new List<string>();
            FoamDictionaryListFile f = new FoamDictionaryListFile(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName));
            f.Read();
            if (!f.Dictionary.IsNull)
            {
                foreach (KeyValuePair<string, FoamDictionary> k in f.Dictionary)
                {
                    FoamDictionary cv = k.Value;
                    if (cv.Child("type").Data == typeName)
                        names.Add(k.Key);
                }
            }
            return names;
        }
        public bool ChangePatchName(string oldName, string newName)
        {
            /*
             FoamDictionaryListFile f = new FoamDictionaryListFile(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName));
             f.Read();
             if (!f.Dictionary.IsNull)
             {
                 FoamDictionary d = f.Dictionary.Item(oldName);
                 f.Dictionary.RemoveChild(oldName);
                 f.Dictionary.SetChild(newName, d);
             }
             f.Write();
             */
            try
            {
                StreamReader sr = new StreamReader(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName));
                string code = sr.ReadToEnd();
                sr.Close();
                code = code.Replace(oldName, newName);
                File.Delete(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName));
                StreamWriter sw = new StreamWriter(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName), false);
                sw.Write(code);
                sw.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SetBoundaryType(string name, BoundaryType bt)
        {
            FoamDictionaryListFile f = new FoamDictionaryListFile(FoamConst.GetBoundaryFileNameFromVxt(vxtFileName));
            f.Read();
            if (!f.Dictionary.IsNull)
            {
                foreach (KeyValuePair<string, FoamDictionary> k in f.Dictionary)
                {
                    if(k.Key == name)
                    {
                        FoamDictionary cv = k.Value;
                        cv.SetChild("type", bt.ToString());
                        cv.RemoveChild("inGroups");
                    }
                }
            }
            f.Write();
        }
        public enum BoundaryType
        {
            patch,
            wall,
            symmetry,
            empty,
            wedge,
            cylic
        }
    }
}
