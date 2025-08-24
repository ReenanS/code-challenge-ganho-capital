using Domain;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Domain;

/// <summary>
/// Testes unitários para a classe de extensão <see cref="DependencyInjection"/>.
/// Cobre registro de serviços no container de DI.
/// </summary>
[Collection("DependencyInjectionTests Collection")]
public class DependencyInjectionTests
{

    /// <summary>
    /// Verifica que todos os serviços esperados estão registrados corretamente.
    /// </summary>
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
