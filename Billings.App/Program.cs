using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Billings.App;
using Billings.App.Common;
using Billings.App.Arguments.Parser;
using Billings.Persistence;
using Billings.App.Services.Authentication.Options;
using Billings.App.Common.Options.Requests;
using Billings.App.Common.Http;
using Microsoft.Extensions.Logging;

try
{
    ArgumentsParser.Parse(args);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
    return;
}
catch (Exception)
{
    Console.WriteLine("Błąd podczas odczytywania argumentów.");
    return;
}

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
        config
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets<Program>();
    })
    .ConfigureServices((hostBuilder, services) =>
    {
        services
            .AddOptions()
            .ConfigureOptions<AuthOptionsSetup>()
            .ConfigureOptions<RequestOptionsSetup>();

        services
            .AddServices()
            .AddMappers()
            .AddProcessors()
            .AddPersistence(hostBuilder.Configuration);

        services.AddLogging(services => services.AddConsole());

        services.AddHttpClient(HttpConstants.RequestHttpClientName, (serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<RequestOptions>>().Value;
            httpClient.BaseAddress = new Uri(options.RequestUrl);
        });

        services.AddHttpClient(HttpConstants.AuthHttpClientName, (serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AuthOptions>>().Value;
            httpClient.BaseAddress = new Uri(options.AuthUrl);
        });
    })
    .Build();

try
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    await services.GetRequiredService<App>().Run();
}
catch (Exception e)
{
    Console.WriteLine("Wystąpił błąd programu.");
}