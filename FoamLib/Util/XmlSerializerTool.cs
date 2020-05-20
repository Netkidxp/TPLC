using FoamLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FoamLib.Util
{
    public class XmlSerializerTool<T> : SerializerToolBase
    {
        public override object FromFile(string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(fileName))
                {
                    return serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                lastError = e.Message;
                return null;
            }
        }

        
        public override bool ToFile(object obj, string fileName)
        {
            try
            {
                if(!(obj is T))
                {
                    lastError = "The object serialized is not a " + typeof(T).ToString();
                    return false;
                }
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                string content = string.Empty;
                //serialize
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, obj);
                    content = writer.ToString();
                }
                //save to file
                using (StreamWriter stream_writer = new StreamWriter(fileName))
                {
                    stream_writer.Write(content);
                }
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
