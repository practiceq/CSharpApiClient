using System;
using System.Net;
using IntakeQ.ApiClient.Models;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient
{
    public class ApiException : Exception
    {
        public ApiError ApiError { get; }

        public override string Message => ApiError?.Message ?? ResponseBody;

        public string ResponseBody { get; }

        public HttpStatusCode StatusCode { get; }

        public ApiException(
            HttpStatusCode statusCode,
            string responseBody)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
            ApiError = JsonConvert.DeserializeObject<ApiError>(responseBody);
        }
    }
}