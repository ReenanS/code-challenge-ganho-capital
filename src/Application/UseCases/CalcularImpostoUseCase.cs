using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases;

public class CalcularImpostoUseCase
{
    private readonly ImpostoService _service;

    public CalcularImpostoUseCase(ImpostoService service)
    {
        _service = service;
    }

    public IEnumerable<ImpostoPago> Execute(IEnumerable<Operacao> operacoes)
    {
        var results = new List<ImpostoPago>();
        var portfolio = new Portfolio();

        foreach (var operacao in operacoes)
        {
            results.Add(_service.Execute(operacao, portfolio));
        }

        return results;
    }
}