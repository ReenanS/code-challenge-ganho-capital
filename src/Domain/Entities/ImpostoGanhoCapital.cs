using System.Text.Json.Serialization;

namespace Domain.Entities;

public abstract record ResultadoOperacao;

public record ImpostoGanhoCapital(
    [property: JsonPropertyName("tax")] decimal Valor
) : ResultadoOperacao;

public record ErroOperacao(
    [property: JsonPropertyName("error")] string Mensagem
) : ResultadoOperacao;
