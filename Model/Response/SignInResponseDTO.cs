using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Model.Response
{
    public class Data
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("isChangePassword")]
        public bool IsChangePassword { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }

    public class SignInResponseDTO
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
