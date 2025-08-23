using Application;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CLI;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        // Registrar dependências por camada
        services.AddDomainServices()          // Domain Layer
                .AddApplicationServices();   // Application Layer

        // Registrar o CLI
        services.AddSingleton<GanhoCapitalCLI>();

        var serviceProvider = services.BuildServiceProvider();

        // Resolver e executar o CLI
        var cli = serviceProvider.GetRequiredService<GanhoCapitalCLI>();
        cli.RunCLI();
    }
}