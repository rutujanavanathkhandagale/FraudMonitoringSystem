using FraudMonitoringSystem.Repositories.Customer.Implementations;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly PersonalDetailsRepository _customerRepo;
    private readonly AccountRepository _accountRepo;
    private readonly KYCRepository _kycRepo;

    public DashboardController(PersonalDetailsRepository customerRepo, AccountRepository accountRepo, KYCRepository kycRepo)
    {
        _customerRepo = customerRepo;
        _accountRepo = accountRepo;
        _kycRepo = kycRepo;
    }

    [HttpGet("customers/count")]
    public async Task<IActionResult> GetCustomerCount()
    {
        var count = await _customerRepo.GetCustomerCountAsync();
        return Ok(count);
    }

    [HttpGet("accounts/count")]
    public async Task<IActionResult> GetAccountCount()
    {
        var count = await _accountRepo.GetAccountCountAsync();
        return Ok(count);
    }

    [HttpGet("kyc/pending/count")]
    public async Task<IActionResult> GetPendingKycCount()
    {
        var count = await _kycRepo.GetPendingKycCountAsync();
        return Ok(count);
    }

    [HttpGet("kyc/verified/count")]
    public async Task<IActionResult> GetVerifiedKycCount()
    {
        var count = await _kycRepo.GetVerifiedKycCountAsync();
        return Ok(count);
    }
}
