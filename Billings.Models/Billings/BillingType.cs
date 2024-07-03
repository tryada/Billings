namespace Billings.Models.Billings;

/// <summary>
/// Rodzaj płatności.
/// </summary>
public class BillingType
{
    /// <summary>
    /// Identyfikator rodzaju płatności.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Zewnętrzny identyfikator rodzaju płatności.
    /// </summary>
    public string BillingTypeId { get; set; } = null!;
    /// <summary>
    /// Nazwa rodzaju płatności.
    /// </summary>
    public string Description { get; set; } = null!;
}