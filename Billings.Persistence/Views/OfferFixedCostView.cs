using Billings.Models.Offers;
using Billings.Persistence.Database;
using Billings.Persistence.Views.Interface;
using Microsoft.EntityFrameworkCore;

namespace Billings.Persistence.Views;

internal class OfferFixedCostView(
    BillingsDbContext dbContext) : IOfferFixedCostsView
{
    public async Task<List<OfferFixedCost>> Get()
    {
        return await dbContext.OfferFixedCosts.ToListAsync();
    }
}