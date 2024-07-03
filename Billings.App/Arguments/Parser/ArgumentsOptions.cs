using CommandLine;

namespace Billings.App.Arguments.Parser;

internal class ArgumentsOptions
{
    [Option(
        "applicationName",
        Required = false,
        HelpText = "The name of the application that made the operation.",
        Hidden = true
    )]
    public string ApplicationName { get; set; } = string.Empty; // ef database update

    [Option(
        'e',
        "environment",
        Required = false,
        HelpText = "The environment in which the operation was made. Possible values: sandbox, production. Default value: sandbox",
        Default = "sandbox"
    )]
    public string Environment { get; set; } = string.Empty;

    [Option(
        'm',
        "marketplace-id",
        Required = false,
        HelpText = "The marketplace ID where operation was made. By default the marketplace ID where the user is registered."
    )]
    public string MarketPlaceId { get; set; } = string.Empty;

    [Option(
        "occured-at-gte",
        Required = false,
        HelpText = "Date from which billing entries are filtered. If occurredAt.lte is also set, occurredAt.gte cannot be later."
    )]
    public DateTime? OccuredAt_Gte { get; set; } = null;

    [Option(
        "occured-at-lte",
        Required = false,
        HelpText = "Date to which billing entries are filtered. If occurredAt.gte is also set, occurredAt.lte cannot be earlier."
    )]
    public DateTime? OccuredAt_Lte { get; set; } = null;

    [Option(
        "type-id",
        Required = false,
        HelpText = "List of billing types by which billing entries are filtered."
    )]
    public string? Type_Id { get; set; } = string.Empty;

    [Option(
        "offer-id",
        Required = false,
        HelpText = "Offer ID by which billing entries are filtered."
    )]
    public string? Offer_Id { get; set; } = string.Empty;

    [Option(
        "order-id",
        Required = false,
        HelpText = "Order UUID by which billing entries are filtered."
    )]
    public string? Order_Id { get; set; } = string.Empty;

    [Option(
        "limit",
        Required = false,
        HelpText = "Number of returned operations. Default value: 100."
    )]
    public int Limit { get; set; } = 100;

    [Option(
        "offset",
        Required = false,
        HelpText = "Index of the first returned payment operation from all search results. Default value: 0."
    )]
    public int Offset { get; set; } = 0;
}