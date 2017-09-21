using System;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class IntakeSummary
    {
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime DateCreated { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime? DateSubmitted { get; set; }
        public string Status { get; set; }
        public string Practitioner { get; set; }
        public string QuestionnaireName { get; set; }
        public string Id { get; set; }
        public int ClientId { get; set; }
    }
}