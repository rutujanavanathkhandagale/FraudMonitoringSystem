using FraudMonitoringSystem.Models.Customer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FraudMonitoringSystem.Models.Investigator
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required(ErrorMessage = "CounterpartyAccount is required.")]
        [StringLength(50)]
        public string CounterpartyAccount { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [JsonIgnore]   // 👈 prevents Swagger from requiring Customer JSON
        public PersonalDetails? Customer { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [Required]
        [RegularExpression("^(Credit|Debit)$")]
        public string TransactionType { get; set; }

        [Required]
        [RegularExpression("^(Branch|ATM|Online|Card|Wire|Wallet)$")]
        public string Channel { get; set; }

        [Required]
        public string SourceType { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [StringLength(100)]
        public string GeoLocation { get; set; }

        [Required]
        [RegularExpression("^(Posted|Reversed)$")]
        public string Status { get; set; }

        // Foreign key to Account
        [Required]
        public long AccountId { get; set; }

        [JsonIgnore]   // 👈 prevents Swagger from requiring Account JSON
        public Account? Account { get; set; }  // make nullable, remove [Required]
    }
}
