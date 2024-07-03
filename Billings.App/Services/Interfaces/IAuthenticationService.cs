using System.Net.Http.Headers;

namespace Billings.App.Services.Interfaces;

/// <summary>
/// Serwis odpowiedzialny za autoryzację użytkownika.
/// </summary>
internal interface IAuthenticationService
{
    /// <summary>
    /// Metoda autoryzująca użytkownika.
    /// </summary>
    Task Authenticate();
    /// <summary>
    /// Zwraza nagłówek autoryzacyjny.
    /// </summary>
    /// <returns>Nagłówek z tokenem</returns>
    AuthenticationHeaderValue GetAuthenticationRequestHeader();
}