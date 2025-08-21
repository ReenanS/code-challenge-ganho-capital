namespace Domain.Entities;

public class Operation
{
    public string OperationType { get; }
    public decimal UnitCost { get; }
    public int Quantity { get; }

    public Operation(string operationType, decimal unitCost, int quantity)
    {
        OperationType = operationType.ToLowerInvariant();
        UnitCost = unitCost;
        Quantity = quantity;
    }

    public bool IsBuy => OperationType == "buy";
    public bool IsSell => OperationType == "sell";
}
