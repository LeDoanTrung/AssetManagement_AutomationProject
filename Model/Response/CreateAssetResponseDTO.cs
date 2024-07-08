using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Model.Response
{
    public class DataAsset
    {
        [JsonProperty("assetCode")]
        public string AssetCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("installedDate")]
        public string InstalledDate { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("specification")]
        public string Specification { get; set; }
    }

    public class Root
    {
        [JsonProperty("data")]
        public DataAsset Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
