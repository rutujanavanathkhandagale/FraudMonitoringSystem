using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FraudMonitoringSystem.Models.Customer;

namespace FraudMonitoringSystem.Models.Investigator
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required(ErrorMessage = "AccountID is required.")]
        public string AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account? Account { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public long CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual PersonalDetails? Customer { get; set; }

        [Required(ErrorMessage = "Customer type is required")]
        [RegularExpression("Business|Student|Retail", ErrorMessage = "Customer type must be Business, Student, or Retail")]
        public string CustomerType { get; set; } = string.Empty;

        [Required(ErrorMessage = "CounterpartyAccount is required.")]
        [StringLength(50, ErrorMessage = "CounterpartyAccount cannot exceed 50 characters.")]
        public string CounterpartyAccount { get; set; } = string.Empty;

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(3, ErrorMessage = "Currency must be a valid 3-letter ISO code.")]
        public string Currency { get; set; } = string.Empty;

        [Required(ErrorMessage = "TransactionType is required.")]
        [RegularExpression("^(Credit|Debit)$", ErrorMessage = "TransactionType must be either Credit or Debit.")]
        public string TransactionType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Channel is required.")]
        [RegularExpression("^(Branch|ATM|Online|Card|Wire|Wallet)$", ErrorMessage = "Invalid channel type.")]
        public string Channel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Timestamp is required.")]
        public DateTime Timestamp { get; set; }

        [StringLength(100, ErrorMessage = "GeoLocation cannot exceed 100 characters.")]
        public string GeoLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Posted|Reversed)$", ErrorMessage = "Status must be Posted or Reversed.")]
        public string Status { get; set; } = string.Empty;

        // Added to fix the Repository errors
        [StringLength(50)]
        public string? SourceType { get; set; }
    }
}