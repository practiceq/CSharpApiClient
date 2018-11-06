using System;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public int ClientId { get; set; }
        public string Status { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime StartDate { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public double Price { get; set; }
        public string PractitionerName { get; set; }
        public string PractitionerEmail { get; set; }
        public string IntakeId { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public bool BookedByClient { get; set; }
    }
}