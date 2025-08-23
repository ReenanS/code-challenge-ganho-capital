using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<ICalculoImpostoGanhoCapitalService, CalculoImpostoGanhoCapitalService>();
        return services;
    }
}
