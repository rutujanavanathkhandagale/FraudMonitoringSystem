using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.DTOs.AlertCase;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

		// ✅ GET ALL ATTACHMENTS
		[HttpGet]
		public IActionResult GetAll()
		{
			var data = from a in _context.CaseAttachments

					   join u in _context.SystemUsers
					   on a.UploadedBy equals u.Id into userGroup
					   from u in userGroup.DefaultIfEmpty()

					   join r in _context.Roles
					   on u.RoleId equals r.RoleId into roleGroup
					   from r in roleGroup.DefaultIfEmpty()

					   select new
					   {
						   AttachmentID = a.AttachmentID,
						   CaseID = a.CaseID,
						   FileURI = a.FileURI,
						   UploadedBy = a.UploadedBy,

						   // 🔥 IMPORTANT LINE
						   UploadedByRole = r != null ? r.RoleName : "Unknown",

						   UploadedDate = a.UploadedDate
					   };

			return Ok(data.ToList());

		}


			[HttpDelete("{id}")]
			public IActionResult DeleteAttachment(int id)
			{
				var attachment = _context.CaseAttachments
					.FirstOrDefault(a => a.AttachmentID == id);

				if (attachment == null)
				{
					return NotFound("Attachment not found");
				}

				// 🔥 Delete file from folder
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", attachment.FileURI);

				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}

				// 🔥 Delete from DB
				_context.CaseAttachments.Remove(attachment);
				_context.SaveChanges();

				return Ok("Deleted successfully");
			}


			// ✅ UPLOAD FILE
			[HttpPost]
			public IActionResult AddAttachment([FromForm] CaseAttachmentDTO dto)
			{
				try
				{
					if (dto.File == null || dto.File.Length == 0)
						return BadRequest("File is required");

					var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

					if (!Directory.Exists(uploadsFolder))
						Directory.CreateDirectory(uploadsFolder);

					var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
					var filePath = Path.Combine(uploadsFolder, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						dto.File.CopyTo(stream);
					}

					var attachment = new CaseAttachment
					{
						//CaseID = dto.CaseID,
						FileURI = fileName,
						UploadedBy = 1,
						UploadedDate = DateTime.UtcNow
					};

					_context.CaseAttachments.Add(attachment);
					_context.SaveChanges();

					return Ok(new
					{
						attachment.AttachmentID,
						attachment.CaseID,
						FileURL = $"{Request.Scheme}://{Request.Host}/Uploads/{fileName}",
						attachment.UploadedBy,
						attachment.UploadedDate
					});
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}

			}
		}
	}



