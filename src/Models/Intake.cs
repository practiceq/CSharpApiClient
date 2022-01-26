using System;
using System.Collections.Generic;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class Intake: IntakeSummary
    {
        public List<Question> Questions { get; set; }
        public List<ConsentForm> ConsentForms { get; set; }
        public string Url { get; set; }
        public string Password { get; set; }

        public class ConsentForm
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string DocumentType { get; set; }
            public bool Signed { get; set; }
            public double? DateSubmitted { get; set; }
        }


    }
}