using FraudMonitoringSystem.Data;
using FraudMonitoringSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ControlChecklistRepository : IControlChecklistRepository

{

    private readonly WebContext _context;

    public ControlChecklistRepository(WebContext context)

    {

        _context = context;

    }

    public async Task<ControlChecklist> ExecuteChecklist(int caseId, string checkedBy)

    {

        var caseData = await _context.Cases

            .Include(c => c.PrimaryCustomer)

            .FirstOrDefaultAsync(c => c.CaseID == caseId);

        if (caseData == null)

            throw new Exception("Case not found");

        var customer = caseData.PrimaryCustomer;

        if (customer == null)

            throw new Exception("Customer not found");

        // 🔹 KYC

        var kyc = await _context.KYCProfile

            .FirstOrDefaultAsync(k => k.CustomerId == customer.CustomerId);

        bool isKycVerified = kyc != null && kyc.Status == "Verified";

        // 🔹 PEP

        bool isPepMatch = await _context.PEPList

            .AnyAsync(p =>

                p.FirstName == customer.FirstName &&

                p.DOB == customer.DOB);

        // 🔹 Sanction

        bool isSanctionMatch = await _context.Sanctions

            .AnyAsync(s =>

                s.FirstName == customer.FirstName &&

                s.DOB == customer.DOB);

        // 🔹 Transaction

        var transactions = await _context.Transaction

            .Where(t => t.CustomerId == customer.CustomerId)

            .ToListAsync();

        bool isTransactionSuspicious = transactions.Any(t =>

            t.Amount > 1000000 ||

            t.SourceType == "CryptoExchange" ||

            t.SourceType == "ForeignTransfer");

        // 🔥 FINAL BUSINESS RULE

        bool finalStatus =

            isKycVerified &&

            !isPepMatch &&

            !isSanctionMatch &&

            !isTransactionSuspicious;

        var checklist = new ControlChecklist

        {

            CaseID = caseId,

            CheckedBy = checkedBy,

            CheckedDate = DateTime.Now,

            Result = finalStatus ? "Pass" : "Fail"

        };

        await _context.Control_Checklist.AddAsync(checklist);

        await _context.SaveChangesAsync();

        return checklist;

    }

    public async Task<List<ControlChecklist>> GetAllAsync()

    {

        return await _context.Control_Checklist.ToListAsync();

    }

    public async Task<List<ControlChecklist>> GetByResultAsync(string result)

    {

        return await _context.Control_Checklist

            .Where(x => x.Result == result)

            .ToListAsync();

    }

}
