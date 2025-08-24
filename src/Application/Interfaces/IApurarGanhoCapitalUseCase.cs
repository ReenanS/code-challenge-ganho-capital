using Domain.Entities;

namespace Application.Interfaces;

/// <summary>
/// Interface para o caso de uso de apuração de ganho de capital.
/// </summary>
public interface IApurarGanhoCapitalUseCase
{
    /// <summary>
    /// Executa o cálculo do imposto sobre ganho de capital para as operações informadas.
    /// </summary>
    /// <param name="operacoes">Lista de operações financeiras (compra, venda).</param>
    /// <returns>
    /// Uma lista de <see cref="ImpostoGanhoCapital"/> representando o valor do imposto calculado
    /// para cada operação.
    /// </returns>
    IEnumerable<ImpostoGanhoCapital> Execute(IEnumerable<Operacao> operacoes);
}