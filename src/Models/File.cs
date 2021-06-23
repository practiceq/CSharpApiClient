using System;
using System.Collections.Generic;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class File
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int Size { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FolderId { get; set; }

    }
}