using Domain.Entities;

namespace UnitTests.Domain.Entities;

/// <summary>
/// Testes unitários para a classe <see cref="ImpostoGanhoCapital"/>.
/// Cobre criação de instâncias, validação de valor e igualdade entre registros.
/// </summary>
[Collection("ImpostoGanhoCapitalTests Collection")]
public class ImpostoGanhoCapitalTests
{
    /// <summary>
    /// Verifica se a instância é criada corretamente com um valor positivo.
    /// </summary>
    [Fact(DisplayName = "Criação de ImpostoGanhoCapital com valor positivo")]
    public void CriarComValorPositivo_DeveAtribuirValorCorreto()
    {
        // Arrange & Act
        var imposto = new ImpostoGanhoCapital(150.75m);

        // Assert
        Assert.Equal(150.75m, imposto.Valor);
    }

    /// <summary>
    /// Verifica se a instância é criada corretamente com valor zero.
    /// </summary>
    [Fact(DisplayName = "Criação de ImpostoGanhoCapital com valor zero")]
    public void CriarComValorZero_DeveAtribuirValorCorreto()
    {
        // Arrange & Act
        var imposto = new ImpostoGanhoCapital(0m);

        // Assert
        Assert.Equal(0m, imposto.Valor);
    }

    /// <summary>
    /// Verifica se duas instâncias com o mesmo valor são consideradas iguais.
    /// </summary>
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

    /// <summary>
    /// Verifica se duas instâncias com valores diferentes não são consideradas iguais.
    /// </summary>
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
