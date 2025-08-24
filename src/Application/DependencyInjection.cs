using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Classe de extensão responsável por registrar os serviços da camada Application
/// no container de Injeção de Dependência.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra os UseCases da camada Application no container de serviços.
    /// </summary>
    /// <param name="services">Coleção de serviços do .NET para registro de dependências.</param>
    /// <returns>A mesma instância de <see cref="IServiceCollection"/> para encadeamento.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IApurarGanhoCapitalUseCase, ApurarGanhoCapitalUseCase>();

        return services;
    }
}
