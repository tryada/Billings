using Billings.App.Services.Billings.Responses;
using Billings.App.Services.Mappers.Interfaces;
using Billings.Models.Billings;
using Billings.Persistence.Repositories.Interfaces;

namespace Billings.App.Services.Mappers;

internal class BillingsMapper(
    IBillingTypesRepository billingTypesRepository,
    IOfferRepository offerRepository,
    IOrderRepository orderRepository) : IBillingsMapper
{
    public async Task<List<Billing>> Map(List<BillingEntry> entries)
    {
        var result = new List<Billing>();

        var responseOfferIds = entries
            .Where(entry => entry.Offer != null)
            .Select(entry => entry.Offer.Id)
            .Distinct()
            .ToList();

        var responseOrderIds = entries
            .Where(entry => entry.Order != null)
            .Select(entry => entry.Order.Id)
            .Distinct()
            .ToList();

        var billingTypesId = entries
            .Select(entry => entry.Type.Id)
            .Distinct()
            .ToList();

        var offerIdsDictionary = await offerRepository.GetOfferIdsDictionary(responseOfferIds);
        var orderIdsDictionary = await orderRepository.GetOrderIdsDictionary(responseOrderIds);
        var billingTypesDictionary = await billingTypesRepository.GetBillingTypesDictionary(billingTypesId);

        foreach (var entry in entries)
        {
            var billing = new Billing
            {
                BillingId = entry.Id,
                OccurredAt = entry.OccurredAt,

                BillingTypeId = billingTypesDictionary[entry.Type.Id],

                OfferId = GetValueSafe(offerIdsDictionary, entry.Offer?.Id),
                OrderId = GetValueSafe(orderIdsDictionary, entry.Order?.Id),

                Value = decimal.Parse(entry.Value.Amount),
                ValueCurrencyCode = entry.Value.Currency,

                Balance = decimal.Parse(entry.Balance.Amount),
                BalanceCurrencyCode = entry.Balance.Currency,
            };

            result.Add(billing);
        }
            
        return result;
    }

    private static int? GetValueSafe(Dictionary<string, int> dictionary, string? key)
    {
        if (key == null)
            return null;

        if (dictionary.TryGetValue(key, out int value))
            return value;
        
        return null;
    }
}