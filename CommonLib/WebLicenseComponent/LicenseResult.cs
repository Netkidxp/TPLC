using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.WebLicenseComponent
{
    public class LicenseResult
    {
        public LicenseResult(ResponseResult result, ResponseData data)
        {
            Result = result;
            Data = data;
        }

        public ResponseResult Result { get; set; }
        public ResponseData Data { get; set; }
    }
}
