using Billings.Models.Billings;
using Billings.Persistence.Database;
using Billings.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Billings.Persistence.Repositories;

internal class BillingTypesRepository(
    BillingsDbContext billingsDbContext) : IBillingTypesRepository
{
    public Task<List<BillingType>> GetBillingTypes()
    {
        return billingsDbContext.BillingTypes.ToListAsync();
    }

    public Task<Dictionary<string, int>> GetBillingTypesDictionary(List<string> billingTypesId)
    {
        return billingsDbContext.BillingTypes
            .Where(x => billingTypesId.Contains(x.BillingTypeId))
            .ToDictionaryAsync(x => x.BillingTypeId, x => x.Id);
    }

    public Task Save(List<BillingType> newTypes)
    {
        billingsDbContext.BillingTypes.UpdateRange(newTypes);
        return billingsDbContext.SaveChangesAsync();
    }
}