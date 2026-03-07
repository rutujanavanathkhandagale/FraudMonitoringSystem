using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.Rules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FraudMonitoringSystem.Models.Investigator;



public class Alert
{
    [Key]
    public int AlertID { get; set; }
    
    
    public int RuleID { get; set; }
    public int? CaseID { get; set; }
    public string Severity { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [ForeignKey("RuleID")]
    public DetectionRule DetectionRule { get; set; }
    [ForeignKey("CaseID")]
    public Case Case { get; set; }



    [ForeignKey("TransactionID")]
    public int TransactionID { get; set; }
    public Transaction Transaction { get; set; }
}