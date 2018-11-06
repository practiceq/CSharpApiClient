namespace IntakeQ.ApiClient.Models
{
    public class SendForm
    {
        public string QuestionnaireId { get; set; }
        public int ClientId { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
        public string PractitionerId { get; set; }
    }
}