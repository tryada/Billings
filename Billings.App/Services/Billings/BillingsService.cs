using System.Web;
using System.Net.Http.Json;

using Billings.App.Arguments;
using Billings.App.Common.Http;
using Billings.App.Services.Billings.Responses;
using Billings.App.Services.Interfaces;
using Billings.App.Common.Extensions;

namespace Billings.App.Services.Billings;

internal class BillingsService(
    IAuthenticationService authenticationService,
    IHttpClientFactory httpClientFactory) : IBillingsService
{
    private const string endpoint = "billing/billing-entries";

    private GetBillingEntriesResponse? cachedBillingEntriesResponse = null;
    public GetBillingEntriesResponse CachedBillingEntriesResponse
    {
        get
        {
            if (cachedBillingEntriesResponse is null)
                throw new InvalidOperationException("Billing entries not fetched");

            return cachedBillingEntriesResponse;
        }
    }

    public async Task Fetch()
    {
        var client = httpClientFactory.CreateClient(HttpConstants.RequestHttpClientName);
        client.DefaultRequestHeaders.Authorization = authenticationService.GetAuthenticationRequestHeader();

        var response = await client.GetAsync(BuildUrl(client));

        cachedBillingEntriesResponse = await response.Content.ReadFromJsonAsync<GetBillingEntriesResponse>()
            ?? throw new InvalidOperationException("Failed to fetch billing entries");
    }

    private string BuildUrl(HttpClient client)
    {
        var builder = new UriBuilder(client.BaseAddress + endpoint);
        var query = HttpUtility.ParseQueryString(builder.Query);

        if (AppArguments.MarketPlaceId is not null)
            query["marketplaceId"] = AppArguments.MarketPlaceId;

        if (AppArguments.DateFrom is not null)
            query["occurredAt.gte"] = AppArguments.DateFrom.Value.ToFormattedString();

        if (AppArguments.DateTo is not null)
            query["occurredAt.lte"] = AppArguments.DateTo.Value.ToFormattedString();

        if (AppArguments.TypeId is not null)
            query["type.id"] = AppArguments.TypeId;

        if (AppArguments.OfferId is not null)
            query["offer.id"] = AppArguments.OfferId;

        if (AppArguments.OrderId is not null)
            query["order.id"] = AppArguments.OrderId;

        query["limit"] = $"{AppArguments.Limit}";
        query["offset"] = $"{AppArguments.Offset}";

        builder.Query = query.ToString();
        return builder.ToString();
    }
}