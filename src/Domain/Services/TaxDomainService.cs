using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public class TaxDomainService
{
    public TaxResult Execute(Operation op, Portfolio portfolio)
    {
        if (op.IsBuy())
        {
            //Console.WriteLine("Essa operação é de COMPRA.");
            portfolio.ApplyBuy(op);
            return new TaxResult(0.00m);
        }

        // SELL
        //Console.WriteLine("Essa operação é de VENDA.");

        if (op.Quantity > portfolio.TotalQuantity)
            throw new InvalidOperationException("Quantidade vendida maior que a disponível.");

        var totalSale = op.UnitCost * op.Quantity;
        var profit = (op.UnitCost - portfolio.AvgPrice) * op.Quantity;

        portfolio.ReduceQuantity(op.Quantity);

        if (profit < 0)
        {
            portfolio.RegisterLoss(Math.Abs(profit));
            return new TaxResult(0.00m);
        }

        if (totalSale <= 20000.00m)
            return new TaxResult(0.00m);

        var taxableProfit = profit - portfolio.AccumulatedLoss;
        if (taxableProfit <= 0)
        {
            portfolio.DeductLoss(profit);
            return new TaxResult(0.00m);
        }

        portfolio.ClearLoss();
        return new TaxResult(Math.Round(taxableProfit * 0.20m, 2, MidpointRounding.AwayFromZero));
    }
}
