using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FraudMonitoringSystem.Models.AlertCase; 

namespace FraudMonitoringSystem.Models
{
    [Table("Control_Checklist")]
    public class ControlChecklist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "CaseID is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "CaseID must be a positive integer.")]
        public int CaseID { get; set; }

        // Navigation property for the Foreign Key relationship to the Case table
        [ForeignKey("CaseID")]
        public virtual Case? Case { get; set; }

        [Required(ErrorMessage = "The 'CheckedBy' field is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "CheckedBy cannot contain numbers.")]
        public string CheckedBy { get; set; } = string.Empty;

        [Required]
        [RegularExpression("PASS|FAIL|PENDING", ErrorMessage = "Result must be PASS, FAIL, or PENDING.")]
        public string OverallResult { get; set; } = "PENDING";

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // One-to-Many relationship: One Checklist has 3 Details (KYC, Transaction, Watchlist)
        [Required]
        public virtual List<ControlDetail> Details { get; set; } = new List<ControlDetail>();
    }

    [Table("Control_Details")]
    public class ControlDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailId { get; set; }

        [Required]
        [StringLength(100)]
        public string ControlName { get; set; } = string.Empty; // "KYC Verification"

        [Required]
        [RegularExpression("PASS|FAIL|PENDING", ErrorMessage = "Status must be PASS, FAIL, or PENDING.")]
        public string Status { get; set; } = "PENDING";

        // Foreign Key linking back to the Parent Checklist
        [Required]
        public int ControlChecklistId { get; set; }

        [ForeignKey("ControlChecklistId")]
        public virtual ControlChecklist? ControlChecklist { get; set; }
    }
}