using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudMonitoringSystem.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class KYCProfilesController : ControllerBase
    {
        private readonly IKYCService _service;

        public KYCProfilesController(IKYCService service)
        {
            _service = service;
        }

        // ✅ Get KYC profile by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var profile = await _service.GetByIdAsync(id);
            if (profile == null)
                return NotFound(new { Message = $"KYC profile {id} not found" });

            return Ok(profile);
        }

        // ✅ Create new KYC profile with multiple documents
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] long customerId,
            [FromForm] List<IFormFile> documents,
            [FromForm] List<string> requiredDocs)
        {
            var profile = await _service.CreateAsync(customerId, documents, requiredDocs);

            // Simulate notification to compliance office
            Console.WriteLine($"[NOTIFY] Compliance office: New KYC created for Customer {customerId}");

            return Ok(new { Message = "KYC created successfully", Profile = profile });
        }

        // ✅ Update existing KYC profile (re-upload documents)
      

        // ✅ Patch partial updates (e.g., compliance officer changes status)
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> Patch(long id, [FromBody] KYCProfile partialProfile)
        //{
        //    var updated = await _service.PatchAsync(id, partialProfile);
        //    if (updated == null)
        //        return NotFound(new { Message = $"KYC profile {id} not found" });

        //    Console.WriteLine($"[NOTIFY] Compliance office: KYC {id} status changed to {updated.Status}");

        //    return Ok(new { Message = "KYC patched successfully", Profile = updated });
        //}

        // ✅ Search KYC profiles by query (status or customer type)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var results = await _service.SearchAsync(query);
            return Ok(results);
        }
    }
}

