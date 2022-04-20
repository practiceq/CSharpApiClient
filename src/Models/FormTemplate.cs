namespace IntakeQ.ApiClient.Models
{
    /// <summary>
    /// Generic representation of a form template (Questionnaire, Treatment Note, Consent Form)
    /// </summary>
    public class FormTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Archived { get; set; }
        /// <summary>
        /// Possible values: Questionnaire, TreatmentNote, ConsentForm
        /// </summary>
        public string Type { get; set; }
    }
}