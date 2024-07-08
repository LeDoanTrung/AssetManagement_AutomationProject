using RestSharp.Authenticators.OAuth2;
using RestSharp.Authenticators;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace AssetManagement.Library.API
{
    public class APIClient
    {
        private readonly RestClient _client;

        public RestRequest Request;

        private RestClientOptions requestOption;

        public APIClient(RestClient client)
        {
            _client = client;
            Request = new RestRequest();
        }

        public APIClient(string url)
        {
            var options = new RestClientOptions(url);
            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
            Request = new RestRequest();
        }

        public APIClient(RestClientOptions options)
        {
            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
            Request = new RestRequest();
        }

        public APIClient SetBasisAuthentication(string username, string password)
        {
            requestOption.Authenticator = new HttpBasicAuthenticator(username, password);
            return new APIClient(requestOption);
        }

        public APIClient SetRequestTokenAuthentication(string consumerKey, string consumerSecret)
        {
            requestOption.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
            return new APIClient(requestOption);
        }

        public APIClient SetAccessTokenAuthentication(string consumerKey, string consumerSecret, string oauthToken, string oauthTokenSecret)
        {
            requestOption.Authenticator = OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, oauthToken, oauthTokenSecret);
            return new APIClient(requestOption);
        }

        public APIClient SetRequestHeaderAuthentication(string token, string authType = "Bearer")
        {
            requestOption.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, authType);
            return new APIClient(requestOption);
        }

        public APIClient SetJwtAuthenticator(string token)
        {
            requestOption.Authenticator = new JwtAuthenticator(token);
            return new APIClient(requestOption);
        }

        public APIClient ClearAuthenticator()
        {
            requestOption.Authenticator = null;
            return new APIClient(requestOption);
        }

        public APIClient AddDefaultHeaders(Dictionary<string, string> headers)
        {
            _client.AddDefaultHeaders(headers);
            return this;
        }

        public APIClient AddHeader(string name, string value)
        {
            Request.AddHeader(name, value);
            return this;
        }

        public APIClient AddAuthorizationHeader(string value)
        {
            return AddHeader("Authorization", value);
        }

        public APIClient AddContentTypeHeader(string value)
        {
            return AddHeader("Content-Type", value);
        }

        public APIClient AddParameter(string name, string value)
        {
            Request.AddParameter(name, value);
            return this;
        }

        public APIClient AddBody(object obj, string contentType)
        {
            Request.AddBody(obj, contentType);
            return this;
        }

        public APIClient CreateRequest(string source = "")
        {
            Request = new RestRequest(source);
            return this;
        }

        public async Task<RestResponse> ExecuteGetAsync()
        {
            return await _client.ExecuteGetAsync(Request);
        }

        public async Task<RestResponse<T>> ExecuteGetAsync<T>()
        {
            return await _client.ExecuteGetAsync<T>(Request);
        }

        public async Task<RestResponse> ExecutePostAsync()
        {
            return await _client.ExecutePostAsync(Request);
        }

        public async Task<RestResponse<T>> ExecutePostAsync<T>()
        {
            return await _client.ExecutePostAsync<T>(Request);
        }

        public async Task<RestResponse> ExecutePutAsync()
        {
            return await _client.ExecutePutAsync(Request);
        }

        public async Task<RestResponse<T>> ExecutePutAsync<T>()
        {
            return await _client.ExecutePutAsync<T>(Request);
        }

        public async Task<RestResponse> ExecutePatchAsync()
        {
            return await _client.ExecutePatchAsync(Request);
        }

        public async Task<RestResponse<T>> ExecutePatchAsync<T>()
        {
            return await _client.ExecutePatchAsync<T>(Request);
        }

        public async Task<RestResponse> ExecuteDeleteAsync()
        {
            return await _client.ExecuteDeleteAsync(Request);
        }

        public async Task<RestResponse<T>> ExecuteDeleteAsync<T>()
        {
            return await _client.ExecuteDeleteAsync<T>(Request);
        }

        public RestResponse ExecuteGet()
        {
            return _client.ExecuteGet(Request);
        }

        public RestResponse ExecutePost()
        {
            return _client.ExecutePost(Request);
        }

        public RestResponse ExecutePut()
        {
            return _client.ExecutePut(Request);
        }

        public RestResponse ExecuteDelete()
        {
            return _client.ExecuteDelete(Request);
        }
    }
}
