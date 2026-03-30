namespace FraudMonitoringSystem.DTOs.Rules
{
    public class DetectionRuleDto
    {
        public int RuleId { get; set; }
        public int ScenarioId { get; set; }

        public string Expression { get; set; } = string.Empty;
        public decimal Threshold { get; set; }
        public string Version { get; set; } = string.Empty;
        public string CustomerType { get; set; }
        public string Status { get; set; } = string.Empty;


        public ScenarioDto Scenario { get; set; } = new ScenarioDto();
    }
}
