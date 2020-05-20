using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
namespace FoamLib.Util
{
    public static class Zip
    {
        public static string CompressDirectory(string dir, string zip)
        {
            try
            {
                if (File.Exists(zip))
                    File.Delete(zip);
                ZipFile.CreateFromDirectory(dir, zip);
                return "ok";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public static string UncomporessZip(string zip, string dir)
        {
            try
            {
                ZipFile.ExtractToDirectory(zip, dir);
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

}
