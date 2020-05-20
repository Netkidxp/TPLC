using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoamLib.IO;

namespace FoamLib.Util
{
    public class StlTool
    {
        IMonitor mon = null;
        public StlTool(IMonitor mon)
        {
            this.mon = mon;
        }

        public bool IsAscii(string fileName)
        {
            throw new NotImplementedException();
        }
        public List<string> GetSolidNameList(List<string> fileNames)
        {
            var res = new List<string>();
            foreach (var stl in fileNames)
            {
                res.Add(Path.GetFileNameWithoutExtension(stl));
            }
            return res;
        }
        public List<string> GetSolidNameList(string fileName)
        {
            try
            {
                var res = new List<string>();
                int i = 0;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("solid"))
                    {
                        string name = line.Replace("solid", "").Trim();
                        res.Add(name == "" ? "solid" + (i++) : name);
                    }
                }
                sr.Close();
                fs.Close();
                return res;
            }
            catch (Exception e)
            {
                if(mon!=null)
                    mon.ErrorLine(e.Message);
                return null;
            }
        }
        public bool Merge(string fileName, List<string> stlfileNames)
        {
            try
            {
                /*
                if (stlfileNames.Count == 0)
                    return false;
                if(stlfileNames.Count == 1)
                {
                    File.Copy(stlfileNames[0], fileName, true);
                    return true;
                }
                */
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                foreach(var stl in stlfileNames)
                {
                    string name = Path.GetFileNameWithoutExtension(stl);
                    using (StreamReader sr = File.OpenText(stl))
                    {
                        string buf = sr.ReadToEnd();
                        int si = buf.IndexOf('\n');
                        buf = "solid " + name + "\r\n" + buf.Substring(si + 1) + "\r\n";
                        sw.Write(buf);
                        /*
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith("solid"))
                                line = "solid " + name;
                            sw.WriteLine(line);
                        }
                        */

                        sr.Close();
                    }
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                if (mon != null)
                    mon.ErrorLine(e.Message);
                return false;
            }
        }
        public List<string> Decompose(string fileName)
        {
            List<string> names = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    int i = 0;
                    StreamWriter sw = null; ;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("solid"))
                        {
                            string name = line.Replace("solid", "").Trim();
                            if (name == "")
                                name = "solid" + (i++);
                            string tmpDir = IoUtil.GetTempleteFolder("geometry");
                            var stlfile = Path.Combine(tmpDir,name);
                            names.Add(stlfile);
                            sw = new StreamWriter(stlfile, false);
                            sw.WriteLine("solid " + name);
                        }
                        else if(line.StartsWith("endsolid"))
                        {
                            sw.WriteLine("endsolid");
                            sw.Close();
                            sw = null;
                        }
                        else
                        {
                            if (sw != null)
                                sw.WriteLine(line);
                        }
                    }
                }
                return names;
            }
            catch (Exception e)
            {
                if (mon != null)
                    mon.ErrorLine(e.Message);
                return null;
            }
        }
    }
}
