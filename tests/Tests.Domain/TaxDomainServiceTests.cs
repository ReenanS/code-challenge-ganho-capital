using Domain.Entities;
using Domain.Enums;
using Domain.Services;

namespace Tests.Domain;

public class TaxDomainServiceTests
{
    private readonly CalculoImpostoGanhoCapitalService _service;

    public TaxDomainServiceTests()
    {
        _service = new CalculoImpostoGanhoCapitalService();
    }

    [Fact]
    public void Execute_ShouldReturnZero_WhenOperationIsBuy()
    {
        var portfolio = new Portfolio();
        var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 };

        var result = _service.Execute(operacao, portfolio);

        Assert.Equal(0.00m, result.Valor);
        Assert.Equal(100, portfolio.QuantidadeAcoesAtual);
        Assert.Equal(10m, portfolio.PrecoMedioPonderado);
    }

    [Fact]
    public void Execute_ShouldThrow_WhenSellQuantityExceedsPortfolio()
    {
        var portfolio = new Portfolio(quantidadeAcoesAtual: 50);
        var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 };

        Assert.Throws<InvalidOperationException>(() => _service.Execute(operacao, portfolio));
    }

    [Fact]
    public void Execute_ShouldRegisterLoss_WhenProfitIsNegative()
    {
        var portfolio = new Portfolio();
        portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });

        var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell, PrecoUnitarioAcao = 5m, QuantidadeAcoesNegociadas = 50 };
        var result = _service.Execute(operacao, portfolio);

        Assert.Equal(0.00m, result.Valor);
        Assert.Equal(50, portfolio.QuantidadeAcoesAtual);
        Assert.Equal(250m, portfolio.PrejuizoAcumulado); // (10-5)*50
    }

    //[Fact]
    //public void Execute_ShouldReturnZero_WhenTotalSaleIsBelowThreshold()
    //{
    //    var portfolio = new Portfolio();
    //    portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });

    //    var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 1000 }; // total < 20000
    //    var result = _service.Execute(operacao, portfolio);

    //    Assert.Equal(0.00m, result.imposto);
    //}

    //[Fact]
    //public void Execute_ShouldDeductLoss_WhenTaxableProfitIsNegative()
    //{
    //    var portfolio = new Portfolio();
    //    portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });
    //    portfolio.RegistrarPrejuizo(500m);

    //    var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell, PrecoUnitarioAcao = 12m, QuantidadeAcoesNegociadas = 50 };
    //    var result = _service.Execute(operacao, portfolio);

    //    Assert.Equal(0.00m, result.imposto);
    //    Assert.True(portfolio.PrejuizoAcumulado  < 500m);
    //}

    //[Fact]
    //public void Execute_ShouldReturnCorrectTax_WhenProfitAboveThresholdAndNoLoss()
    //{
    //    var portfolio = new Portfolio();
    //    portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });

    //    var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell, PrecoUnitarioAcao = 20m, QuantidadeAcoesNegociadas = 50 };
    //    var result = _service.Execute(operacao, portfolio);

    //    var expectedProfit = (20m - 10m) * 50; // 500
    //    var expectedTax = Math.Round(expectedProfit * 0.20m, 2, MidpointRounding.AwayFromZero);

    //    Assert.Equal(expectedTax, result.imposto);
    //    Assert.Equal(50, portfolio.QuantidadeAcoesAtual);
    //    Assert.Equal(0m, portfolio.PrejuizoAcumulado );
    //}

    //[Fact]
    //public void Execute_ShouldClearLoss_WhenProfitExceedsAccumulatedLoss()
    //{
    //    var portfolio = new Portfolio();
    //    portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });
    //    portfolio.RegistrarPrejuizo(100m);

    //    var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell, PrecoUnitarioAcao = 20m, QuantidadeAcoesNegociadas = 50 };
    //    var result = _service.Execute(operacao, portfolio);

    //    var lucroAcumulado = (20m - 10m) * 50 - 100m; // 500-100 = 400
    //    var expectedTax = Math.Round(lucroAcumulado * 0.20m, 2, MidpointRounding.AwayFromZero);

    //    Assert.Equal(expectedTax, result.imposto);
    //    Assert.Equal(0m, portfolio.PrejuizoAcumulado );
    //}
}
