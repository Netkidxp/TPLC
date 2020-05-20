using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class IoUtil
    {
        public static bool CopyDir(string srcPath, string aimPath)
        {
            //如果不存在目标路径，则创建之
            if (!System.IO.Directory.Exists(aimPath))
            {
                System.IO.Directory.CreateDirectory(aimPath);
            }
            //令目标路径为aimPath\srcPath
            string srcdir = System.IO.Path.Combine(aimPath, System.IO.Path.GetFileName(srcPath));
            //如果源路径是文件夹，则令目标目录为aimPath\srcPath\
            if (Directory.Exists(srcPath))
                srcdir += Path.DirectorySeparatorChar;
            // 如果目标路径不存在,则创建目标路径
            if (!System.IO.Directory.Exists(srcdir))
            {
                System.IO.Directory.CreateDirectory(srcdir);
            }
            //获取源文件下所有的文件
            String[] files = Directory.GetFileSystemEntries(srcPath);
            foreach (string element in files)
            {
                //如果是文件夹，循环
                if (Directory.Exists(element))
                    CopyDir(element, srcdir);
                else
                    File.Copy(element, srcdir + Path.GetFileName(element), true);
            }
            return true;
        }
        public static string GetTempleteFolder(string title = "")
        {
            string rs = Path.Combine(Path.GetTempPath(), title + "_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(rs);
            return rs;
        }
    }
}
