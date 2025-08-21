using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Operation
{
    [JsonPropertyName("operation")]
    public OperationType OperationType { get; private set; }

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; private set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; private set; }

    // Métodos auxiliares para melhorar legibilidade
    public bool IsBuy() => OperationType == OperationType.Buy;
    public bool IsSell() => OperationType == OperationType.Sell;

}
