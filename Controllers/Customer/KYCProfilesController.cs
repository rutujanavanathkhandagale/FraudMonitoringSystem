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
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(long id)

        {
            var profile = await _service.GetByIdAsync(id);

            if (profile == null)

                return NotFound(new { Message = "KYC profile not found" });

            return Ok(profile);

        }
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult> GetByCustomerId(long customerId)
        {
            var kyc = await _service.GetByCustomerIdAsync(customerId);
            if (kyc == null)
                return NotFound();

            // We manually create a response object to ensure 'fullName' exists
            return Ok(new
            {
                kyc.KYCId,
                kyc.CustomerId,
                kyc.Status,
                kyc.DocumentRefsJSON,
                // Pull the name from the included Customer/PersonalDetails object
                FullName = kyc.Customer?.FirstName ?? "User Name Not Found",
                Customer = kyc.Customer // Keep the full object just in case
            });
        }
        [HttpPut("verify/customer/{customerId}")]

        public async Task<IActionResult> VerifyByCustomerId(long customerId)

        {

            var result = await _service.VerifyByCustomerIdAsync(customerId);

            // ✅ FIX: check result first

            if (result == null)

            {

                return NotFound(new

                {

                    Message = $"No KYC profile found for CustomerId {customerId}"

                });

            }

            return Ok(new

            {

                Message = "KYC verified successfully",

                Status = result.Status,

                Profile = result

            });

        }


        [HttpPost]

        public async Task<IActionResult> Create(

            [FromForm] long customerId,

            [FromForm] List<IFormFile> documents,

            [FromForm] List<string> requiredDocs)

        {
            var profile = await _service.CreateAsync(customerId, documents, requiredDocs);

            return Ok(new
            {

                Message = "KYC created successfully",

                Status = profile.Status,

                Profile = profile

            });

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllProfiles()
        {
            // Call the service method to get data
            var profiles = await _service.GetAllAsync();

            // Return a clean object for your dashboard
            return Ok(profiles.Select(p => new {
                p.KYCId,
                p.CustomerId,
                p.Status,
                // Pulling from the nested Customer object
                FullName = $"{p.Customer?.FirstName} {p.Customer?.LastName}",
                Email = p.Customer?.Email,
                Phone = p.Customer?.Phone
            }));
        }


        [HttpPut("{id}/verify")]

        public async Task<IActionResult> Verify(long id)
        {
            var profile = await _service.VerifyAsync(id);
            if (profile == null)
                return NotFound(new { Message = "KYC profile not found" });

            return Ok(new
            {
                Message = "KYC verified successfully",
                Status = profile.Status,
                Profile = profile
            });
        }

    }

}



