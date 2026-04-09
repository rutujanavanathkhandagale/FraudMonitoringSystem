using System.ComponentModel.DataAnnotations;

namespace FraudMonitoringSystem.Models.WatchList
{
    public class WatchlistEntry
    {
        [Key]
        public long EntryId { get; set; }

        [Required]
        [RegularExpression("Sanctions|PEP|InternalBlackList")]
        public string ListType { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Identifier { get; set; } // Account No

        [Required]
        [RegularExpression("Active|Archived")]
        public string Status { get; set; } = "Active";
    }
}
