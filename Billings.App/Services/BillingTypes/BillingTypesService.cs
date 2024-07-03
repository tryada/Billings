using System.Net.Http.Json;

using Billings.App.Common.Http;
using Billings.App.Services.BillingTypes.Responses;
using Billings.App.Services.Interfaces;
using Billings.Models.Billings;
using Billings.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billings.App.Services.BillingTypes;

internal class BillingTypesService(
    IAuthenticationService authenticationService,
    IBillingTypesRepository billingsTypesRepository,
    IHttpClientFactory httpClientFactory,
    ILogger<BillingTypesService> logger) : IBillingTypesService
{
    private const string endpoint = "billing/billing-types";

    public async Task EnsureBillingTypes()
    {
        var client = httpClientFactory.CreateClient(HttpConstants.RequestHttpClientName);
        client.DefaultRequestHeaders.Authorization = authenticationService.GetAuthenticationRequestHeader();

        var response = await client.GetAsync(endpoint);
        var responseResult = await response.Content.ReadFromJsonAsync<List<BillingTypeResponse>>();

        var exising = await billingsTypesRepository.GetBillingTypes();

        var newTypes = responseResult
            .Where(x => exising.Any(e => e.BillingTypeId == x.Id) == false)
            .ToList();

        if (newTypes.Count > 0)
        {
            var toSaveList = new List<BillingType>();

            foreach (var type in newTypes)
            {
                var newType = new BillingType
                {
                    BillingTypeId = type.Id,
                    Description = type.Description,
                };

                toSaveList.Add(newType);
            }

            await billingsTypesRepository.Save(toSaveList);
            logger.LogInformation($"Ilość nowych typów rozliczeń: {toSaveList.Count}");
        }
    }
}
