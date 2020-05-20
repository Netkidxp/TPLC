using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.LocalLicenseComonent
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LocalLicenseData : IDisposable
    {
        string hardwareId = "";
        DateTime startDate = DateTime.Now;
        DateTime expiredDate = DateTime.Now.AddDays(30);
        long maxCount = 100;
        long usedCount = 0;

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime ExpiredDate { get => expiredDate; set => expiredDate = value; }
        public long MaxCount { get => maxCount; set => maxCount = value; }
        public long UsedCount { get => usedCount; set => usedCount = value; }
        public string HardwareId { get => hardwareId; set => hardwareId = value; }

        public void Dispose()
        {
           
        }
    }
}
