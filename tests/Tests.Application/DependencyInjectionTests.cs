using Application;
using Application.UseCases;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Tests.Application;

/// <summary>
/// Testes unitários para a classe de extensão <see cref="DependencyInjection"/>.
/// Cobre registro de serviços no container de DI.
/// </summary>
[Collection("DependencyInjectionTests Collection")]
public class DependencyInjectionTests
{

    /// <summary>
    /// Verifica que todos os serviços esperados estão registrados corretamente.
    /// Facilita testes futuros caso novos serviços sejam adicionados.
    /// </summary>
    [Fact(DisplayName = "Verificação de registros adicionais deve passar caso novos serviços sejam adicionados")]
    public void AddApplicationServices_RegistroDeServicos_Futuro()
    {
        // Arrange
        // Arrange
        var mockService = new Mock<ICalculoImpostoGanhoCapitalService>();
        var services = new ServiceCollection();
        services.AddSingleton(mockService.Object);

        // Act
        var returnedServices = services.AddApplicationServices();
        var provider = returnedServices.BuildServiceProvider();

        // Assert
        var apurarUseCase = provider.GetService<ApurarGanhoCapitalUseCase>();
        Assert.NotNull(apurarUseCase);
        Assert.IsType<ApurarGanhoCapitalUseCase>(apurarUseCase);
    }
}
