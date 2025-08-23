using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registrar UseCases
        services.AddTransient<ApurarGanhoCapitalUseCase>();
        return services;
    }
}