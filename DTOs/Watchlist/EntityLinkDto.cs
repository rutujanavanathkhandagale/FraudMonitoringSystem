namespace FraudMonitoringSystem.DTOs.Watchlist
{
    public class EntityLinkDto
    {
        public long CustomerId { get; set; }
        public long AccountId { get; set; }
        public string LinkType { get; set; }
    }
}
