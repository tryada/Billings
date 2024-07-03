namespace Billings.App.Common.Extensions;

internal static class DateTimeExtensions
{
    /// <summary>
    /// Formatuje datę do postaci yyyy-MM-ddTHH:mm:ss.fffZ
    /// Format wykorzystywany przez Allegro API
    /// </summary>
    /// <param name="dateTime">Data</param>
    /// <returns>Sformatowana data</returns>
    internal static string ToFormattedString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}