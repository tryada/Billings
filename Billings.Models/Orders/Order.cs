namespace Billings.Models.Orders;

/// <summary>
/// Przedstawia zamówienie.
/// </summary>
public class Order
{
    /// <summary>
    /// Identyfikator zamówienia.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Zewnętrzy identyfikator zamówienia.
    /// </summary>
    public string OrderId { get; set; } = null!;
    /// <summary>
    /// Identyfikator zamówienia w systemie ERP.
    /// </summary>
    public int? ErpOrderId { get; set; }
    /// <summary>
    /// Identyfikator faktury.
    /// </summary>
    public int? InvoiceId { get; set; }
    /// <summary>
    /// Identyfikator sklepu.
    /// </summary>
    public int StoreId { get; set; }
}