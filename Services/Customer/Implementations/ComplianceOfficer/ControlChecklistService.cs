using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Services.Customer.Interfaces;
using FraudMonitoringSystem.Services.Customer.Interfaces.Watchlist;

public class ControlChecklistService : IControlChecklistService
{
    private readonly IControlChecklistRepository _repo;
    private readonly IKYCService _kycService;
    private readonly IWatchlistService _watchlistService;
    private readonly ITransactionPatternService _txnService;

    public ControlChecklistService(
        IControlChecklistRepository repo,
        IKYCService kycService,
        IWatchlistService watchlistService,
        ITransactionPatternService txnService)
    {
        _repo = repo;
        _kycService = kycService;
        _watchlistService = watchlistService;
        _txnService = txnService;
    }

    // ========================= CREATE =========================
    public async Task<ControlChecklist> CreateAnalysisAsync(ControlChecklist checklist)
    {
        // FIX: Declare variables before using them
        var customerId = await _repo.GetCustomerIdByCaseIdAsync(checklist.CaseID);
        var transactionId = await _repo.GetTransactionIdByCaseIdAsync(checklist.CaseID);

        if (customerId == 0) throw new Exception("Customer not found.");

        // Fetch backend statuses using the declared IDs
        var kyc = await _kycService.VerifyByCustomerIdAsync(customerId);
        var kycStatus = (kyc != null && kyc.Status?.Trim().ToUpper() == "VERIFIED") ? "PASS" : "FAIL";

        var watchlist = await _watchlistService.IsCustomerOnWatchlistAsync(customerId);
        var watchlistStatus = (watchlist == true) ? "FAIL" : "PASS";

        var txn = await _txnService.AnalyzeAsync(customerId, transactionId);
        var txnStatus = (txn != null && txn.TransactionResult?.Trim().ToUpper() == "PASS") ? "PASS" : "FAIL";

        // Build the details list
        checklist.Details = new List<ControlDetail>
    {
        new ControlDetail { ControlName = "KYC Verification", Status = kycStatus },
        new ControlDetail { ControlName = "Watchlist Entity", Status = watchlistStatus },
        new ControlDetail { ControlName = "Transaction Pattern", Status = txnStatus }
    };

        // Strict Calculation
        checklist.OverallResult = (kycStatus == "PASS" && watchlistStatus == "PASS" && txnStatus == "PASS")
            ? "PASS"
            : "FAIL";

        return await _repo.AddAsync(checklist);
    }




    // ========================= UPDATE =========================
    public async Task<ControlChecklist> UpdateAnalysisAsync(int caseId, List<ControlDetail> details)
    {
        var existing = await _repo.GetByCaseIdAsync(caseId);
        if (existing == null) return null;

        existing.Details = details;
        existing.OverallResult = Calculate(details);

        return await _repo.UpdateAsync(existing);
    }

    // ========================= GET =========================
    public async Task<IEnumerable<ControlChecklist>> GetHistoryAsync(string status)
    {
        var data = status == "ALL"
            ? await _repo.GetAllAsync()
            : await _repo.GetByStatusAsync(status);

        return data ?? new List<ControlChecklist>();
    }

    // ========================= DELETE =========================
    public async Task<bool> RemoveAnalysisAsync(int caseId)
    {
        return await _repo.DeleteAsync(caseId);
    }

    // ========================= CALCULATE =========================
    private string Calculate(List<ControlDetail> details)
    {
        if (details.Any(d => d.Status == "PENDING"))
            return "PENDING";

        return details.All(d => d.Status == "PASS")
            ? "PASS"
            : "FAIL";
    }
}