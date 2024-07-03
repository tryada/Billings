using Billings.Models.Tokens;

namespace Billings.Persistence.Repositories.Interfaces;

public interface IAllegroTokenRepository
{
    Task<AllegroToken?> GetToken();
    Task SaveToken(AllegroToken token);
}
