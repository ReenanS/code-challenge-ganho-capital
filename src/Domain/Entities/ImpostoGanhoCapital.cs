namespace Domain.Entities;

/// <summary>
/// Representa o valor do imposto sobre ganho de capital de uma operação financeira.
/// </summary>
/// <remarks>
/// Esta classe é um <see cref="record"/> imutável que encapsula o valor do imposto calculado.
/// </remarks>
/// <param name="Valor">O valor do imposto a ser pago sobre o ganho de capital.</param>
public record ImpostoGanhoCapital(decimal Valor);