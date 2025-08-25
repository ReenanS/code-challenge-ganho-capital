using Domain.Entities;

namespace UnitTests.Domain.Entities;

[Collection("ImpostoGanhoCapitalTests Collection")]
public class ImpostoGanhoCapitalTests
{

    [Fact(DisplayName = "Criação de ImpostoGanhoCapital com valor positivo")]
    public void CriarComValorPositivo_DeveAtribuirValorCorreto()
    {
        // Arrange & Act
        var imposto = new ImpostoGanhoCapital(150.75m);

        // Assert
        Assert.Equal(150.75m, imposto.Valor);
    }

    [Fact(DisplayName = "Criação de ImpostoGanhoCapital com valor zero")]
    public void CriarComValorZero_DeveAtribuirValorCorreto()
    {
        // Arrange & Act
        var imposto = new ImpostoGanhoCapital(0m);

        // Assert
        Assert.Equal(0m, imposto.Valor);
    }

    [Fact(DisplayName = "Igualdade entre registros com mesmo valor")]
    public void InstanciasComMesmoValor_SaoIguais()
    {
        // Arrange
        var imposto1 = new ImpostoGanhoCapital(200m);
        var imposto2 = new ImpostoGanhoCapital(200m);

        // Act & Assert
        Assert.Equal(imposto1, imposto2);
        Assert.True(imposto1.Equals(imposto2));
    }

    [Fact(DisplayName = "Registros com valores diferentes não são iguais")]
    public void InstanciasComValoresDiferentes_NaoSaoIguais()
    {
        // Arrange
        var imposto1 = new ImpostoGanhoCapital(100m);
        var imposto2 = new ImpostoGanhoCapital(150m);

        // Act & Assert
        Assert.NotEqual(imposto1, imposto2);
        Assert.False(imposto1.Equals(imposto2));
    }
}
