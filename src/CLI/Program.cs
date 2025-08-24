using Application;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CLI;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = new ServiceCollection();

        // Registrar dependências por camada
        services.AddDomainServices()
                .AddApplicationServices();

        // Registrar o CLI
        services.AddSingleton<GanhoCapitalCLI>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        // Resolver e executar o CLI
        GanhoCapitalCLI cli = serviceProvider.GetRequiredService<GanhoCapitalCLI>();
        cli.RunCLI();
    }
}