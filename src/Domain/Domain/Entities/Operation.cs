using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Operation
{
    [JsonPropertyName("operation")]
    public OperationTypeEnum OperationType { get; private set; }

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    // Métodos auxiliares para melhorar legibilidade
    public bool IsBuy() => OperationType == OperationTypeEnum.Buy;
    public bool IsSell() => OperationType == OperationTypeEnum.Sell;

}
