namespace IntakeQ.ApiClient.Models
{
    public class ResendForm
    {
        public string IntakeId { get; set; }
        /// <summary>
        /// Leave null for using the original delivery method. Set to "email" or "sms" to use a specific method. Make sure the Client record has an email or phone number.  
        /// </summary>
        public string DeliveryMethod { get; set; }
    }
    
}