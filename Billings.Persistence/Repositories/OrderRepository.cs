using Billings.Persistence.Database;
using Billings.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Billings.Persistence.Repositories;

internal class OrderRepository(
    BillingsDbContext billingsDbContext) : IOrderRepository
{
    public async Task<Dictionary<string, int>> GetOrderIdsDictionary(List<string> responseOrderIds)
    {
        return await billingsDbContext.Orders
            .Where(order => responseOrderIds.Contains(order.OrderId))
            .ToDictionaryAsync(order => order.OrderId, order => order.Id);
    }
}