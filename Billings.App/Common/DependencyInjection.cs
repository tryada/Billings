using Microsoft.Extensions.DependencyInjection;

using Billings.App.Services.Authentication;
using Billings.App.Services.Billings;
using Billings.App.Services.Interfaces;
using Billings.App.Services.Processors.Interfaces;
using Billings.App.Services.Processors;
using Billings.App.Services.Mappers.Interfaces;
using Billings.App.Services.Mappers;
using Billings.App.Services.BillingTypes;

namespace Billings.App.Common;

internal static class DependencyInjection
{
    /// <summary>
    /// Dpodaje serwisy.
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<App>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IBillingTypesService, BillingTypesService>()
            .AddScoped<IBillingsService, BillingsService>();
    }

    /// <summary>
    /// Dodaje obiekty przetwarzające dane.
    /// </summary>
    public static IServiceCollection AddProcessors(this IServiceCollection services)
    {
        return services
            .AddScoped<IBillingsProcessor, BillingsProcessor>();
    }

    /// <summary>
    /// Dodaje mappery.
    /// </summary>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services
            .AddScoped<IBillingsMapper, BillingsMapper>();
    }
}