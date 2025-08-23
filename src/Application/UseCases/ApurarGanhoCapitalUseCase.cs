using Domain.Entities;
using Domain.Services;

namespace Application.UseCases;

public class ApurarGanhoCapitalUseCase
{
    private readonly ICalculoImpostoGanhoCapitalService _service;

    public ApurarGanhoCapitalUseCase(ICalculoImpostoGanhoCapitalService service)
    {
        _service = service;
    }

    public IEnumerable<ImpostoGanhoCapital> Execute(IEnumerable<Operacao> operacoes)
    {
        var results = new List<ImpostoGanhoCapital>();
        var portfolio = new Portfolio();

        foreach (var operacao in operacoes)
        {
            results.Add(_service.Calcular(operacao, portfolio));
        }

        return results;
    }
}