using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IApurarGanhoCapitalUseCase, ApurarGanhoCapitalUseCase>();

        return services;
    }
}
