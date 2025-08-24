using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Interface para serviços que calculam o imposto sobre ganho de capital
/// em operações financeiras de ações.
/// </summary>
public interface ICalculoImpostoGanhoCapitalService
{
    /// <summary>
    /// Calcula o imposto sobre ganho de capital de uma operação, considerando o portfólio do investidor.
    /// </summary>
    /// <param name="operacao">A operação financeira (compra ou venda) a ser processada.</param>
    /// <param name="portfolio">O portfólio do investidor, contendo quantidade de ações, preço médio ponderado e prejuízos acumulados.</param>
    /// <returns>
    /// Um objeto <see cref="ImpostoGanhoCapital"/> representando o valor do imposto calculado.
    /// </returns>
    ImpostoGanhoCapital Calcular(Operacao operacao, Portfolio portfolio);
}
