using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.WebLicenseComponent
{
    public class ResponseResult
    {
        public ResponseResult(bool result, string error)
        {
            IsOK = result;
            Message = error;
        }
        public static ResponseResult SUCCESS
        {
            get
            {
                return new ResponseResult(true, "");
            }
        }
        public bool IsOK { get; set; }
        public string Message { get; set; }
    }
}
