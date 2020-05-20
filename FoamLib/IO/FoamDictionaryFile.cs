using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.IO
{
    public class FoamDictionaryFile : FoamFile
    {
        FoamDictionary dictionary = new FoamDictionary();
        IMonitor mon = null;
        public FoamDictionaryFile(string fileName, IMonitor mon = null):base(fileName)
        {
            this.mon = mon;
        }
        public FoamDictionaryFile(IMonitor mon = null)
        {
            this.mon = mon;
        }
        public FoamDictionary Dictionary { get => dictionary; set => dictionary = value; }

        public override void Read()
        {
            StreamReader sr = new StreamReader(fileName, new UTF8Encoding(false));
            string code = sr.ReadToEnd();
            sr.Close();
            FoamDictionary d = FoamDictionary.DecodeDictionary(code, false, mon);
            if (d.IsNull && mon != null)
                mon.ErrorLine("decode foam file error, file name: \"" + fileName + "\"");
            Dictionary.CopyFrom(d);
        }
        public override string ToString()
        {
            return Dictionary.ToString();
        }
        public override void WriteBody(StreamWriter writer)
        {
            writer.Write(Dictionary);
        }
    }
}
