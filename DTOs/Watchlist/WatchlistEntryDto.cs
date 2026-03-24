namespace FraudMonitoringSystem.DTOs.Watchlist
{
    public class WatchlistEntryDto
    {
        public long EntryId { get; set; }
        public string ListType { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Status { get; set; }
    }
}
