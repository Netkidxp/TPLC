using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.LocalLicenseComonent
{
    
    public class LocalLicense : License
    {
        LocalLicenseData licenseData = null;

        public LocalLicense(LocalLicenseData licenseData)
        {
            this.LicenseData = licenseData;
        }

        public override string LicenseKey => "";

        public LocalLicenseData LicenseData { get => licenseData; set => licenseData = value; }

        public override void Dispose()
        {
            LicenseData.Dispose();
            LicenseData = null;
        }
    }
}
