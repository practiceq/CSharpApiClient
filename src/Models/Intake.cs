using System;
using System.Collections.Generic;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class Intake: IntakeSummary
    {
        public List<Question> Questions { get; set; }


    }
}