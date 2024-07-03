using Billings.App.Common.Options.Base;
using Microsoft.Extensions.Configuration;

namespace Billings.App.Common.Options.Requests;

public class RequestOptionsSetup(
    IConfiguration configuration) : BaseEnvironmentOptionsSetup<RequestOptions>(configuration)
{
    private const string requestUrlKey = "request_url";

    protected override void SetupOptions(RequestOptions options)
    {
        options.RequestUrl = GetConfigurationValue(requestUrlKey);
    }
}