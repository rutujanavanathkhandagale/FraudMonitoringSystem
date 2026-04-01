namespace FraudMonitoringSystem.DTOs.AlertCase
{
	public class InvestigationNoteDTO
	{
		public int CaseID { get; set; }

		public int AuthorID { get; set; }

		public string NoteText { get; set; }
	}
}
