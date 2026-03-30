namespace FraudMonitoringSystem.DTOs.Rules
{
    public class ScenarioDto
    {
        public int ScenarioId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string RiskDomain { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
