namespace Billings.App.Arguments;

/// <summary>
/// Zawiera argumenty aplikacji.
/// </summary>
public static class AppArguments
{
    internal const string ProductionEnvironmentKey = "production";
    internal const string SandboxEnvironmentKey = "sandbox";

    public static string Environment { get; private set; } = null!;
    public static string? MarketPlaceId { get; private set; }
    public static DateTime? DateFrom { get; private set; }
    public static DateTime? DateTo { get; private set; }
    public static string? TypeId { get; private set; }
    public static string? OfferId { get; private set; }
    public static string? OrderId { get; private set; }
    public static int Limit { get; private set; }
    public static int Offset { get; private set; }

    public static void SetArguments(
        string environment,
        string? marketPlaceId,
        DateTime? dateFrom,
        DateTime? dateTo,
        string? typeId,
        string? offerId,
        string? orderId,
        int limit,
        int offset)
    {
        Environment = environment;
        MarketPlaceId = marketPlaceId;
        DateFrom = dateFrom;
        DateTo = dateTo;
        TypeId = typeId;
        OfferId = offerId;
        OrderId = orderId;
        Limit = limit;
        Offset = offset;
    }
}