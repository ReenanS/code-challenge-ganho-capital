using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain;

public class PortfolioTests
{
    [Fact]
    public void ApplyBuy_ShouldUpdateAvgPriceCorrectly()
    {
        var portfolio = new Portfolio();
        portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });
        portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 20m, QuantidadeAcoesNegociadas = 50 });

        decimal expectedAvg = ((100 * 10m) + (50 * 20m)) / (100 + 50);
        Assert.Equal(expectedAvg, portfolio.PrecoMedioPonderado);
        Assert.Equal(150, portfolio.QuantidadeAcoesAtual);
    }

    [Fact]
    public void RegisterLoss_ShouldIncreaseAccumulatedLoss()
    {
        var portfolio = new Portfolio();
        portfolio.RegistrarPrejuizo(500m);
        portfolio.RegistrarPrejuizo(250m);

        Assert.Equal(750m, portfolio.PrejuizoAcumulado );
    }

    [Fact]
    public void DeductLoss_ShouldReduceAccumulatedLossAndNotGoBelowZero()
    {
        var portfolio = new Portfolio(prejuizoAcumulado: 500m);
        portfolio.DeduzirPrejuizo(200m);
        Assert.Equal(300m, portfolio.PrejuizoAcumulado );

        // Deduzir mais que o acumulado deve zerar
        portfolio.DeduzirPrejuizo(500m);
        Assert.Equal(0m, portfolio.PrejuizoAcumulado );
    }

    [Fact]
    public void ClearLoss_ShouldSetAccumulatedLossToZero()
    {
        var portfolio = new Portfolio(prejuizoAcumulado: 1000m);
        portfolio.LimparPrejuizo();
        Assert.Equal(0m, portfolio.PrejuizoAcumulado );
    }

    [Fact]
    public void ReduceQuantity_ShouldDecreaseTotalQuantity()
    {
        var portfolio = new Portfolio(quantidadeAcoesAtual: 200);
        portfolio.ReduzirQuantidadeAcoes(50);
        Assert.Equal(150, portfolio.QuantidadeAcoesAtual);

        // Reduzir mais que o total deve permitir zero (ou pode lançar exceção se quiser)
        portfolio.ReduzirQuantidadeAcoes(200);
        Assert.Equal(-50, portfolio.QuantidadeAcoesAtual); // atualmente permite negativo
    }

    [Fact]
    public void ComplexScenario_ShouldMaintainCorrectState()
    {
        var portfolio = new Portfolio();
        portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 10m, QuantidadeAcoesNegociadas = 100 });
        portfolio.RegistrarPrejuizo(500m);
        portfolio.AplicarCompra(new Operacao { TipoOperacao = TipoOperacaoEnum.Buy, PrecoUnitarioAcao = 20m, QuantidadeAcoesNegociadas = 50 });
        portfolio.DeduzirPrejuizo(300m);
        portfolio.ReduzirQuantidadeAcoes(50);

        decimal expectedAvg = ((100 * 10m) + (50 * 20m)) / 150;
        Assert.Equal(expectedAvg, portfolio.PrecoMedioPonderado);
        Assert.Equal(100, portfolio.QuantidadeAcoesAtual); // 150 - 50
        Assert.Equal(200m, portfolio.PrejuizoAcumulado ); // 500 - 300
    }
}
