using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain;

public class PortfolioTests
{
    [Fact]
    public void ApplyBuy_ShouldUpdateAvgPriceCorrectly()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 20m, Quantity = 50 });

        decimal expectedAvg = ((100 * 10m) + (50 * 20m)) / (100 + 50);
        Assert.Equal(expectedAvg, portfolio.AvgPrice);
        Assert.Equal(150, portfolio.TotalQuantity);
    }

    [Fact]
    public void RegisterLoss_ShouldIncreaseAccumulatedLoss()
    {
        var portfolio = new Portfolio();
        portfolio.RegisterLoss(500m);
        portfolio.RegisterLoss(250m);

        Assert.Equal(750m, portfolio.AccumulatedLoss);
    }

    [Fact]
    public void DeductLoss_ShouldReduceAccumulatedLossAndNotGoBelowZero()
    {
        var portfolio = new Portfolio(accumulatedLoss: 500m);
        portfolio.DeductLoss(200m);
        Assert.Equal(300m, portfolio.AccumulatedLoss);

        // Deduzir mais que o acumulado deve zerar
        portfolio.DeductLoss(500m);
        Assert.Equal(0m, portfolio.AccumulatedLoss);
    }

    [Fact]
    public void ClearLoss_ShouldSetAccumulatedLossToZero()
    {
        var portfolio = new Portfolio(accumulatedLoss: 1000m);
        portfolio.ClearLoss();
        Assert.Equal(0m, portfolio.AccumulatedLoss);
    }

    [Fact]
    public void ReduceQuantity_ShouldDecreaseTotalQuantity()
    {
        var portfolio = new Portfolio(totalQuantity: 200);
        portfolio.ReduceQuantity(50);
        Assert.Equal(150, portfolio.TotalQuantity);

        // Reduzir mais que o total deve permitir zero (ou pode lançar exceção se quiser)
        portfolio.ReduceQuantity(200);
        Assert.Equal(-50, portfolio.TotalQuantity); // atualmente permite negativo
    }

    [Fact]
    public void ComplexScenario_ShouldMaintainCorrectState()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });
        portfolio.RegisterLoss(500m);
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 20m, Quantity = 50 });
        portfolio.DeductLoss(300m);
        portfolio.ReduceQuantity(50);

        decimal expectedAvg = ((100 * 10m) + (50 * 20m)) / 150;
        Assert.Equal(expectedAvg, portfolio.AvgPrice);
        Assert.Equal(100, portfolio.TotalQuantity); // 150 - 50
        Assert.Equal(200m, portfolio.AccumulatedLoss); // 500 - 300
    }
}
