using RestSharp;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutomationFramework.Core
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<IRestResponse> GetAsync(string resource, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            var request = new RestRequest(resource, Method.GET);
            AddHeaders(request, headers);
            AddQueryParameters(request, queryParams);
            return await ExecuteAsync(request);
        }

        public async Task<IRestResponse> PostAsync(string resource, object body, Dictionary<string, string> headers = null)
        {
            var request = new RestRequest(resource, Method.POST);
            AddHeaders(request, headers);
            request.AddJsonBody(JsonConvert.SerializeObject(body));
            return await ExecuteAsync(request);
        }

        public async Task<IRestResponse> PutAsync(string resource, object body, Dictionary<string, string> headers = null)
        {
            var request = new RestRequest(resource, Method.PUT);
            AddHeaders(request, headers);
            request.AddJsonBody(JsonConvert.SerializeObject(body));
            return await ExecuteAsync(request);
        }

        public async Task<IRestResponse> DeleteAsync(string resource, Dictionary<string, string> headers = null)
        {
            var request = new RestRequest(resource, Method.DELETE);
            AddHeaders(request, headers);
            return await ExecuteAsync(request);
        }

        private void AddHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
        }

        private void AddQueryParameters(RestRequest request, Dictionary<string, string> queryParams)
        {
            if (queryParams != null)
            {
                foreach (var param in queryParams)
                {
                    request.AddQueryParameter(param.Key, param.Value);
                }
            }
        }

        private async Task<IRestResponse> ExecuteAsync(RestRequest request)
        {
            try
            {
                var response = await _client.ExecuteAsync(request);
                if (!response.IsSuccessful)
                {
                    throw new ApplicationException($"Error: {response.StatusCode}, Content: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Exception: {ex.Message}", ex);
            }
        }
    }
}