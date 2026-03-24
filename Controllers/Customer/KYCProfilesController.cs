using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Services.Customer.Implementations;
using FraudMonitoringSystem.Services.Customer.Interfaces;

using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<KYCProfile>> GetByCustomerId(long customerId)
        {
            var kyc = await _service.GetByCustomerIdAsync(customerId);
            if (kyc == null)
                return NotFound();

            return Ok(kyc);
        }

        [HttpPut("verify/customer/{customerId}")]
        public async Task<IActionResult> VerifyByCustomerId(long customerId)
        {
            var result = await _service.VerifyAsync(customerId);
            if (result == null)
                return NotFound($"No KYC profile found for CustomerId {customerId}");

            return Ok(result);
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

        [HttpPut("{id}")]



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




