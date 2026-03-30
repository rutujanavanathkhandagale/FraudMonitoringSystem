namespace FraudMonitoringSystem.DTOs.Rules
{
    public class DetectionRuleCreateDto
    {
        public int ScenarioId { get; set; }
        public string Expression { get; set; } = string.Empty;
        public decimal Threshold { get; set; }
        public string Version { get; set; }
        public string CustomerType { get; set; } = string.Empty;
        public string Status { get; set; }
    }
}
