using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.WebLicenseComponent
{
    public class WebLicense : License
    {
        Type type = null;
        LicenseResult licenseResult = null;

        public WebLicense(Type type)
        {
            this.type = type ?? throw new NullReferenceException();
        }

        public WebLicense(Type type, LicenseResult licenseResult) : this(type)
        {
            this.LicenseResult = licenseResult ?? throw new NullReferenceException();
        }

        public override string LicenseKey => type.GUID.ToString();

        public LicenseResult LicenseResult { get => licenseResult; set => licenseResult = value; }

        public override void Dispose()
        {
            type = null;
            LicenseResult = null;
        }
    }
}
