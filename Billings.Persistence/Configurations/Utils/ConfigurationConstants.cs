namespace Billings.Persistence.Configurations.Utils;

internal static class ConfigurationConstants
{
    public static class TableNames
    {
        public const string OfferFixedCosts = "OfferFixedCosts";
        public const string AllegroTokens = "AllegroTokens";
        public const string Orders = "Orders";
        public const string Billings = "Billings";
        public const string BillingTypes = "BillingTypes";
        public const string Offers = "Offers";
    }

    public static class Types
    {
        public const string Decimal = "decimal(18,2)";
    }
}