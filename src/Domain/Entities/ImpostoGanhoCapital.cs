using System.Text.Json.Serialization;

namespace Domain.Entities;

public record ImpostoGanhoCapital(
    [property: JsonPropertyName("tax")] decimal Valor
);
