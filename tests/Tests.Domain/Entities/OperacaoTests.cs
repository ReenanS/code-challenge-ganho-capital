using Domain.Entities;
using Domain.Enums;

namespace Domain.Tests.Entities;

/// <summary>
/// Testes unitários para a classe <see cref="Operacao"/>.
/// Cobre criação de instâncias, métodos EhCompra e EhVenda, e validação de propriedades.
/// </summary>
[Collection("OperacaoTests Collection")]
public class OperacaoTests
{
    /// <summary>
    /// Verifica se o construtor padrão inicializa a operação com valores padrão.
    /// </summary>
    [Fact(DisplayName = "Construtor padrão deve inicializar propriedades com valores padrão")]
    public void ConstrutorPadrao_DeveInicializarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var operacao = new Operacao();

        // Assert
        Assert.Equal(default(TipoOperacaoEnum), operacao.TipoOperacao);
        Assert.Equal(0m, operacao.PrecoUnitarioAcao);
        Assert.Equal(0, operacao.QuantidadeAcoesNegociadas);
    }

    /// <summary>
    /// Verifica se o construtor parametrizado inicializa corretamente as propriedades.
    /// </summary>
    [Fact(DisplayName = "Construtor parametrizado deve inicializar propriedades corretamente")]
    public void ConstrutorParametrizado_DeveInicializarPropriedadesCorretamente()
    {
        // Arrange
        var tipo = TipoOperacaoEnum.Buy;
        decimal preco = 50.75m;
        int quantidade = 100;

        // Act
        var operacao = new Operacao(tipo, preco, quantidade);

        // Assert
        Assert.Equal(tipo, operacao.TipoOperacao);
        Assert.Equal(preco, operacao.PrecoUnitarioAcao);
        Assert.Equal(quantidade, operacao.QuantidadeAcoesNegociadas);
    }

    /// <summary>
    /// Verifica se o método EhCompra retorna true quando a operação é do tipo Buy.
    /// </summary>
    [Fact(DisplayName = "EhCompra deve retornar true para operação Buy")]
    public void EhCompra_DeveRetornarTrue_ParaBuy()
    {
        // Arrange
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 10m, 1);

        // Act
        var resultado = operacao.EhCompra();

        // Assert
        Assert.True(resultado);
    }

    /// <summary>
    /// Verifica se o método EhCompra retorna false quando a operação não é Buy.
    /// </summary>
    [Fact(DisplayName = "EhCompra deve retornar false para operação Sell")]
    public void EhCompra_DeveRetornarFalse_ParaSell()
    {
        // Arrange
        var operacao = new Operacao(TipoOperacaoEnum.Sell, 10m, 1);

        // Act
        var resultado = operacao.EhCompra();

        // Assert
        Assert.False(resultado);
    }

    /// <summary>
    /// Verifica se o método EhVenda retorna true quando a operação é do tipo Sell.
    /// </summary>
    [Fact(DisplayName = "EhVenda deve retornar true para operação Sell")]
    public void EhVenda_DeveRetornarTrue_ParaSell()
    {
        // Arrange
        var operacao = new Operacao(TipoOperacaoEnum.Sell, 10m, 1);

        // Act
        var resultado = operacao.EhVenda();

        // Assert
        Assert.True(resultado);
    }

    /// <summary>
    /// Verifica se o método EhVenda retorna false quando a operação não é Sell.
    /// </summary>
    [Fact(DisplayName = "EhVenda deve retornar false para operação Buy")]
    public void EhVenda_DeveRetornarFalse_ParaBuy()
    {
        // Arrange
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 10m, 1);

        // Act
        var resultado = operacao.EhVenda();

        // Assert
        Assert.False(resultado);
    }

    /// <summary>
    /// Verifica se os getters e setters das propriedades funcionam corretamente.
    /// </summary>
    [Fact(DisplayName = "Propriedades devem ser setadas e lidas corretamente")]
    public void Propriedades_DevePermitirGetESet()
    {
        // Arrange
        var operacao = new Operacao();
        var tipo = TipoOperacaoEnum.Sell;
        decimal preco = 20.50m;
        int quantidade = 200;

        // Act
        operacao.TipoOperacao = tipo;
        operacao.PrecoUnitarioAcao = preco;
        operacao.QuantidadeAcoesNegociadas = quantidade;

        // Assert
        Assert.Equal(tipo, operacao.TipoOperacao);
        Assert.Equal(preco, operacao.PrecoUnitarioAcao);
        Assert.Equal(quantidade, operacao.QuantidadeAcoesNegociadas);
    }
}
