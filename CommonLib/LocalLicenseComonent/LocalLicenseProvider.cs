using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.LocalLicenseComonent
{
    public class LocalLicenseProvider : LicenseProvider
    {
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory;
            LocalLicenseData licData = LocalLicenseUtil.ReadLicense(Path.Combine(root, "license.lic"));
            if (licData != null)
                licData.UsedCount = LocalLicenseUtil.GetUsedCount();
            return new LocalLicense(licData);
        }
    }
}
