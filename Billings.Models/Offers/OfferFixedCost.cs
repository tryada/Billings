namespace Billings.Models.Offers;

public class OfferFixedCost
{
    public int OfferId { get; set; }
    public string OfferName { get; set; } = null!;
    public decimal Value { get; set; }
}