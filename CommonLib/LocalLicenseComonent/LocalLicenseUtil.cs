using FoamLib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fqh.CommonLib.LocalLicenseComonent
{
    
    public class LocalLicenseUtil
    {
        const long pos = 0x1222;
        public static long GetUsedCount()
        {
            if (!File.Exists(CountFileName))
                return -1;
            try
            {
                FileStream fs = new FileStream(CountFileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                br.BaseStream.Seek(pos, SeekOrigin.Begin);
                long count =  br.ReadInt64();
                br.Close();
                fs.Close();
                return count;
            }
            catch(Exception)
            {
                return -1;
            }
        }
        public static bool IncUsedCount()
        {
            long uc = GetUsedCount();
            if (uc == -1)
                return false;
            return SetUsedCount(uc + 1);
        }
        public static bool ResetUsedCount()
        {
            return SetUsedCount(0);
        }
        public static bool SetUsedCount(long count)
        {
            if (!File.Exists(CountFileName))
                return false;
            try
            {
                FileStream fs = new FileStream(CountFileName, FileMode.Open);
                BinaryWriter br = new BinaryWriter(fs);
                br.BaseStream.Seek(pos, SeekOrigin.Begin);
                br.Write(count);
                br.Flush();
                br.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string CountFileName
        {
            get
            {
                string root = System.AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(root, "vtkCommonGeometry-7.1.dll");
            }
        }
        public static bool WriteLicense(LocalLicenseData licData, string fileName)
        {
            try
            {
                string tmp = Path.GetTempFileName();
                XmlSerializerTool<LocalLicenseData> bs = new XmlSerializerTool<LocalLicenseData>();
                bs.ToFile(licData, tmp);
                CryptoHelp.EncryptFile(tmp, fileName, "a89sd8jkk34dm%^%1239");
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static LocalLicenseData ReadLicense(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                
                string tmp = Path.GetTempFileName();
                CryptoHelp.DecryptFile(fileName, tmp, "a89sd8jkk34dm%^%1239");
                XmlSerializerTool<LocalLicenseData> bs = new XmlSerializerTool<LocalLicenseData>();
                LocalLicenseData licData = (LocalLicenseData)bs.FromFile(tmp);
                return licData;

            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static DateTime GetLocalDateTime()
        {
            return DateTime.Now;
        }
        public static DateTime GetNetDateTime()
        {
            //返回国际标准时间
            //只使用的TimerServer的IP地址，未使用域名
            string[,] TimerServer = new string[14, 2];
            int[] ServerTab = new int[] { 3, 2, 4, 8, 9, 6, 11, 5, 10, 0, 1, 7, 12 };

            TimerServer[0, 0] = "time-a.nist.gov";
            TimerServer[0, 1] = "129.6.15.28";
            TimerServer[1, 0] = "time-b.nist.gov";
            TimerServer[1, 1] = "129.6.15.29";
            TimerServer[2, 0] = "time-a.timefreq.bldrdoc.gov";
            TimerServer[2, 1] = "132.163.4.101";
            TimerServer[3, 0] = "time-b.timefreq.bldrdoc.gov";
            TimerServer[3, 1] = "132.163.4.102";
            TimerServer[4, 0] = "time-c.timefreq.bldrdoc.gov";
            TimerServer[4, 1] = "132.163.4.103";
            TimerServer[5, 0] = "utcnist.colorado.edu";
            TimerServer[5, 1] = "128.138.140.44";
            TimerServer[6, 0] = "time.nist.gov";
            TimerServer[6, 1] = "192.43.244.18";
            TimerServer[7, 0] = "time-nw.nist.gov";
            TimerServer[7, 1] = "131.107.1.10";
            TimerServer[8, 0] = "nist1.symmetricom.com";
            TimerServer[8, 1] = "69.25.96.13";
            TimerServer[9, 0] = "nist1-dc.glassey.com";
            TimerServer[9, 1] = "216.200.93.8";
            TimerServer[10, 0] = "nist1-ny.glassey.com";
            TimerServer[10, 1] = "208.184.49.9";
            TimerServer[11, 0] = "nist1-sj.glassey.com";
            TimerServer[11, 1] = "207.126.98.204";
            TimerServer[12, 0] = "nist1.aol-ca.truetime.com";
            TimerServer[12, 1] = "207.200.81.113";
            TimerServer[13, 0] = "nist1.aol-va.truetime.com";
            TimerServer[13, 1] = "64.236.96.53";
            int portNum = 13;
            string hostName;
            byte[] bytes = new byte[1024];
            int bytesRead = 0;
            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            for (int i = 0; i < 13; i++)
            {
                hostName = TimerServer[ServerTab[i], 0];
                try
                {
                    client.Connect(hostName, portNum);
                    System.Net.Sockets.NetworkStream ns = client.GetStream();
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                    client.Close();
                    break;
                }
                catch (System.Exception)
                {
                    return new DateTime(0);
                }
            }
            char[] sp = new char[1];
            sp[0] = ' ';
            System.DateTime dt = new DateTime();
            string str1;
            str1 = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
            string[] s;
            s = str1.Split(sp);
            dt = System.DateTime.Parse(s[1] + " " + s[2]);//得到标准时间
            //dt=dt.AddHours (8);//得到北京时间*/
            return dt;
        }
        

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        private DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }
    }


    public class CryptoHelpException : ApplicationException
    {
        public CryptoHelpException(string msg) : base(msg) { }
    }
    /// <summary>
    /// CryptHelp
    /// </summary>
    public class CryptoHelp
    {
        private const ulong FC_TAG = 0xFC010203040506CF;
        private const int BUFFER_SIZE = 128 * 1024;

        /// <summary>
        /// 检验两个Byte数组是否相同
        /// </summary>
        /// <param name="b1">Byte数组</param>
        /// <param name="b2">Byte数组</param>
        /// <returns>true－相等</returns>
        private static bool CheckByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; ++i)
                {
                    if (b1[i] != b2[i])
                        return false;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 创建Rijndael SymmetricAlgorithm
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt"></param>
        /// <returns>加密对象</returns>
        private static SymmetricAlgorithm CreateRijndael(string password, byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, "SHA256", 1000);

            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }
        /// <summary>
        /// 加密文件随机数生成
        /// </summary>
        private static RandomNumberGenerator rand = new RNGCryptoServiceProvider();
        /// <summary>
        /// 生成指定长度的随机Byte数组
        /// </summary>
        /// <param name="count">Byte数组长度</param>
        /// <returns>随机Byte数组</returns>
        private static byte[] GenerateRandomBytes(int count)
        {
            byte[] bytes = new byte[count];
            rand.GetBytes(bytes);
            return bytes;
        }
        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="inFile">待加密文件</param>
        /// <param name="outFile">加密后输入文件</param>
        /// <param name="password">加密密码</param>
        public static void EncryptFile(string inFile, string outFile, string password)
        {
            using (FileStream fin = File.OpenRead(inFile),
                fout = File.OpenWrite(outFile))
            {
                long lSize = fin.Length; // 输入文件长度
                int size = (int)lSize;
                byte[] bytes = new byte[BUFFER_SIZE]; // 缓存
                int read = -1; // 输入文件读取数量
                int value = 0;

                // 获取IV和salt
                byte[] IV = GenerateRandomBytes(16);
                byte[] salt = GenerateRandomBytes(16);

                // 创建加密对象
                SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
                sma.IV = IV;

                // 在输出文件开始部分写入IV和salt
                fout.Write(IV, 0, IV.Length);
                fout.Write(salt, 0, salt.Length);

                // 创建散列加密
                HashAlgorithm hasher = SHA256.Create();
                using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(), CryptoStreamMode.Write),
                    chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    BinaryWriter bw = new BinaryWriter(cout);
                    bw.Write(lSize);

                    bw.Write(FC_TAG);
                    // 读写字节块到加密流缓冲区
                    while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        cout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                    }
                    // 关闭加密流
                    chash.Flush();
                    chash.Close();
                    // 读取散列
                    byte[] hash = hasher.Hash;

                    // 输入文件写入散列
                    cout.Write(hash, 0, hash.Length);
                    // 关闭文件流
                    cout.Flush();
                    cout.Close();
                }
            }
        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="inFile">待解密文件</param>
        /// <param name="outFile">解密后输出文件</param>
        /// <param name="password">解密密码</param>
        public static void DecryptFile(string inFile, string outFile, string password)
        {
            // 创建打开文件流
            using (FileStream fin = File.OpenRead(inFile),
                fout = File.OpenWrite(outFile))
            {
                int size = (int)fin.Length;
                byte[] bytes = new byte[BUFFER_SIZE];
                int read = -1;
                int value = 0;
                int outValue = 0;
                byte[] IV = new byte[16];
                fin.Read(IV, 0, 16);
                byte[] salt = new byte[16];
                fin.Read(salt, 0, 16);

                SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
                sma.IV = IV;
                value = 32;
                long lSize = -1;

                // 创建散列对象, 校验文件
                HashAlgorithm hasher = SHA256.Create();
                using (CryptoStream cin = new CryptoStream(fin, sma.CreateDecryptor(), CryptoStreamMode.Read),
                    chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    // 读取文件长度
                    BinaryReader br = new BinaryReader(cin);
                    lSize = br.ReadInt64();
                    ulong tag = br.ReadUInt64();

                    if (FC_TAG != tag)
                        throw new CryptoHelpException("文件被破坏");

                    long numReads = lSize / BUFFER_SIZE;
                    long slack = (long)lSize % BUFFER_SIZE;

                    for (int i = 0; i < numReads; ++i)
                    {
                        read = cin.Read(bytes, 0, bytes.Length);
                        fout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                        outValue += read;
                    }
                    if (slack > 0)
                    {
                        read = cin.Read(bytes, 0, (int)slack);
                        fout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                        outValue += read;
                    }
                    chash.Flush();
                    chash.Close();
                    fout.Flush();
                    fout.Close();
                    byte[] curHash = hasher.Hash;
                    // 获取比较和旧的散列对象
                    byte[] oldHash = new byte[hasher.HashSize / 8];
                    read = cin.Read(oldHash, 0, oldHash.Length);
                    if ((oldHash.Length != read) || (!CheckByteArrays(oldHash, curHash)))
                        throw new CryptoHelpException("文件被破坏");
                }

                if (outValue != lSize)
                    throw new CryptoHelpException("文件大小不匹配");
            }
        }
    }
}
