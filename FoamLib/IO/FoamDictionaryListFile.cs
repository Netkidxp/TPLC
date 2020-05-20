using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoamLib.IO
{
    public class FoamDictionaryListFile : FoamFile
    {

        FoamDictionary dictionary = null;
        string headerStr = "";
        IMonitor monitor = null;

        public FoamDictionary Dictionary
        {
            get => dictionary;
            set => dictionary = value;
        }

        public FoamDictionaryListFile(string fileName, IMonitor mon = null) : base(fileName)
        {
            monitor = mon;
        }

        public FoamDictionaryListFile(IMonitor mon = null)
        {
            monitor = mon;
        }

        public override void Read()
        {
            StreamReader sr = new StreamReader(fileName, new UTF8Encoding(false));
            string code = sr.ReadToEnd();
            sr.Close();
            headerStr = FoamConst.ReadHeader(code);
            code = FoamConst.ClearHeader(code);
            code = FoamConst.GetListContent(code);
            Dictionary = FoamDictionary.DecodeDictionary(code, false, monitor);
        }

        public override void WriteBody(StreamWriter writer)
        {
            writer.Write(headerStr + "\r\n");
            writer.WriteLine(Dictionary.Count.ToString());
            writer.WriteLine("(");
            writer.Write(Dictionary);
            writer.Write(")");
        }
    }
}
