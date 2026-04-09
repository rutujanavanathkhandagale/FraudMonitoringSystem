using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.WatchList
{
    public class EntityLink
    {
        [Key]
        public long LinkId { get; set; }

        [Required]
        public long FromCustomerId { get; set; }

        [Required]
        public long ToAccountId { get; set; }

        [Required]
        [RegularExpression("Owner|Authorized|Associated",
        ErrorMessage = "LinkType must be Owner, Authorized, or Associated")]
        public string LinkType { get; set; }
    }
}