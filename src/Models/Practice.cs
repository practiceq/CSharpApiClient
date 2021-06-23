using System;

namespace IntakeQ.ApiClient.Models
{
    public class Practice
    {
        public string Id { get; set; }
        public string PracticeName { get; set; }
        /// <summary>
        /// The first name of the main practitioner in this account.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the main practitioner in this account.
        /// </summary>
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ExternalPracticeId { get; set; }
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Optional. By default, an IntakeQ practice account is also a practitioner account. You can use this field to pass an external ID for the main practitioner associated with this practice
        /// </summary>
        public string ExternalPractitionerId { get; set; }
    }
}