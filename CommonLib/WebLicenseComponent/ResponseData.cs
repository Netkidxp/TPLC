using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fqh.CommonLib.WebLicenseComponent
{
    public enum LicenseState
    {
        NotRegisted,
        Expired,
        Ok
    }

    public class ResponseData
    {
        [JsonProperty("hardware_id")]
        public string hardware_id { get; set; }

        [JsonProperty("guid")]
        public string guid { get; set; }

        [JsonProperty("state")]
        public LicenseState state { get; set; }

        [JsonProperty("start_date")]
        public DateTime start_date { get; set; }

        [JsonProperty("day_count")]
        public int day_count { get; set; }
    }
}
