using System;
using System.Collections.Generic;

namespace IntakeQ.ApiClient.Models
{
    public class Assistant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ExternalAssistantId { get; set; }
        public DateTime DateCreated { get; set; }
        public string RoleName { get; set; }
        public List<string> PractitionerIds { get; set; }
    }
}