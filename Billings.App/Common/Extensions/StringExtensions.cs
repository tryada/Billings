namespace Billings.App.Common.Extensions;

internal static class StringExtensions
{
    /// <summary>
    /// Sprawdza czy string nie jest pusty
    /// </summary>
    /// <param name="value">String</param>
    /// <returns>Czy string jest pusty</returns>
    internal static bool IsNotEmpty(this string value) => !string.IsNullOrEmpty(value);
    /// <summary>
    /// Zwraca wartość stringa lub null w przypadku gdy string jest pusty
    /// </summary>
    /// <param name="value">String</param>
    /// <returns>Wartość lub null</returns>
    internal static string? GetValueOrNull(this string value) => value.IsNotEmpty() ? value : null;
}