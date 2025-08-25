using Application;
using Application.Interfaces;
using Application.UseCases;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace UnitTests.Application;

[Collection("DependencyInjectionTests Collection")]
public class DependencyInjectionTests
{

    [Fact(DisplayName = "Verificação de registros adicionais deve passar caso novos serviços sejam adicionados")]
    public void AddApplicationServices_RegistroDeServicos_Futuro()
    {
        // Arrange
        var mockService = new Mock<ICalculoImpostoGanhoCapitalService>();
        var services = new ServiceCollection();
        services.AddSingleton(mockService.Object);

        // Act
        IServiceCollection returnedServices = services.AddApplicationServices();
        ServiceProvider provider = returnedServices.BuildServiceProvider();

        // Assert
        IApurarGanhoCapitalUseCase apurarGanhoCapitalService = provider.GetService<IApurarGanhoCapitalUseCase>();
        Assert.NotNull(apurarGanhoCapitalService);
        Assert.IsType<ApurarGanhoCapitalUseCase>(apurarGanhoCapitalService);
    }
}
