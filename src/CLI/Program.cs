using Application;
using Application.UseCases;
using Domain;
using Domain.Entities;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // 1️⃣ Configurar container de DI
        var services = new ServiceCollection();

        // Registrar dependências por camada
        services.AddDomainServices()          // Domain Layer
                .AddApplicationServices()     // Application Layer
                .AddInfrastructureServices(); // Infrastructure Layer

        var serviceProvider = services.BuildServiceProvider();

        // Recuperar JsonSerializerOptions da Infra
        var jsonOptions = serviceProvider.GetRequiredService<JsonSerializerOptions>();

        // 2️⃣ Loop de leitura da CLI
        string? line;
        while ((line = Console.ReadLine()) != null)
        {
            try
            {
                var operacoes = JsonSerializer.Deserialize<List<Operacao>>(line, jsonOptions);
                if (operacoes is null) continue;

                // 3️⃣ Recuperar UseCase via DI
                var useCase = serviceProvider.GetRequiredService<ApurarGanhoCapitalUseCase>();

                // 4️⃣ Executar o UseCase
                var results = useCase.Execute(operacoes)
                                     .Select(r => new { tax = r.Valor });

                // 5️⃣ Escrever resultado
                Console.WriteLine(JsonSerializer.Serialize(results, jsonOptions));
            }
            catch (Exception ex)
            {
                // Tratamento simples de erros (opcional para logs ou feedback)
                Console.Error.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}