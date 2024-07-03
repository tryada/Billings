using Billings.App.Arguments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Billings.App.Common.Options.Base;

/// <summary>
/// Bazowa klasa do konfiguracji opcji zależnych od środowiska
/// </summary>
public abstract class BaseEnvironmentOptionsSetup<T>(
    IConfiguration configuration) : IConfigureOptions<T> where T : class
{
    private readonly List<string> AvailableEnviroments =
    [
        AppArguments.ProductionEnvironmentKey,
        AppArguments.SandboxEnvironmentKey
    ];

    private string environment = null!;

    public void Configure(T options)
    {
        environment = AppArguments.Environment;

        if (AvailableEnviroments.Contains(environment) == false)
            throw new ArgumentException($"Invalid enviroment argument: {environment}");

        SetupOptions(options);
    }

    protected abstract void SetupOptions(T options);

    protected string GetConfigurationValue(string key)
    {
        return configuration[$"{environment}:{key}"]!;
    }
}