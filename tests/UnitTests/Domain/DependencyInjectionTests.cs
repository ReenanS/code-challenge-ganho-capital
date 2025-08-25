using Domain;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Domain;

[Collection("DependencyInjectionTests Collection")]
public class DependencyInjectionTests
{

    [Fact(DisplayName = "Verificação de registros adicionais deve passar caso novos serviços sejam adicionados")]
    public void AddDomainServices_RegistroDeServicos_Futuro()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        IServiceCollection returnedServices = services.AddDomainServices();
        ServiceProvider provider = returnedServices.BuildServiceProvider();

        // Assert
        ICalculoImpostoGanhoCapitalService calculoService = provider.GetService<ICalculoImpostoGanhoCapitalService>();
        Assert.NotNull(calculoService);
        Assert.IsType<CalculoImpostoGanhoCapitalService>(calculoService);
    }
}
