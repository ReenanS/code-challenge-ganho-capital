using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain.Entities;

/// <summary>
/// Testes unitários enxutos para a classe <see cref="Operacao"/>.
/// Cobre criação de instâncias, métodos EhCompra/EhVenda e validações de domínio.
/// </summary>
[Collection("OperacaoTests Collection")]
public class OperacaoTests
{

    [Fact(DisplayName = "Construtor padrão deve inicializar propriedades com valores padrão")]
    public void ConstrutorPadrao_DeveInicializarPropriedadesComValoresPadrao()
    {
        var operacao = new Operacao();

        Assert.Equal(default, operacao.TipoOperacao);
        Assert.Equal(0m, operacao.PrecoUnitarioAcao);
        Assert.Equal(0, operacao.QuantidadeAcoesNegociadas);
    }

    [Fact(DisplayName = "Construtor parametrizado deve inicializar propriedades corretamente")]
    public void ConstrutorParametrizado_DeveInicializarPropriedadesCorretamente()
    {
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 50.75m, 100);

        Assert.Equal(TipoOperacaoEnum.Buy, operacao.TipoOperacao);
        Assert.Equal(50.75m, operacao.PrecoUnitarioAcao);
        Assert.Equal(100, operacao.QuantidadeAcoesNegociadas);
    }

    [Theory(DisplayName = "EhCompra e EhVenda devem retornar resultado correto conforme TipoOperacao")]
    [InlineData(TipoOperacaoEnum.Buy, true, false)]
    [InlineData(TipoOperacaoEnum.Sell, false, true)]
    public void EhCompraEhVenda_DeveRetornarCorretamente(TipoOperacaoEnum tipo, bool esperaCompra, bool esperaVenda)
    {
        var operacao = new Operacao(tipo, 10m, 1);

        Assert.Equal(esperaCompra, operacao.EhCompra());
        Assert.Equal(esperaVenda, operacao.EhVenda());
    }


    [Theory(DisplayName = "PrecoUnitarioAcao negativo deve lançar ArgumentOutOfRangeException")]
    [InlineData(-0.01)]
    [InlineData(-100)]
    public void PrecoUnitarioAcao_Negativo_DeveLancarExcecao(decimal precoNegativo)
    {
        var operacao = new Operacao();

        ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            operacao.PrecoUnitarioAcao = precoNegativo;
        });

        Assert.Contains("não pode ser menor que 0", ex.Message);
    }

    [Theory(DisplayName = "QuantidadeAcoesNegociadas negativa deve lançar ArgumentOutOfRangeException")]
    [InlineData(-1)]
    [InlineData(-100)]
    public void QuantidadeAcoesNegociadas_Negativa_DeveLancarExcecao(int quantidadeNegativa)
    {
        var operacao = new Operacao();

        ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            operacao.QuantidadeAcoesNegociadas = quantidadeNegativa;
        });

        Assert.Contains("não pode ser menor que 0", ex.Message);
    }

    [Fact(DisplayName = "TipoOperacao inválido deve lançar ArgumentException")]
    public void TipoOperacao_Invalido_DeveLancarExcecao()
    {
        var operacao = new Operacao();
        var valorInvalido = (TipoOperacaoEnum)999;

        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            operacao.TipoOperacao = valorInvalido;
        });

        Assert.Contains("Tipo de operação inválido", ex.Message);
    }

    [Fact(DisplayName = "Construtor parametrizado com valores inválidos deve lançar exceções")]
    public void ConstrutorParametrizado_ValoresInvalidos_DeveLancarExcecao()
    {
        // Tipo inválido
        var tipoInvalido = (TipoOperacaoEnum)999;
        Assert.Throws<ArgumentException>(() => new Operacao(tipoInvalido, 10m, 1));

        // Preço negativo
        Assert.Throws<ArgumentOutOfRangeException>(() => new Operacao(TipoOperacaoEnum.Buy, -1m, 1));

        // Quantidade negativa
        Assert.Throws<ArgumentOutOfRangeException>(() => new Operacao(TipoOperacaoEnum.Buy, 10m, -5));
    }

}
