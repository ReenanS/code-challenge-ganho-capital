using Application;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CLI;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDomainServices()
                .AddApplicationServices();

        services.AddSingleton<GanhoCapitalCLI>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        GanhoCapitalCLI cli = serviceProvider.GetRequiredService<GanhoCapitalCLI>();
        cli.RunCLI();
    }
}
