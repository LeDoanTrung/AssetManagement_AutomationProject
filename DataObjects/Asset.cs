using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.DataObjects
{
    public class Asset
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("specification")]
        public string Specification { get; set; }

        [JsonProperty("installedDate")]
        public string InstalledDate { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

    }
}
