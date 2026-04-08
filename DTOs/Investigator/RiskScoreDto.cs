namespace FraudMonitoringSystem.DTOs.Investigator
{
    public class RiskScoreDto
    {
        public int TransactionID { get; set; }
        public decimal ScoreValue { get; set; }
        public string ReasonsJSON { get; set; }
    }
}
