using System.Text.Json.Serialization;

namespace Domain.Entities;

/// <summary>
/// Representa o valor do imposto sobre ganho de capital de uma operação financeira.
/// </summary>
/// <remarks>
/// Esta classe é um <see cref="record"/> imutável que encapsula o valor do imposto calculado.
/// </remarks>
public record ImpostoGanhoCapital(
    [property: JsonPropertyName("tax")] decimal Valor
);
