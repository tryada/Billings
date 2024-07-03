using Billings.Models.Offers;

namespace Billings.Persistence.Repositories.Interfaces;

public interface IOfferRepository
{
    Task AddOffer(Offer offer);
    Task<Dictionary<string, int>> GetOfferIdsDictionary(List<string> responseOfferIds);
}