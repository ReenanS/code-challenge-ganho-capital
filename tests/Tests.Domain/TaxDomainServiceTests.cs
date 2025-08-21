using Domain.Entities;
using Domain.Enums;
using Domain.Services;

namespace Tests.Domain;

public class TaxDomainServiceTests
{
    private readonly TaxDomainService _service;

    public TaxDomainServiceTests()
    {
        _service = new TaxDomainService();
    }

    [Fact]
    public void Execute_ShouldReturnZero_WhenOperationIsBuy()
    {
        var portfolio = new Portfolio();
        var op = new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 };

        var result = _service.Execute(op, portfolio);

        Assert.Equal(0.00m, result.Tax);
        Assert.Equal(100, portfolio.TotalQuantity);
        Assert.Equal(10m, portfolio.AvgPrice);
    }

    [Fact]
    public void Execute_ShouldThrow_WhenSellQuantityExceedsPortfolio()
    {
        var portfolio = new Portfolio(totalQuantity: 50);
        var op = new Operation { OperationType = OperationTypeEnum.Sell, UnitCost = 10m, Quantity = 100 };

        Assert.Throws<InvalidOperationException>(() => _service.Execute(op, portfolio));
    }

    [Fact]
    public void Execute_ShouldRegisterLoss_WhenProfitIsNegative()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });

        var op = new Operation { OperationType = OperationTypeEnum.Sell, UnitCost = 5m, Quantity = 50 };
        var result = _service.Execute(op, portfolio);

        Assert.Equal(0.00m, result.Tax);
        Assert.Equal(50, portfolio.TotalQuantity);
        Assert.Equal(250m, portfolio.AccumulatedLoss); // (10-5)*50
    }

    [Fact]
    public void Execute_ShouldReturnZero_WhenTotalSaleIsBelowThreshold()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });

        var op = new Operation { OperationType = OperationTypeEnum.Sell, UnitCost = 10m, Quantity = 1000 }; // total < 20000
        var result = _service.Execute(op, portfolio);

        Assert.Equal(0.00m, result.Tax);
    }

    [Fact]
    public void Execute_ShouldDeductLoss_WhenTaxableProfitIsNegative()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });
        portfolio.RegisterLoss(500m);

        var op = new Operation { OperationType = OperationTypeEnum.Sell, UnitCost = 12m, Quantity = 50 };
        var result = _service.Execute(op, portfolio);

        Assert.Equal(0.00m, result.Tax);
        Assert.True(portfolio.AccumulatedLoss < 500m);
    }

    [Fact]
    public void Execute_ShouldReturnCorrectTax_WhenProfitAboveThresholdAndNoLoss()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });

        var op = new Operation { OperationType = OperationTypeEnum.Sell, UnitCost = 20m, Quantity = 50 };
        var result = _service.Execute(op, portfolio);

        var expectedProfit = (20m - 10m) * 50; // 500
        var expectedTax = Math.Round(expectedProfit * 0.20m, 2, MidpointRounding.AwayFromZero);

        Assert.Equal(expectedTax, result.Tax);
        Assert.Equal(50, portfolio.TotalQuantity);
        Assert.Equal(0m, portfolio.AccumulatedLoss);
    }

    [Fact]
    public void Execute_ShouldClearLoss_WhenProfitExceedsAccumulatedLoss()
    {
        var portfolio = new Portfolio();
        portfolio.ApplyBuy(new Operation { OperationType = OperationTypeEnum.Buy, UnitCost = 10m, Quantity = 100 });
        portfolio.RegisterLoss(100m);

        var op = new Operation { OperationType = OperationTypeEnum.Sell, UnitCost = 20m, Quantity = 50 };
        var result = _service.Execute(op, portfolio);

        var taxableProfit = (20m - 10m) * 50 - 100m; // 500-100 = 400
        var expectedTax = Math.Round(taxableProfit * 0.20m, 2, MidpointRounding.AwayFromZero);

        Assert.Equal(expectedTax, result.Tax);
        Assert.Equal(0m, portfolio.AccumulatedLoss);
    }
}
