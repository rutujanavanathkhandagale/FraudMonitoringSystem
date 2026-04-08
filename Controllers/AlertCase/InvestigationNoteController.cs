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

        // ✅ GET all notes		
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_context.InvestigationNotes.ToList());
        }

        // ✅ POST new note (FIXED)		
        [HttpPost]
        public IActionResult AddNote([FromBody] InvestigationNoteDTO dto)
        {
            try
            {
                // ✅ Validate CaseID
                var caseExists = _context.Cases.Any(c => c.CaseID == dto.CaseID);
                if (!caseExists)
                    return BadRequest("Invalid CaseID");

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
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add note: {ex.Message}");
            }
        }

        // ✅ GET notes by case		
        [HttpGet("case/{caseId}")]
        public IActionResult GetNotesByCase(int caseId)
        {
            var notes = _context.InvestigationNotes
                .Where(n => n.CaseID == caseId)
                .ToList();

            return Ok(notes);
        }
        // ❌ DELETE NOTE (FINAL FIX)		
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _context.InvestigationNotes.Find(id);

            if (note == null)
            {
                return NotFound($"Note with ID {id} not found");
            }

            _context.InvestigationNotes.Remove(note);
            _context.SaveChanges();

            return Ok($"Note {id} deleted successfully");
        }

    }
}
