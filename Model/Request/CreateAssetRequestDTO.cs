using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Model.Request
{
    public class CreateAssetRequestDTO
    {
        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("specification")]
        public string Specification { get; set; }

        [JsonProperty("installDate")]
        public string InstallDate { get; set; }

        [JsonProperty("assetState")]
        public string AssetState { get; set; }
    }
}
