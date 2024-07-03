using Billings.App.Services.Interfaces;
using Billings.App.Services.Processors.Interfaces;

namespace Billings.App;

internal class App(
    IAuthenticationService authenticationService,
    IBillingTypesService billingTypesService,
    IBillingsService billingsService,
    IBillingsProcessor billingsProcessor)
{
    internal async Task Run()
    {
        await authenticationService.Authenticate();

        await billingTypesService.EnsureBillingTypes();
        await billingsService.Fetch();

        await billingsProcessor.Process();
    }
}