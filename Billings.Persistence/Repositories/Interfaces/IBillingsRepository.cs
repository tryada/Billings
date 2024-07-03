using Billings.Models.Billings;

namespace Billings.Persistence.Repositories.Interfaces;

public interface IBillingsRepository
{
    Task Save(List<Billing> billings);
    Task<List<string>> GetExistingBillingIds(List<string> billingIds);
}