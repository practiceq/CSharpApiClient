using System;
using System.Collections.Generic;
using IntakeQ.ApiClient.Helpers;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Models
{
    public class Invoice
    {
        

        public string Id { get; set; }
        public string Status { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public double DueDate { get; set; }
        public double IssuedDate { get; set; }
        public double DateCreated { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public List<string> AdditionalEmailRecipients { get; set; }             
        public List<InvoicePayment> Payments { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public int Number { get; set; }
        public string NoteToClient { get; set; }
        public string Currency { get; set; }
        public bool AllowTipping { get; set; }
        public bool AllowPartialPayments { get; set; }
      
        public string MemberId { get; set; }

        public bool Automated { get; set; }
        public string CreatedBy { get; set; }
      
        public string CurrencyIso { get; set; }
       
        public List<string> DiagnosisList { get; set; }       

        //Payment fields
        public decimal TipAmount { get; set; }
       
        public List<InvoiceItem> Items { get; set; }
        public string ClientPaymentPlanId { get; set; }
        public int? ClientPaymentPlanInterval { get; set; }
        public int ClientIdNumber { get; set; }
        
        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }
    }
    
     public class InvoicePayment
    {       
        public double Date { get; set; }

        /// <summary>
        /// This amount includes any tips because it needs to reflect the payment provider's amount
        /// </summary>
        public decimal Amount { get; set; }
        public string Currency { get; set; }
       
        public string Method { get; set; }
        public string AdditionalInfo { get; set; }        
        public decimal RefundedAmount { get; set; }

        /// <summary>
        /// The tip amount is included in the payment amount
        /// </summary>
        public decimal TipAmount { get; set; }
        public string ProcessedBy { get; set; }
        public string ProcessedByType { get; set; }
        public CardDetails CardDetails { get; set; }
    }

    public class InvoiceItem
    {
        public InvoiceItem()
        {
            SubItems = new List<InvoiceSubItem>();
            Taxes = new List<InvoiceTax>();
        }

        public string Description { get; set; }
        public decimal Units { get; set; }
        public decimal Price { get; set; }
        public double? Date { get; set; }
        public bool TaxesIncludedInPrice { get; set; }
        public bool IsCopay { get; set; }
        public bool RemovedFromSuperbill { get; set; }
        public string ServiceCode { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceSubItem> SubItems { get; set; }
        public List<InvoiceTax> Taxes { get; set; }
    }

    public class InvoiceTax
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }

    }

    public class InvoiceSubItem
    {
        public string ServiceCode { get; set; }
        public decimal Price { get; set; }
        public decimal Units { get; set; }
        public string Description { get; set; }
        
        public double? Date { get; set; }

        public List<string> Modifiers { get; set; }
    }
    
    public class CardDetails
    {
        public string Brand { get; set; }
        public string LastDigits { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string CardId { get; set; }
    }
}