
using Billings.Models.Billings;

namespace Billings.Persistence.Repositories.Interfaces;

public interface IBillingTypesRepository
{
    Task<List<BillingType>> GetBillingTypes();
    Task<Dictionary<string, int>> GetBillingTypesDictionary(List<string> billingTypesId);
    Task Save(List<BillingType> newTypes);
}