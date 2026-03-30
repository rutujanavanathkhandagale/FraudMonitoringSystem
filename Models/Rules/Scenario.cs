using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.Rules
{
    public class Scenario
    {
        [Key]
        public int ScenarioId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        [Required, StringLength(50)]
        public string RiskDomain { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Status { get; set; }  // Active/Inactive
       
    }
}
