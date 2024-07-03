namespace Billings.App.Services.Authentication.Options;

/// <summary>
/// Opcje do wykonania autoryzacji
/// </summary>
public class AuthOptions
{
    public string AuthUrl { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}