using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

public class ApurarGanhoCapitalUseCase : IApurarGanhoCapitalUseCase
{
    private readonly ICalculoImpostoGanhoCapitalService _service;

    public ApurarGanhoCapitalUseCase(ICalculoImpostoGanhoCapitalService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public IEnumerable<ImpostoGanhoCapital> Execute(IEnumerable<Operacao> operacoes)
    {

        var portfolio = new Portfolio();

        return operacoes
            .Select(operacao => _service.Calcular(operacao, portfolio))
            .ToList();
    }
}
