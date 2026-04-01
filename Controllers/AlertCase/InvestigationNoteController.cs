using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.DTOs.AlertCase;

namespace FraudMonitoringSystem.Controllers.AlertCase
{
	[ApiController]
	[Route("api/[controller]")]
	public class InvestigationNotesController : ControllerBase
	{
		private readonly WebContext _context;

		public InvestigationNotesController(WebContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetNotes()
		{
			return Ok(_context.InvestigationNotes.ToList());
		}
[HttpPost]
public IActionResult AddNote(InvestigationNoteDTO dto)
{
    // ✅ CHECK IF CASE EXISTS
    var caseExists = _context.Cases.Any(c => c.CaseID == dto.CaseID);
 
    if (!caseExists)
        return BadRequest("Invalid CaseID");
 
    // ✅ CREATE NOTE
    var note = new InvestigationNote
    {
        CaseID = dto.CaseID,
        AuthorID = dto.AuthorID,
        NoteText = dto.NoteText,
        CreatedDate = DateTime.UtcNow
    };
 
    _context.InvestigationNotes.Add(note);
    _context.SaveChanges();
 
    return Ok(note);
}
		[HttpGet("case/{caseId}")]
		public IActionResult GetNotesByCase(int caseId)
		{
			var notes = _context.InvestigationNotes
				.Where(n => n.CaseID == caseId)
				.ToList();

			return Ok(notes);
		}

	}
}
