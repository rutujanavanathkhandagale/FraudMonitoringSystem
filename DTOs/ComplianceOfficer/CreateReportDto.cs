namespace FraudMonitoringSystem.DTOs.ComplianceOfficer
{
    public class CreateReportDto

    {

        public int CaseID { get; set; }

        public string ReportType { get; set; }

        public string Period { get; set; }

        public DateTime SubmittedDate { get; set; }

        public string Status { get; set; }

    }

}
