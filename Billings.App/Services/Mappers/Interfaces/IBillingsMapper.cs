using Billings.App.Services.Billings.Responses;
using Billings.Models.Billings;

namespace Billings.App.Services.Mappers.Interfaces;

internal interface IBillingsMapper
{
    Task<List<Billing>> Map(List<BillingEntry> entries);
}