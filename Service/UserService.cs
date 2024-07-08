using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library.API;
using AssetManagement.Library.ShareData;
using AssetManagement.Model.Request;
using AssetManagement.Model.Response;
using Newtonsoft.Json;
using RestSharp;


namespace AssetManagement.Service
{
    public class UserService
    {
        private readonly APIClient _client;

        public UserService(APIClient client)
        {
            _client = client;
        }

        public async Task<RestResponse> SignInAsync(string username, string password)
        {
            return await _client.CreateRequest(APIConstant.SignInEndPoint)
                .AddHeader("accept", ContentType.Json)
                .AddHeader("Content-Type", ContentType.Json)
                .AddBody(new SignInRequestDTO
                {
                    UserName = username,
                    Password = password
                }, ContentType.Json)
                .ExecutePostAsync();
        }

        public async Task StoreTokenAsync(string accountKey, Account account)
        {
            if (DataStorage.GetData(accountKey) is null)
            {
                var response = await this.SignInAsync(account.UserName, account.Password);

                response.VerifyStatusCodeOK();

                var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
                DataStorage.SetData(accountKey, "Bearer " + result["token"]);
            }
        }

        public string GetToken(string accountKey)
        {
            if (DataStorage.GetData(accountKey) is null)
            {
                throw new Exception("Token is not stored with account " + accountKey);
            }

            return (string)DataStorage.GetData(accountKey);
        }


    }
}
