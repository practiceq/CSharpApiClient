using System;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class TreatmentNoteSummary
    {
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string PractitionerEmail { get; set; }
        public string PractitionerName { get; set; }
        public string Id { get; set; }
        public int ClientId { get; set; }
        public string PractitionerId { get; set; }
        public string NoteName { get; set; }
    }
}
