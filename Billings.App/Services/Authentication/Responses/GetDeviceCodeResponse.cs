namespace Billings.App.Services.Authentication.Responses;

internal record GetDeviceCodeResponse(
    string User_code,
    string Device_code,
    int Expires_in,
    int Interval,
    string Verification_uri,
    string Verification_uri_complete);