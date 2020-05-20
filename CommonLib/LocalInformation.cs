using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib
{

    public class LocalInformation
    {
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public static string MainboardId
        {
            get
            {
                try
                {
                    ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
                    string serNumber = string.Empty;
                    string manufacturer = string.Empty;
                    string product = string.Empty;

                    foreach (ManagementObject m in mos.Get())
                    {
                        serNumber = m["SerialNumber"].ToString();//序列号
                        manufacturer = m["Manufacturer"].ToString();//制造商
                        product = m["Product"].ToString();//型号
                    }
                    return serNumber;
                }
                catch
                {
                    return "";
                }
            }
        }
        public static string HarddiskId
        {
            get
            {
                try
                {
                    string hdId = string.Empty;
                    ManagementClass hardDisk = new ManagementClass("win32_DiskDrive");
                    ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                    foreach (ManagementObject m in hardDiskC)
                    {
                        //hdId = m["Model"].ToString().Trim();
                        hdId = m.Properties["Model"].Value.ToString();//WDC WD800BB-56JKC0
                    }
                    return hdId;
                }
                catch
                {
                    return "";
                }
            }
        }
        public static string MacAddress
        {
            get
            {
                try
                {
                    string MoAddress = string.Empty;
                    ManagementClass networkAdapter = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection adapterC = networkAdapter.GetInstances();
                    foreach (ManagementObject m in adapterC)
                    {
                        if ((bool)m["IPEnabled"] == true)
                        {
                            MoAddress = m["MacAddress"].ToString().Trim();
                            m.Dispose();
                        }
                    }
                    return MoAddress;
                }
                catch
                {
                    return "";
                }
            }
        }
        public static string HardwareId
        {
            get
            {
                return GenerateMD5(HarddiskId + MainboardId/* + MacAddress*/).ToUpper();
            }
        }
        
    }
}
