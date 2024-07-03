using Billings.App.Services.Billings.Responses;

namespace Billings.App.Services.Interfaces;

internal interface IBillingsService
{
    /// <summary>
    /// Płatności pobrane z Allegro API.
    /// </summary>
    GetBillingEntriesResponse CachedBillingEntriesResponse { get; }
    /// <summary>
    /// Pobiera płatności z Allegro API.
    /// </summary>
    Task Fetch();
}