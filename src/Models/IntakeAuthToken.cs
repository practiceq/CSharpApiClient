using System;

namespace IntakeQ.ApiClient.Models
{
    public class IntakeAuthToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Url { get; set; }
    }
}