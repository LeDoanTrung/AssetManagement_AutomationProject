using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.DataObjects
{
    public class SearchKeyword
    {
        [JsonProperty("keyword")]
        public string Keyword { get; set; }
    }
}
