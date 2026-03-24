using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FraudMonitoringSystem.Models.Customer

{

    public class KYCProfile

    {

        [Key]

        public long KYCId { get; set; }

        [Required]

        public long CustomerId { get; set; }

        [Required(ErrorMessage = "Documents are required")]

        public string DocumentRefsJSON { get; set; }

      

        [Required]

        [StringLength(20)]

        [RegularExpression("Pending|Verified",

            ErrorMessage = "Status must be Pending or Verified")]

        public string Status { get; set; } = "Pending";

        [ForeignKey("CustomerId")]

        [ValidateNever]
        [JsonIgnore]
        public PersonalDetails? Customer { get; set; }

    }

}
