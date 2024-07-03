using Billings.App.Common.Extensions;
using CommandLine;

namespace Billings.App.Arguments.Parser;

/// <summary>
/// Odpowiada za parsowanie argumentów programu
/// </summary>
internal class ArgumentsParser
{
    /// <summary>
    /// Parsuje argumenty aplikacji oraz ustawia statyczne pola klasy AppArguments
    /// </summary>
    /// <param name="args">Argumenty</param>
    public static void Parse(string[] args)
    {
        var result = CommandLine.Parser.Default.ParseArguments<ArgumentsOptions>(args)
            .WithNotParsed(errors => throw new ArgumentException("Niepoprawne argumenty"));

        var resultValue = result.Value;

        Validate(resultValue);

#pragma warning disable CS8604 // Possible null reference argument.
        AppArguments.SetArguments(
            environment: resultValue.Environment,
            marketPlaceId: resultValue.MarketPlaceId.GetValueOrNull(),
            dateFrom: resultValue.OccuredAt_Gte,
            dateTo: resultValue.OccuredAt_Lte,
            typeId: resultValue.Type_Id.GetValueOrNull(),
            offerId: resultValue.Offer_Id.GetValueOrNull(),
            orderId: resultValue.Order_Id.GetValueOrNull(),
            limit: resultValue.Limit,
            offset: resultValue.Offset);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Waliduje argumenty
    /// </summary>
    /// <param name="resultValue">Argumenty aplikacji</param>
    private static void Validate(ArgumentsOptions resultValue)
    {
        if (resultValue.OccuredAt_Gte is not null && resultValue.OccuredAt_Lte is not null)
        {
            if (resultValue.OccuredAt_Gte > resultValue.OccuredAt_Lte)
                throw new ArgumentException("Data occured-at-gte nie może być wcześniejsza niż occured-at-lte.");
        }
    }
}