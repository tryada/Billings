namespace Billings.App.Services.Authentication.Responses;

internal record GetAuthorizationResponse(
    string Access_token,
    string Token_type,
    string Refresh_token,
    int Expires_in,
    string Scope,
    bool Allegro_api,
    string Jti,
    string Error);