using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main()
    {
        string? line;
        while ((line = Console.ReadLine()) != null)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            var operations = JsonSerializer.Deserialize<List<Operation>>(line, options);

            if (operations is null) continue;

            var useCase = new CalculateTaxesUseCase(new TaxDomainService());
            var results = useCase.Execute(operations)
                .Select(r => new { tax = r.Tax });

            Console.WriteLine(JsonSerializer.Serialize(results));
        }
    }
}