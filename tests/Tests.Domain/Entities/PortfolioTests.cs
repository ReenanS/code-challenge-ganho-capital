using Domain.Entities;
using Domain.Enums;

namespace Domain.Tests.Entities;

/// <summary>
/// Testes unitários para a classe <see cref="Portfolio"/>.
/// Cobre criação de instâncias, operações de compra, manipulação de prejuízos e quantidade de ações.
/// </summary>
[Collection("PortfolioTests Collection")]
public class PortfolioTests
{
    /// <summary>
    /// Verifica se o portfólio é criado com valores padrão.
    /// </summary>
    [Fact(DisplayName = "Construtor padrão deve inicializar propriedades com valores zerados")]
    public void ConstrutorPadrao_DeveInicializarPropriedadesComValoresZerados()
    {
        // Arrange & Act
        var portfolio = new Portfolio();

        // Assert
        Assert.Equal(0m, portfolio.PrecoMedioPonderado);
        Assert.Equal(0, portfolio.QuantidadeAcoesAtual);
        Assert.Equal(0m, portfolio.PrejuizoAcumulado);
    }

    /// <summary>
    /// Verifica se o portfólio é criado com valores parametrizados.
    /// </summary>
    [Fact(DisplayName = "Construtor parametrizado deve inicializar propriedades corretamente")]
    public void ConstrutorParametrizado_DeveInicializarPropriedadesCorretamente()
    {
        // Arrange & Act
        var portfolio = new Portfolio(10.5m, 100, 50m);

        // Assert
        Assert.Equal(10.5m, portfolio.PrecoMedioPonderado);
        Assert.Equal(100, portfolio.QuantidadeAcoesAtual);
        Assert.Equal(50m, portfolio.PrejuizoAcumulado);
    }

    /// <summary>
    /// Verifica se AplicarCompra ajusta corretamente o preço médio ponderado e quantidade de ações.
    /// </summary>
    [Fact(DisplayName = "AplicarCompra deve ajustar PrecoMedioPonderado e QuantidadeAcoesAtual corretamente")]
    public void AplicarCompra_DeveCalcularPrecoMedioPonderadoECrescerQuantidade()
    {
        // Arrange
        var portfolio = new Portfolio(20m, 10, 0);
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 30m, 5);

        // Act
        portfolio.AplicarCompra(operacao);

        // Assert
        Assert.Equal(23.33m, Math.Round(portfolio.PrecoMedioPonderado, 2));
        Assert.Equal(15, portfolio.QuantidadeAcoesAtual);
    }

    /// <summary>
    /// Verifica se RegistrarPrejuizo incrementa corretamente o prejuízo acumulado.
    /// </summary>
    [Fact(DisplayName = "RegistrarPrejuizo deve incrementar PrejuizoAcumulado")]
    public void RegistrarPrejuizo_DeveIncrementarPrejuizoAcumulado()
    {
        // Arrange
        var portfolio = new Portfolio();
        portfolio.RegistrarPrejuizo(50m);

        // Act
        portfolio.RegistrarPrejuizo(25m);

        // Assert
        Assert.Equal(75m, portfolio.PrejuizoAcumulado);
    }

    /// <summary>
    /// Verifica se DeduzirPrejuizo decrementa corretamente o prejuízo acumulado sem ficar negativo.
    /// </summary>
    [Fact(DisplayName = "DeduzirPrejuizo não deve permitir PrejuizoAcumulado negativo")]
    public void DeduzirPrejuizo_DeveDiminuirPrejuizoAcumuladoSemNegativo()
    {
        // Arrange
        var portfolio = new Portfolio();
        portfolio.RegistrarPrejuizo(100m);

        // Act
        portfolio.DeduzirPrejuizo(40m);

        // Assert
        Assert.Equal(60m, portfolio.PrejuizoAcumulado);

        // Deduzindo mais do que o acumulado
        portfolio.DeduzirPrejuizo(100m);
        Assert.Equal(0m, portfolio.PrejuizoAcumulado);
    }

    /// <summary>
    /// Verifica se LimparPrejuizo zera corretamente o prejuízo acumulado.
    /// </summary>
    [Fact(DisplayName = "LimparPrejuizo deve zerar PrejuizoAcumulado")]
    public void LimparPrejuizo_DeveZerarPrejuizoAcumulado()
    {
        // Arrange
        var portfolio = new Portfolio();
        portfolio.RegistrarPrejuizo(50m);

        // Act
        portfolio.LimparPrejuizo();

        // Assert
        Assert.Equal(0m, portfolio.PrejuizoAcumulado);
    }

    /// <summary>
    /// Verifica se ReduzirQuantidade diminui corretamente a quantidade de ações.
    /// </summary>
    [Fact(DisplayName = "ReduzirQuantidade deve diminuir QuantidadeAcoesAtual")]
    public void ReduzirQuantidade_DeveDiminuirQuantidadeDeAcoes()
    {
        // Arrange
        var portfolio = new Portfolio(0, 100, 0);

        // Act
        portfolio.ReduzirQuantidade(30);

        // Assert
        Assert.Equal(70, portfolio.QuantidadeAcoesAtual);
    }

    /// <summary>
    /// Verifica comportamento ao aplicar compra com quantidade zero.
    /// </summary>
    [Fact(DisplayName = "AplicarCompra com quantidade zero não altera o portfólio")]
    public void AplicarCompra_ComQuantidadeZero_NaoAlteraPortfolio()
    {
        // Arrange
        var portfolio = new Portfolio(50m, 10, 0);
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 100m, 0);

        // Act
        portfolio.AplicarCompra(operacao);

        // Assert
        Assert.Equal(50m, portfolio.PrecoMedioPonderado);
        Assert.Equal(10, portfolio.QuantidadeAcoesAtual);
    }
}
