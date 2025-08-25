using Domain.Entities;
using Domain.Enums;

namespace UnitTests.Domain.Entities;

[Collection("PortfolioTests Collection")]
public class PortfolioTests
{

    [Fact(DisplayName = "Construtor padrão deve inicializar propriedades com valores zerados")]
    public void ConstrutorPadrao_DeveInicializarPropriedadesComValoresZerados()
    {
        var portfolio = new Portfolio();

        Assert.Equal(0m, portfolio.PrecoMedioPonderado);
        Assert.Equal(0, portfolio.QuantidadeAcoesAtual);
        Assert.Equal(0m, portfolio.PrejuizoAcumulado);
    }

    [Fact(DisplayName = "Construtor parametrizado deve inicializar propriedades corretamente")]
    public void ConstrutorParametrizado_DeveInicializarPropriedadesCorretamente()
    {
        var portfolio = new Portfolio(10.5m, 100, 50m);

        Assert.Equal(10.5m, portfolio.PrecoMedioPonderado);
        Assert.Equal(100, portfolio.QuantidadeAcoesAtual);
        Assert.Equal(50m, portfolio.PrejuizoAcumulado);
    }

    [Fact(DisplayName = "AplicarCompra deve ajustar PrecoMedioPonderado e QuantidadeAcoesAtual corretamente")]
    public void AplicarCompra_DeveCalcularPrecoMedioPonderadoECrescerQuantidade()
    {
        var portfolio = new Portfolio(20m, 10, 0);
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 30m, 5);

        portfolio.AplicarCompra(operacao);

        Assert.Equal(23.33m, Math.Round(portfolio.PrecoMedioPonderado, 2));
        Assert.Equal(15, portfolio.QuantidadeAcoesAtual);
    }

    [Fact(DisplayName = "RegistrarPrejuizo deve incrementar PrejuizoAcumulado")]
    public void RegistrarPrejuizo_DeveIncrementarPrejuizoAcumulado()
    {
        var portfolio = new Portfolio();
        portfolio.RegistrarPrejuizo(50m);
        portfolio.RegistrarPrejuizo(25m);

        Assert.Equal(75m, portfolio.PrejuizoAcumulado);
    }

    [Fact(DisplayName = "DeduzirPrejuizo não deve permitir PrejuizoAcumulado negativo")]
    public void DeduzirPrejuizo_DeveDiminuirPrejuizoAcumuladoSemNegativo()
    {
        var portfolio = new Portfolio();
        portfolio.RegistrarPrejuizo(100m);

        portfolio.DeduzirPrejuizo(40m);
        Assert.Equal(60m, portfolio.PrejuizoAcumulado);

        portfolio.DeduzirPrejuizo(100m);
        Assert.Equal(0m, portfolio.PrejuizoAcumulado);
    }

    [Fact(DisplayName = "ReduzirQuantidade deve diminuir QuantidadeAcoesAtual")]
    public void ReduzirQuantidade_DeveDiminuirQuantidadeDeAcoes()
    {
        var portfolio = new Portfolio(0, 100, 0);

        portfolio.ReduzirQuantidade(30);

        Assert.Equal(70, portfolio.QuantidadeAcoesAtual);
    }

}
