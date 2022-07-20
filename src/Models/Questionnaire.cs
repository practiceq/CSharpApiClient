namespace IntakeQ.ApiClient.Models
{
    public class Questionnaire
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Archived { get; set; }
        public bool Anonymous { get; set; }
        public string PractitionerId { get; set; }
        public string ExternalPractitionerId { get; set; }
    }
}