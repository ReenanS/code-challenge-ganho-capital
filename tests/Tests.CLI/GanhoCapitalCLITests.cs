using System.Text.Json;
using Application.Interfaces;
using CLI;
using Domain.Entities;
using Moq;

namespace Tests.CLI;

/// <summary>
/// Testes unitários para o caso de uso <see cref="GanhoCapitalCLI"/>.
/// Cobre construtor, execução de CLI com JSON válido e inválido, e tratamento de exceções.
/// </summary>
[Collection("GanhoCapitalCLITests Collection")]
public class GanhoCapitalCLITests
{
    private readonly Mock<IApurarGanhoCapitalUseCase> _mockUseCase;
    private readonly GanhoCapitalCLI _cli;
    private readonly StringWriter _consoleOutput;
    private readonly StringWriter _consoleError;

    public GanhoCapitalCLITests()
    {
        _mockUseCase = new Mock<IApurarGanhoCapitalUseCase>();
        _cli = new GanhoCapitalCLI(_mockUseCase.Object);

        _consoleOutput = new StringWriter();
        _consoleError = new StringWriter();
        Console.SetOut(_consoleOutput);
        Console.SetError(_consoleError);
    }

    /// <summary>
    /// Testa se RunCLI processa JSON válido corretamente e imprime o resultado.
    /// </summary>
    [Fact(DisplayName = "RunCLI deve processar JSON válido e imprimir resultado")]
    public void RunCLI_ComJsonValido_DeveExecutarUseCaseEImprimirResultado()
    {
        // Arrange
        var operacoes = new List<Operacao>
        {
            new Operacao(Domain.Enums.TipoOperacaoEnum.Buy, 10m, 5),
            new Operacao(Domain.Enums.TipoOperacaoEnum.Sell, 20m, 4)
        };

        _mockUseCase.Setup(x => x.Execute(It.IsAny<List<Operacao>>()))
                    .Returns(new List<ImpostoGanhoCapital>
                    {
                        new ImpostoGanhoCapital(100.0m)
                    });

        string json = JsonSerializer.Serialize(operacoes);
        Console.SetIn(new StringReader(json + Environment.NewLine + null));

        // Act
        _cli.RunCLI();

        // Assert
        _mockUseCase.Verify(x => x.Execute(It.IsAny<List<Operacao>>()), Times.Once);
        string output = _consoleOutput.ToString();

        Assert.Contains("Seja bem-vindo ao Code Challenge: Ganho de Capital", output);
        Assert.Contains("Para encerrar o programa pressione CTRL + C", output);
        Assert.Contains("Digite suas operações em formato JSON abaixo:", output);
        Assert.Contains("[{\"tax\":100.0}]", output);
        Assert.Contains("Digite a sua próxima operação em formato JSON:", output);
    }

    /// <summary>
    /// Testa se RunCLI captura JSON inválido e escreve mensagem de erro no Console.Error.
    /// </summary>
    [Fact(DisplayName = "RunCLI deve tratar JSON inválido")]
    public void RunCLI_ComJsonInvalido_DeveEscreverErro()
    {
        // Arrange
        string jsonInvalido = "{ \"campo\": }"; // JSON inválido
        Console.SetIn(new StringReader(jsonInvalido + Environment.NewLine + null));

        // Act
        _cli.RunCLI();

        // Assert
        string errorOutput = _consoleError.ToString();
        Assert.Contains("JSON inválido", errorOutput);
    }

    /// <summary>
    /// Testa se RunCLI captura exceções do use case e escreve mensagem de erro no Console.Error.
    /// </summary>
    [Fact(DisplayName = "RunCLI deve tratar exceção do use case")]
    public void RunCLI_UseCaseLancaExcecao_DeveEscreverErro()
    {
        // Arrange
        var operacoes = new List<Operacao> { new Operacao() };
        _mockUseCase.Setup(x => x.Execute(It.IsAny<List<Operacao>>()))
                    .Throws(new Exception("Erro simulado"));

        string json = JsonSerializer.Serialize(operacoes);
        Console.SetIn(new StringReader(json + Environment.NewLine + null));

        // Act
        _cli.RunCLI();

        // Assert
        string errorOutput = _consoleError.ToString();
        Assert.Contains("Erro ao executar o caso de uso", errorOutput);
    }
}
