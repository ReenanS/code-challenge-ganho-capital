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

            var operacoes = JsonSerializer.Deserialize<List<Operacao>>(line, options);

            if (operacoes is null) continue;

            var useCase = new ApurarGanhoCapitalUseCase(new CalculoImpostoGanhoCapitalService());

            var results = useCase.Execute(operacoes)
                .Select(r => new { tax = r.Valor });

            Console.WriteLine(JsonSerializer.Serialize(results));
        }
    }
}