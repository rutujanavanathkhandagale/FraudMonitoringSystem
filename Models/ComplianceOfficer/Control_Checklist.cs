using FraudMonitoringSystem.Models.AlertCase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace FraudMonitoringSystem.Models
{
    public class ControlChecklist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChecklistID { get; set; }
        [Required]
        [ForeignKey("CaseID")]
        public int CaseID { get; set; }
        
        [JsonIgnore]
        public virtual Case? Case { get; set; }

        [Required]
        public string CheckedBy { get; set; } = string.Empty;
        [Required]
        public DateTime CheckedDate { get; set; }
        public string Status { get; set; } = "Fail"; // Pass / Fail 
        [Required]
        public string Result { get; set; } = "Fail";
    }
}