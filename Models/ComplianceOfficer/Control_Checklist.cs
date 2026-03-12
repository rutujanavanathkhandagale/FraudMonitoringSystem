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
        public int CaseID { get; set; }
        [Required]
        public string CheckedBy { get; set; } = string.Empty;
        public DateTime CheckedDate { get; set; }
        public string Result { get; set; } = "Fail";
    }
}