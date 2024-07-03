using Billings.Models.Offers;
using Billings.Models.Orders;

namespace Billings.Models.Billings;

/// <summary>
/// Przedstawia rozliczenie.
/// </summary>
public class Billing
{
    /// <summary>
    /// Identyfikator rozliczenia.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Zeewnętrzny identyfikator rozliczenia.
    /// </summary>
    public string BillingId { get; set; } = null!;
    /// <summary>
    /// Data wystąpienia rozliczenia.
    /// </summary>
    public DateTime OccurredAt { get; set; }
    /// <summary>
    /// Identyfikator oferty.
    /// </summary>
    public int? OfferId { get; set; }
    /// <summary>
    /// Oferta.
    /// </summary>
    public Offer Offer { get; set; } = null!;
    /// <summary>
    /// Identyfikator płatności.
    /// </summary>
    public int BillingTypeId { get; set; }
    /// <summary>
    /// Rodzaj rozliczenia.
    /// </summary>
    public BillingType BillingType { get; set; } = null!;
    /// <summary>
    /// Identyfikat zamówienia.
    /// </summary>
    public int? OrderId { get; set; }
    /// <summary>
    /// Zamówienie.
    /// </summary>
    public Order? Order { get; set; }
    /// <summary>
    /// Warość rozliczenia.
    /// </summary>
    public decimal Value { get; set; }
    /// <summary>
    /// Waluta wartości rozliczenia.
    /// </summary>
    public string ValueCurrencyCode { get; set; } = null!;
    /// <summary>
    /// Saldo po rozliczeniu.
    /// </summary>
    public decimal Balance { get; set; }
    /// <summary>
    /// Waluta salda po rozliczeniu.
    /// </summary>
    public string BalanceCurrencyCode { get; set; } = null!;
}