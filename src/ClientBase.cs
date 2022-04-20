using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IntakeQ.ApiClient.Helpers;
using IntakeQ.ApiClient.Models;
using Newtonsoft.Json;


namespace IntakeQ.ApiClient
{
    public abstract class ClientBase : IDisposable
    {
        private readonly string _apiKey;
        protected readonly HttpClient _httpClient;

        public ClientBase(string apiKey, string baseUrl)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl),
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Key", _apiKey);
#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
#endif
        }

        protected StringContent GetJsonContent(object jsonObject) => new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage response) => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        protected async Task PostOrThrow(string path, object jsonPayload = null)
        {
            var response = await _httpClient.PostAsync(path, jsonPayload == null ? null : GetJsonContent(jsonPayload));
            await EnsureSuccess(response);
        }

        protected async Task<T> PostOrThrow<T>(string path, object jsonPayload = null)
        {
            var response = await _httpClient.PostAsync(path, jsonPayload == null ? null : GetJsonContent(jsonPayload));
            await EnsureSuccess(response);
            return await DeserializeResponse<T>(response);
        }

        protected async Task<T> PutOrThrow<T>(string path, object jsonPayload)
        {
            var response = await _httpClient.PutAsync(path, GetJsonContent(jsonPayload));
            await EnsureSuccess(response);
            return await DeserializeResponse<T>(response);
        }

        protected async Task DeleteOrThrow(string path)
        {
            var response = await _httpClient.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
        }

        protected async Task<T> GetOrThrow<T>(string path, (string Key, string Value)[] parameters = null)
        {
            var parameterCollection = new NameValueCollection();
            parameters?
                .Where(parameter => !string.IsNullOrEmpty(parameter.Value))
                .ToList()
                .ForEach(parameter => parameterCollection.Add(parameter.Key, parameter.Value));
            var response = await _httpClient.GetAsync($"{path}{parameterCollection.ToQueryString()}");
            await EnsureSuccess(response);
            return await DeserializeResponse<T>(response);
        }

        protected async Task<byte[]> GetBytesOrThrow(string path)
        {
            var response = await _httpClient.GetAsync(path);
            await EnsureSuccess(response);
            return await response.Content.ReadAsByteArrayAsync();
        }

        protected async Task EnsureSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                throw new ApiException(response.StatusCode, responseBody);
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
