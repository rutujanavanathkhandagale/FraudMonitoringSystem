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

       

        public async Task<IActionResult> Update(

            long id,

            [FromForm] List<IFormFile> documents,

            [FromForm] List<string> requiredDocs)

        {

            var profile = await _service.UpdateAsync(id, documents, requiredDocs);

            if (profile == null)

                return NotFound(new { Message = "KYC profile not found" });

            return Ok(new

            {

                Message = "KYC updated successfully",

                Status = profile.Status,

                Profile = profile

            });

        }

    }

}
