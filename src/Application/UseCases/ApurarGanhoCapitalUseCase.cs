using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

/// <summary>
/// Caso de uso responsável por apurar o imposto sobre ganho de capital
/// para uma lista de operações financeiras.
/// </summary>
public class ApurarGanhoCapitalUseCase : IApurarGanhoCapitalUseCase
{
    private readonly ICalculoImpostoGanhoCapitalService _service;

    public ApurarGanhoCapitalUseCase(ICalculoImpostoGanhoCapitalService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Apura o imposto sobre ganho de capital para as operações,
    /// mantendo o histórico de um portfólio em memória.
    /// </summary>
    /// <param name="operacoes">Coleção de operações financeiras (compra, venda).</param>
    /// <returns>
    /// Uma coleção de <see cref="ImpostoGanhoCapital"/> com os valores calculados para cada operação.
    /// </returns>
    /// <exception cref="ArgumentNullException">Se <paramref name="operacoes"/> for nulo.</exception>
    public IEnumerable<ImpostoGanhoCapital> Execute(IEnumerable<Operacao> operacoes)
    {
        if (operacoes is null)
        {
            throw new ArgumentNullException(nameof(operacoes));
        }

        var portfolio = new Portfolio();

        return operacoes
            .Select(operacao => _service.Calcular(operacao, portfolio))
            .ToList();
    }
}
