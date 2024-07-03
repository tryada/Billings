namespace Billings.App.Services.Billings.Responses;

internal record GetBillingEntriesResponse(
    List<BillingEntry> BillingEntries
);

internal record BillingEntry(
    string Id,
    DateTime OccurredAt,
    Type Type,
    Offer Offer,
    Value Value,
    Tax Tax,
    Balance Balance,
    Order Order
);

internal record Type(
    string Id,
    string Name
);

internal record Offer(
    string Id,
    string Name
);

internal record Value(
    string Amount,
    string Currency
);

internal record Tax(
    string Percentage,
    string Annotation
);

internal record Balance(
    string Amount,
    string Currency
);

internal record Order(
    string Id
);