using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

/// <summary>
/// Classe de extensão responsável por registrar os serviços da camada Domain
/// no container de Injeção de Dependência.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra os UseCases da camada Domain no container de serviços.
    /// </summary>
    /// <param name="services">Coleção de serviços do .NET para registro de dependências.</param>
    /// <returns>A mesma instância de <see cref="IServiceCollection"/> para encadeamento.</returns>
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<ICalculoImpostoGanhoCapitalService, CalculoImpostoGanhoCapitalService>();

        return services;
    }
}
