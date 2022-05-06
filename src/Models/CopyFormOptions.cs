namespace IntakeQ.ApiClient.Models
{
    public class CopyFormOptions
    {
        public string SourceMasterFormId { get; set; }
        public string DestinationPracticeId { get; set; }
        /// <summary>
        /// Use this if this questionnaire has attached consent forms and you want them to be copied as well 
        /// </summary>
        public bool CopyAttachedConsentForms { get; set; }
    }
}