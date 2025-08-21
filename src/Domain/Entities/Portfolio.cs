namespace Domain.Entities;

public class Portfolio
{
    public decimal AvgPrice { get; private set; }
    public int TotalQuantity { get; private set; }
    public decimal AccumulatedLoss { get; private set; }

    public Portfolio(decimal avgPrice = 0, int totalQuantity = 0, decimal accumulatedLoss = 0)
    {
        AvgPrice = avgPrice;
        TotalQuantity = totalQuantity;
        AccumulatedLoss = accumulatedLoss;
    }

    public void ApplyBuy(Operation op)
    {
        AvgPrice = ((TotalQuantity * AvgPrice) + (op.Quantity * op.UnitCost)) / (TotalQuantity + op.Quantity);
        TotalQuantity += op.Quantity;
    }

    public void RegisterLoss(decimal value)
    {
        AccumulatedLoss += value;
    }

    public void DeductLoss(decimal value)
    {
        AccumulatedLoss = Math.Max(0, AccumulatedLoss - value);
    }

    public void ClearLoss()
    {
        AccumulatedLoss = 0;
    }

    public void ReduceQuantity(int q) => TotalQuantity -= q;
}

