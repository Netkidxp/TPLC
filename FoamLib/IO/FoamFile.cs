using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.IO
{
    public class FoamFile
    {
        
        const string FOAM_ANNO = @"";

        const string SPLITLINE = @"";
        
        protected string fileName = "";

        public string FileName { get => fileName; set => fileName = value; }

        public FoamFile()
        {

        }
        /*
        public FoamFile(StreamReader reader)
        {
            this.reader = reader;
        }
        */
        public FoamFile(string fileName)
        {
            this.fileName = fileName;
        }
        public virtual void Read()
        {
            
        }
        public virtual void WriteBody(StreamWriter writer)
        {

        }
        public void Write(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            Write(sw);
            sw.Close();
        }
        public void Write(StreamWriter writer)
        {
            //writer.Write(FOAM_ANNO);
            WriteBody(writer);
            //writer.Write(SPLITLINE);
            writer.Flush();
        }
        public void Write()
        {
            if (fileName != null)
                Write(fileName);
        }
    }
}
