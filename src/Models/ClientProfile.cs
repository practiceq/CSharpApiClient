using System;
using System.Collections.Generic;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class ClientProfile 
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime? DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }

        public string[] Tags { get; set; }
        public bool Archived { get; set; }

        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string UnitNumber { get; set; }

        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string StateShort { get; set; }

        public string AdditionalInformation { get; set; }


        public string PrimaryInsuranceCompany { get; set; }
        public string PrimaryInsurancePolicyNumber { get; set; }
        public string PrimaryInsuranceGroupNumber { get; set; }
        public string PrimaryInsuranceHolderName { get; set; }
        public string PrimaryInsuranceRelationship { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime? PrimaryInsuranceHolderDateOfBirth { get; set; }

        public string SecondaryInsuranceCompany { get; set; }
        public string SecondaryInsurancePolicyNumber { get; set; }
        public string SecondaryInsuranceGroupNumber { get; set; }
        public string SecondaryInsuranceHolderName { get; set; }
        public string SecondaryInsuranceRelationship { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime? SecondaryInsuranceHolderDateOfBirth { get; set; }

        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime? DateCreated { get; set; }
        [JsonConverter(typeof(FromJavascripDateConverter))]
        public DateTime? LastActivityDate { get; set; }

        public string PractitionerId { get; set; }
        public List<ClientProfileCustomField> CustomFields { get; set; }

        public ClientProfile()
        {
            CustomFields = new List<ClientProfileCustomField>();
        }
    }

    public class ClientProfileCustomField
    {
        public string FieldId { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}