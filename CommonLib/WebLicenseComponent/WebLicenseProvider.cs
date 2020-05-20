using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fqh.CommonLib.WebLicenseComponent
{
    public class WebLicenseProvider : LicenseProvider
    {
        const string Url = @"http://www.netkidxp.cn:8000/check/";
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            LicenseResult ret = WebLisenceUtils.Check(Url, LocalInformation.HardwareId, type.GUID.ToString());
            return new WebLicense(type, ret);
            /*
            if (context.UsageMode == LicenseUsageMode.Runtime || context.UsageMode == LicenseUsageMode.Designtime)
            {
                
                
                LicenseResult ret = WebLisenceUtils.Check(Url,LocalInformation.HardwareId, type.GUID.ToString());

                if (ret.Result.IsOK && ret.Data.state == LicenseState.Ok)
                {
                    return new WebLicense(type,ret);
                }
                else
                {
                    if (ret.Result.IsOK)
                    {
                        if (ret.Data == null)
                        {
                            //throw new Exception("fatal error, illegal response format");
                            MessageBox.Show("Fatal error, illegal response format", "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (ret.Data.state == LicenseState.Expired)
                        {
                            //throw new Exception("license expired");
                            MessageBox.Show("License expired", "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (ret.Data.state == LicenseState.NotRegisted)
                        {
                            //throw new Exception("custom not registed");
                            MessageBox.Show("Custom has not registed", "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //throw new Exception(ret.WebResult.Error);
                        MessageBox.Show(ret.Result.Message, "License error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return null;
                }
               
        }
         */
        }
    }
}
