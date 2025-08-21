using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases;

public class CalculateTaxesUseCase
{
    private readonly TaxDomainService _service;

    public CalculateTaxesUseCase(TaxDomainService service)
    {
        _service = service;
    }

    public IEnumerable<TaxResult> Execute(IEnumerable<Operation> operations)
    {
        var results = new List<TaxResult>();
        var portfolio = new Portfolio();

        foreach (var op in operations)
        {
            results.Add(_service.Execute(op, portfolio));
        }

        return results;
    }
}