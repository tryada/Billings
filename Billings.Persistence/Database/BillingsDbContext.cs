using Microsoft.EntityFrameworkCore;

using Billings.Models.Billings;
using Billings.Models.Offers;
using Billings.Models.Orders;
using Billings.Models.Tokens;

namespace Billings.Persistence.Database;

internal class BillingsDbContext(
    DbContextOptions<BillingsDbContext> options) : DbContext(options)
{
    public DbSet<Billing> Billings { get; set; }
    public DbSet<BillingType> BillingTypes { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferFixedCost> OfferFixedCosts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<AllegroToken> AllegroTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingsDbContext).Assembly);
    }
}