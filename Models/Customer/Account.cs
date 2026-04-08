using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FraudMonitoringSystem.Models.Customer
{
    public class Account
    {
        [Key]
        [StringLength(20, ErrorMessage = "AccountId cannot exceed 20 characters")]
        public string? AccountId { get; set; }   // optional, generated server-side

        [Required(ErrorMessage = "CustomerId is required")]
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }   // numeric IDs

        [JsonIgnore]
        public PersonalDetails? Customer { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        [StringLength(20, ErrorMessage = "Account Number cannot exceed 20 characters")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Product Type is required")]
        [RegularExpression("Savings|Current|Card|Loan|Wallet",
            ErrorMessage = "Product Type must be Savings, Current, Card, Loan, or Wallet")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        [RegularExpression("USD|EUR|INR|GBP|JPY|AUD|CAD|CHF|CNY|NZD",
            ErrorMessage = "Currency must be valid")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("Active|Frozen|Closed",
            ErrorMessage = "Status must be Active, Frozen, or Closed")]
        public string Status { get; set; } = "Active";
    }
}
