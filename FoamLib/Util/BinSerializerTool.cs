using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class BinSerializerTool : SerializerToolBase
    {
        public override object FromFile(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                fs.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[fs.Length + 1];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();
                MemoryStream memory = new MemoryStream(data);
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(memory);
            }
            catch(Exception e)
            {
                lastError = e.Message;
                return null;
            }
        }

        public override bool ToFile(object obj, string fileName)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream memory = new MemoryStream();
                bf.Serialize(memory, obj);
                byte[] bytes = memory.GetBuffer();
                memory.Close();
                FileStream fs = new FileStream(fileName, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                fs.Close();
                return true;
            }
            catch(Exception e)
            {
                lastError = e.Message;
                return false;
            }
        }
    }
}
