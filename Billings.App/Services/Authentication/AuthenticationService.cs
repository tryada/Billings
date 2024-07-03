using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

using Billings.App.Services.Authentication.Options;
using Billings.App.Services.Authentication.Responses;
using Billings.App.Services.Interfaces;
using Billings.Persistence.Repositories.Interfaces;
using Billings.Models.Tokens;
using Billings.App.Common.Http;
using Billings.App.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Billings.App.Services.Authentication;

internal class AuthenticationService(
    IOptions<AuthOptions> options,
    IAllegroTokenRepository tokenRepository,
    IHttpClientFactory httpClientFactory,
    ILogger<AuthenticationService> logger) : IAuthenticationService
{
    private const string errorMessage = "Błąd autoryzacji. Proszę spróbować później.";

    private readonly AuthOptions optionsValue = options.Value;

    private string CodeUrl =>
        $"device?client_id={optionsValue.ClientId}";

    private string TokenUrl =>
        $"token";

    private string accessToken = null!;

    /// <summary>
    /// Metoda autoryzująca
    /// 1. W przypadku braku tokenu pobiera nowy
    /// 2. Kiedy token jest ważny, zostanie wykorzystany do pobierania zasobów
    /// 2. W przypadku wygaśnięcia tokenu odświeża go
    /// </summary>
    public async Task Authenticate()
    {
        var token = await tokenRepository.GetToken();
        if (token is null)
        {
            logger.LogWarning("Nie znaleziono tokenu. Pobieranie nowego...");
            await GetToken();
            return;
        }

        if (token.ExpiresAt > DateTime.UtcNow)
        {
            logger.LogInformation("Token jest nadal ważny.");
            accessToken = token.AccessToken;
            return;
        }

        logger.LogInformation("Token wygasł. Odświeżanie...");
        await RefreshOrGetToken(token);
    }

    /// <summary>
    /// Odświeża token, w przypadku niepowodzenia próbuje pobrać nowy.
    /// </summary>
    private async Task RefreshOrGetToken(AllegroToken token)
    {
        using var client = httpClientFactory.CreateClient(HttpConstants.AuthHttpClientName);
        client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue();
        var data = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", token.RefreshToken.Replace('a','b'))
        ]);

        var response = await client.PostAsync(TokenUrl, data);

        if (response.IsSuccessStatusCode)
        {
            var responseObject = await response.Content.ReadFromJsonAsync<GetAuthorizationResponse>();

            token.Refresh(
                responseObject.Access_token,
                responseObject.Refresh_token,
                responseObject.Expires_in);

            await tokenRepository.SaveToken(token);
            accessToken = token.AccessToken;
        }
        else
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                logger.LogWarning("Błąd autoryzacji. Pobieranie nowego tokenu...");
                await GetToken();
            }
            else
            {
                logger.LogError("Błąd autoryzacji. Kod błędu: {0}", response.StatusCode);
                throw new Exception(errorMessage);
            }
        }
    }

    /// <summary>
    /// Pobiera nowy token dostępu, należy podać istniejący token w przypadku nieudanej próby odświeżania.
    /// Wymagane jest powiązanie aplikacji za pomocą linku.
    /// Nadpisuje istniejący token.
    /// </summary>
    /// <param name="existedToken">Istniejący token</param>
    private async Task GetToken(AllegroToken? existedToken = null)
    {
        var codesResult = await DoDeviceCodeRequest();

        System.Console.WriteLine("Otwórz poniższy adres URL w przeglądarce:");
        System.Console.WriteLine(codesResult.Verification_uri_complete);

        await AwaitForAccessToken(codesResult, existedToken);
    }

    private async Task<GetDeviceCodeResponse> DoDeviceCodeRequest()
    {
        var client = httpClientFactory.CreateClient(HttpConstants.AuthHttpClientName);
        client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue();

        var response = await client.PostAsync(CodeUrl, new FormUrlEncodedContent([]));
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Błąd autoryzacji. Kod błędu: {0}", response.StatusCode);
            throw new Exception(errorMessage);
        }

        return await response.Content.ReadFromJsonAsync<GetDeviceCodeResponse>();
    }

    /// <summary>
    /// Oczekuje na potwierdzenie dostępu do aplikacji i pobiera token dostępu
    /// Odpytuje o token dostępu co określony czas
    /// </summary>
    private async Task AwaitForAccessToken(GetDeviceCodeResponse codeResult, AllegroToken? existedToken)
    {
        var interval = codeResult.Interval;

        while (true)
        {
            logger.LogInformation("Oczekiwanie na token dostępu...");

            await Task.Delay(codeResult.Interval * 1000);
            var authResult = await DoAuthorizationRequest(codeResult.Device_code);

            if (authResult.Error.IsNotEmpty())
            {
                if (authResult.Error == "slow_down")
                    interval += codeResult.Interval;
                else if (authResult.Error == "access_denied")
                    break;
            }
            else if (authResult.Access_token.IsNotEmpty())
            {
                AllegroToken toSave;
                if (existedToken is not null)
                {
                    existedToken.Refresh(
                        authResult.Access_token,
                        authResult.Refresh_token,
                        authResult.Expires_in);

                    toSave = existedToken;
                }
                else
                {
                    toSave = AllegroToken.Create(
                        authResult.Access_token,
                        authResult.Refresh_token,
                        authResult.Expires_in);
                }

                await tokenRepository.SaveToken(toSave);
                accessToken = toSave.AccessToken;
                return;
            }
        }

        logger.LogError("Błąd autoryzacji. Brak dostępu do aplikacji.");
        throw new Exception(errorMessage);
    }

    /// <summary>
    /// Odpytuje o token dostępu
    /// </summary>
    private async Task<GetAuthorizationResponse> DoAuthorizationRequest(string deviceCode)
    {
        var client = httpClientFactory.CreateClient(HttpConstants.AuthHttpClientName);
        var data = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:device_code"),
            new KeyValuePair<string, string>("device_code", deviceCode)
        ]);
        client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue();

        var response = await client.PostAsync(TokenUrl, data);
        return await response.Content.ReadFromJsonAsync<GetAuthorizationResponse>();
    }

    /// <summary>
    /// Zwraca nagłówek autoryzacyjny dla autoryzacji
    /// </summary>
    private AuthenticationHeaderValue GetAuthenticationHeaderValue()
    {
        var clientId = optionsValue.ClientId;
        var clientSecret = optionsValue.ClientSecret;

        var credentials = $"{clientId}:{clientSecret}";

        return new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials))
        );
    }

    /// <summary>
    /// Zwraca nagłówek wykorzystywany do pobrania zasobów z Allegro API
    /// </summary>
    public AuthenticationHeaderValue GetAuthenticationRequestHeader()
    {
        return new AuthenticationHeaderValue("Bearer", accessToken);
    }
}