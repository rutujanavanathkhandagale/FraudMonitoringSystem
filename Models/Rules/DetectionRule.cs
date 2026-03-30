using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraudMonitoringSystem.Models.Rules
{
    public class DetectionRule
    {
        [Key]
        public int RuleId { get; set; }


        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;


        [Required, StringLength(200)]
        public string Expression { get; set; } = string.Empty;


        [Range(1, int.MaxValue)]
        public decimal Threshold { get; set; }

        [Required]
        public string CustomerType { get; set; } = string.Empty;


        [Required, StringLength(20)]
        public string Version { get; set; } = string.Empty;


        [Required, StringLength(20)]
        public string Status { get; set; } = string.Empty;


        [ForeignKey("Scenario")]
        public int ScenarioId { get; set; }
        public Scenario Scenario { get; set; } = null!;
        public bool IsActive { get; internal set; }
    }
}
