using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.WebLicenseComponent
{
    public static class WebLisenceUtils
    {
        private static byte[] _rgbKey = ASCIIEncoding.ASCII.GetBytes("s5#_F8iQ4");
        private static byte[] _rgbIV = ASCIIEncoding.ASCII.GetBytes("$kO83Op+");

        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>
        private static bool RemoteCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }

        public static string Post(string url, string data, int timeout = 10000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.ClientCertificates.Add(X509Certificate.CreateFromCertFile(@"F:\foamlib\RSA\server_public.crt"));
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.Timeout = timeout;
            var byteData = Encoding.UTF8.GetBytes(data);
            var length = byteData.Length;
            request.ContentLength = length;
            var writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            return responseString;
        }

        public static LicenseResult CheckLocal(string url, Type type)
        {
            return Check(url, LocalInformation.HardwareId, type.GUID.ToString());
        }

        public static LicenseResult Check(string url, string hardwareId, string guid)
        {
            try
            {
                //const string Url = "http://www.netkidxp.cn:8000/checkLisence/";
                var dic = new SortedDictionary<string, string>
                {
                    {"hardware_id", hardwareId},
                    {"guid", guid},
                };
                var jsonParam = JsonConvert.SerializeObject(dic);
                string responseStr = Post(url, jsonParam);
                ResponseData response = JsonConvert.DeserializeObject<ResponseData>(responseStr);
                LicenseResult result = new LicenseResult(ResponseResult.SUCCESS, response);
                return result;
            }
            catch (Exception exp)
            {
                ResponseResult r = new ResponseResult(false, exp.Message);
                return new LicenseResult(r, null);
            }
        }

        public static LicenseResult RegistLocal(string url,Type type, int monthCount)
        {
            return Regist(url, LocalInformation.HardwareId, type.AssemblyQualifiedName, type.GUID.ToString(), monthCount);
        }
        public static LicenseResult Regist(string url, string hardwareId, string componentName, string guid, int dayCount)
        {
            try
            {
                //const string Url = "http://www.netkidxp.cn:8000/registLisence";
                var dic = new SortedDictionary<string, string>
                {
                    {"hardware_id", hardwareId},
                    {"guid", guid },
                    {"component_name",componentName },
                    {"day_count", dayCount.ToString()},
                };
                var jsonParam = JsonConvert.SerializeObject(dic);
                string responseStr = Post(url, jsonParam);
                ResponseData response = JsonConvert.DeserializeObject<ResponseData>(responseStr);
                LicenseResult result = new LicenseResult(ResponseResult.SUCCESS, response);
                return result;
            }
            catch (Exception exp)
            {
                ResponseResult r = new ResponseResult(false, exp.Message);
                return new LicenseResult(r, null);
            }
        }
        
    }
}
