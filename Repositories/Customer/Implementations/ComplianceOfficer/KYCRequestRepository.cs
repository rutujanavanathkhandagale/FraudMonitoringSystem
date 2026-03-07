using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Repositories.Customer.Interfaces.ComplianceOfficer;
using Microsoft.EntityFrameworkCore;
public class KYCRequestRepository : IKYCRequestRepository
{
    private readonly WebContext _context;
    public KYCRequestRepository(WebContext context)
    {
        _context = context;
    }
    public async Task<KYCProfile?> GetByCustomerIdAsync(long customerId)
    {
        return await _context.KYCProfile
                             .FirstOrDefaultAsync(x => x.CustomerId == customerId);
    }
}