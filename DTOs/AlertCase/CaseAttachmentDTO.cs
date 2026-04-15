namespace FraudMonitoringSystem.DTOs.AlertCase
{
	public class CaseAttachmentDTO
	{
		public int CaseID{ get; set; }

		public string? FileURI { get; set; }
		public IFormFile File{ get; set; }

		//public int UploadedByName { get; set; }
	}
}
