using Billings.App.Services.Interfaces;
using Billings.App.Services.Mappers.Interfaces;
using Billings.App.Services.Processors.Interfaces;
using Billings.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billings.App.Services.Processors;

internal class BillingsProcessor(
    IBillingsService billingsService,
    IBillingsRepository billingsRepository,
    IBillingsMapper billingsMapper,
    ILogger<BillingsProcessor> logger) : IBillingsProcessor
{
    public async Task Process()
    {
        var billings = billingsService.CachedBillingEntriesResponse;
        if (billings == null)
            return;

        var billingIds = billings.BillingEntries
            .Select(b => b.Id)
            .Distinct()
            .ToList();

        var existingBillings = await billingsRepository.GetExistingBillingIds(billingIds);

        var candidateToSave = billings.BillingEntries
            .Where(b => !existingBillings.Contains(b.Id))
            .ToList();

        if (candidateToSave.Count == 0)
            return;

        var toSave = await billingsMapper.Map(candidateToSave);
        logger.LogInformation($"Ilość nowych rozliczeń: {toSave.Count}");
        await billingsRepository.Save(toSave);
    }
}