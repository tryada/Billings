using Microsoft.EntityFrameworkCore;

using Billings.Models.Tokens;
using Billings.Persistence.Database;
using Billings.Persistence.Repositories.Interfaces;

namespace Billings.Persistence.Repositories;

internal class AllegroTokenRepository(
    BillingsDbContext dbContext) : IAllegroTokenRepository
{
    public async Task<AllegroToken?> GetToken()
    {
        return await dbContext.AllegroTokens.SingleOrDefaultAsync();
    }

    public async Task SaveToken(AllegroToken token)
    {
        dbContext.AllegroTokens.Update(token);
        await dbContext.SaveChangesAsync();
    }
}
