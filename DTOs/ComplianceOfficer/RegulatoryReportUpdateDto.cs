namespace FraudMonitoringSystem.DTOs.ComplianceOfficer
{
    public class RegulatoryReportUpdateDto
    {
        public string ReportType { get; set; }
        public string Period { get; set; }
        public DateOnly SubmittedDate { get; set; }
        public string Status { get; set; }


    }
}
