using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Operation
{
    [JsonPropertyName("operation")]
    public OperationTypeEnum OperationType { get; set; }

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    public Operation() { } // Construtor padrão necessário para deserialização

    // Construtor adicional para inicialização rápida
    public Operation(OperationTypeEnum type, decimal unitCost, int quantity)
    {
        OperationType = type;
        UnitCost = unitCost;
        Quantity = quantity;
    }

    // Métodos auxiliares para melhorar legibilidade
    public bool IsBuy() => OperationType == OperationTypeEnum.Buy;
    public bool IsSell() => OperationType == OperationTypeEnum.Sell;

}
