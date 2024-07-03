using Billings.App.Common.Options.Base;
using Microsoft.Extensions.Configuration;

namespace Billings.App.Services.Authentication.Options;

public class AuthOptionsSetup(
    IConfiguration configuration) : BaseEnvironmentOptionsSetup<AuthOptions>(configuration)
{
    private const string authUrlKey = "auth_url";
    private const string clientIdKey = "client_id";
    private const string clientSecretKey = "client_secret";

    protected override void SetupOptions(AuthOptions options)
    {
        options.AuthUrl = GetConfigurationValue(authUrlKey);
        options.ClientId = GetConfigurationValue(clientIdKey);
        options.ClientSecret = GetConfigurationValue(clientSecretKey);
    }
}