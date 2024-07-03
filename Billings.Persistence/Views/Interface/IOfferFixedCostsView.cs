using Billings.Models.Offers;

namespace Billings.Persistence.Views.Interface;

public interface IOfferFixedCostsView
{
    Task<List<OfferFixedCost>> Get();
}
