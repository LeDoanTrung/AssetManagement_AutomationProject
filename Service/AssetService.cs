using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.Library.API;
using AssetManagement.Model.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Service
{
    public class AssetService
    {
        private readonly APIClient _client;

        public AssetService(APIClient client)
        {
            _client = client;
        }

        public async Task<RestResponse> CreateNewAssetAsync(CreateAssetRequestDTO asset, string token)
        {
            return await _client.CreateRequest(APIConstant.CreateNewAssetEndPoint)
                    .AddHeader("accept", ContentType.Json)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(token)
                    .AddBody(asset, ContentType.Json)
                    .ExecutePostAsync();
        }


    }
}
