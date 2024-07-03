using Billings.Models.Billings;
using Billings.Persistence.Database;
using Billings.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Billings.Persistence.Repositories;

internal class BillingsRepository(
    BillingsDbContext dbContext) : IBillingsRepository
{
    public Task<List<string>> GetExistingBillingIds(List<string> billingIds)
    {
        return dbContext.Billings
            .Where(b => billingIds.Contains(b.BillingId))
            .Select(b => b.BillingId)
            .ToListAsync();
    }

    public Task Save(List<Billing> billings)
    {
        dbContext.Billings.AddRange(billings);
        return dbContext.SaveChangesAsync();
    }
}