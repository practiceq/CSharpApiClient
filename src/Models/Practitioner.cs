using System;

namespace IntakeQ.ApiClient.Models
{
    public class Practitioner
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ExternalPractitionerId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}