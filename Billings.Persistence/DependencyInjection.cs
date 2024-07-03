using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Billings.Persistence.Database;
using Billings.Persistence.Repositories;
using Billings.Persistence.Repositories.Interfaces;
using Billings.Persistence.Views.Interface;
using Billings.Persistence.Views;

namespace Billings.Persistence;

public static class DependencyInjection
{
    private const string defaultConnectionName = "DefaultConnection";

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDbContext<BillingsDbContext>((options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(defaultConnectionName));
            })
            .AddRepositories()
            .AddViews();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOfferRepository, OfferRepository>()
            .AddScoped<IBillingsRepository, BillingsRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IBillingTypesRepository, BillingTypesRepository>()
            .AddScoped<IAllegroTokenRepository, AllegroTokenRepository>();
    }

    public static IServiceCollection AddViews(
        this IServiceCollection services)
    {
        return services
            .AddScoped<IOfferFixedCostsView, OfferFixedCostView>();
    }
}