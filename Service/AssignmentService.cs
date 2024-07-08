using AssetManagement.Constants;
using AssetManagement.Library.API;
using AssetManagement.Model.Request;
using AssetManagement.Model.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Service
{
    public class AssignmentService
    {
        private readonly APIClient _client;

        public AssignmentService(APIClient client)
        {
            _client = client;
        }

        public async Task<RestResponse> CreateNewAssignmentAsync(CreateAssetRequestDTO asset, string token)
        {
            return await _client.CreateRequest(APIConstant.CreateNewAssetEndPoint)
                    .AddHeader("accept", ContentType.Json)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(token)
                    .AddBody(asset, ContentType.Json)
                    .ExecutePostAsync();
        }

        public async Task<RestResponse> ResponseAssignmentAsync(string assignmentId, string status)
        {
            string endpoint = String.Format(APIConstant.ResponseAssignmentEndPoint, assignmentId);
            return await _client.CreateRequest(endpoint)
                    .AddHeader("accept", ContentType.Json)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddParameter("status", status)
                    .ExecutePatchAsync();
        }


    }
}
