using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.DataObjects
{
    public class Assignment
    {
        [JsonProperty("assignedDate")]
        public string AssignedDate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }
}
