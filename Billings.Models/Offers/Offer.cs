namespace Billings.Models.Offers;

/// <summary>
/// Przedstawia ofertę.
/// </summary>
public class Offer
{
    /// <summary>
    /// Identyfikator oferty.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Zewnętrzny identyfikator oferty.
    /// </summary>
    public string OfferId { get; set; } = null!;
    /// <summary>
    /// Nazwa oferty.
    /// </summary>
    public string Name { get; set; } = null!;
}