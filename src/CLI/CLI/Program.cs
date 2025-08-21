using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string? line;
        while ((line = Console.ReadLine()) != null)
        {
            var operations = JsonSerializer.Deserialize<List<Operation>>(line,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (operations is null) continue;

            var useCase = new CalculateTaxesUseCase(new TaxDomainService());
            var results = useCase.Execute(operations)
                .Select(r => new { tax = r.Tax });

            Console.WriteLine(JsonSerializer.Serialize(results));
        }
    }
}