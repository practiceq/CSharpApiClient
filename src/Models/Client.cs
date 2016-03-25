using System;
using System.Collections.Generic;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class Client
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ClientNumber { get; set; }
    }
}