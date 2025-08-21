using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain;

public class OperationTests
{
    [Fact]
    public void IsBuy_ShouldReturnTrue_WhenOperationIsBuy()
    {
        var op = new Operation { OperationType = OperationTypeEnum.Buy };
        Assert.True(op.IsBuy());
        Assert.False(op.IsSell());
    }

    [Fact]
    public void IsSell_ShouldReturnTrue_WhenOperationIsSell()
    {
        var op = new Operation { OperationType = OperationTypeEnum.Sell };
        Assert.True(op.IsSell());
        Assert.False(op.IsBuy());
    }

    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        var op = new Operation
        {
            OperationType = OperationTypeEnum.Buy,
            UnitCost = 15.5m,
            Quantity = 100
        };

        Assert.Equal(OperationTypeEnum.Buy, op.OperationType);
        Assert.Equal(15.5m, op.UnitCost);
        Assert.Equal(100, op.Quantity);
    }
}
