using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Domain.Entities;

namespace CLI;

public class GanhoCapitalCLI
{
    private readonly IApurarGanhoCapitalUseCase _apurarGanhoCapitalUseCase;

    public GanhoCapitalCLI(IApurarGanhoCapitalUseCase apurarGanhoCapitalUseCase)
    {
        _apurarGanhoCapitalUseCase = apurarGanhoCapitalUseCase ?? throw new ArgumentNullException(nameof(apurarGanhoCapitalUseCase));
    }

    public void RunCLI()
    {
        Console.WriteLine("Seja bem-vindo ao Code Challenge: Ganho de Capital\n");
        Console.WriteLine("Para encerrar o programa pressione CTRL + C\n");
        Console.WriteLine("Digite suas operações em formato JSON abaixo:\n");

        string? line;
        while ((line = Console.ReadLine()) != null)
        {
            line = line.Trim();

            List<Operacao>? operacoes;

            try
            {
                operacoes = JsonSerializer.Deserialize<List<Operacao>>(line, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                });
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON inválido: {ex.Message}");
                continue;
            }

            try
            {
                IEnumerable<ImpostoGanhoCapital> resultado = _apurarGanhoCapitalUseCase.Execute(operacoes!);

                string res = JsonSerializer.Serialize(resultado, new JsonSerializerOptions
                {
                    WriteIndented = false
                });

                Console.WriteLine("\n");
                Console.WriteLine(res);
                Console.WriteLine("\n");
                Console.WriteLine("Digite a sua próxima operação em formato JSON:\n");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao executar o caso de uso: {ex.Message}");
            }
        }
    }
}
