using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure;

/// <summary>
/// Classe de extensão responsável por registrar os serviços da camada Infrastructure
/// no container de Injeção de Dependência.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra os UseCases da camada Infrastructure no container de serviços.
    /// </summary>
    /// <param name="services">Coleção de serviços do .NET para registro de dependências.</param>
    /// <returns>A mesma instância de <see cref="IServiceCollection"/> para encadeamento.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });

        return services;
    }
}
