using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using Microsoft.AspNetCore.Mvc;
using FraudMonitoringSystem.DTOs.AlertCase;

namespace FraudMonitoringSystem.Controllers.AlertCase
{
	[ApiController]
	[Route("api/[controller]")]
	public class CaseAttachmentsController : ControllerBase
	{
		private readonly WebContext _context;

		public CaseAttachmentsController(WebContext context)
		{
			_context = context;
		}

		// GET all attachments
		[HttpGet]
		public IActionResult GetAttachments()
		{
			return Ok(_context.CaseAttachments.ToList());
		}

		// POST new attachment
		[HttpPost]
		[HttpPost]
		public IActionResult AddAttachment([FromForm] CaseAttachmentDTO dto)
		{
			if (dto.File == null || dto.File.Length == 0)
				return BadRequest("File is required");

			// 📁 Folder path
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			// 📄 Unique file name
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.File.FileName);

			var filePath = Path.Combine(folderPath, fileName);

			// 💾 Save file physically
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				dto.File.CopyTo(stream);
			}

			// 💾 Save in DB
			var attachment = new CaseAttachment
			{
				CaseID = dto.CaseID,
				FileURI = fileName,   // store file name or full path
				UploadedBy = dto.UploadedBy,
				UploadedDate = DateTime.UtcNow
			};

			_context.CaseAttachments.Add(attachment);
			_context.SaveChanges();

			return Ok(new
			{
				attachment.AttachmentID,
				attachment.CaseID,
				FileURL = $"{Request.Scheme}://{Request.Host}/Uploads/{attachment.FileURI}",
				attachment.UploadedBy,
				attachment.UploadedDate
			});
		}



			// GET attachment by CaseID
			[HttpGet("case/{caseId}")]
		public IActionResult GetAttachmentsByCase(int caseId)
		{
			var attachments = _context.CaseAttachments
				.Where(a => a.CaseID == caseId)
				.ToList();

			return Ok(attachments);
		}
	}
}
