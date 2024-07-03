using Billings.Models.Offers;
using Billings.Persistence.Database;
using Billings.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Billings.Persistence.Repositories;

internal class OfferRepository(
    BillingsDbContext context) : IOfferRepository
{
    public async Task AddOffer(Offer offer)
    {
        await context.Offers.AddAsync(offer);
        await context.SaveChangesAsync();
    }

    public async Task<Dictionary<string, int>> GetOfferIdsDictionary(List<string> responseOfferIds)
    {
        return await context.Offers
            .Where(offer => responseOfferIds.Contains(offer.OfferId))
            .ToDictionaryAsync(offer => offer.OfferId, offer => offer.Id);
    }
}