
namespace Billings.Persistence.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Dictionary<string, int>> GetOrderIdsDictionary(List<string> responseOrderIds);
}