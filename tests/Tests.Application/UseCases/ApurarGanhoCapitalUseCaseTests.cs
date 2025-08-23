using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CLI.Tests;

/// <summary>
/// Testes unitários para o caso de uso <see cref="ApurarGanhoCapitalUseCase"/>.
/// Cobre execução do método Execute() com diferentes listas de operações e validação de retorno.
/// </summary>
[Collection("ApurarGanhoCapitalUseCaseTests Collection")]
public class ApurarGanhoCapitalUseCaseTests
{
    private readonly Mock<ICalculoImpostoGanhoCapitalService> _mockService;

    public ApurarGanhoCapitalUseCaseTests()
    {
        _mockService = new Mock<ICalculoImpostoGanhoCapitalService>();
    }

    /// <summary>
    /// Verifica que Execute retorna lista vazia quando nenhuma operação é fornecida.
    /// </summary>
    [Fact(DisplayName = "Execute com lista vazia deve retornar lista vazia")]
    public void Execute_ListaVazia_RetornaListaVazia()
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao>();

        // Act
        var resultado = useCase.Execute(operacoes);

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Never);
    }

    /// <summary>
    /// Testa Execute com diferentes tipos de operação (Buy e Sell) e valores calculados.
    /// </summary>
    [Theory(DisplayName = "Execute com operações de diferentes tipos deve retornar valores corretos")]
    [InlineData(Domain.Enums.TipoOperacaoEnum.Buy, 10, 5, 50)]
    [InlineData(Domain.Enums.TipoOperacaoEnum.Sell, 15, 3, 45)]
    public void Execute_OperacoesDiversas_RetornaValoresCalculados(Domain.Enums.TipoOperacaoEnum tipo, decimal preco, int quantidade, decimal valorEsperado)
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao> { new Operacao(tipo, preco, quantidade) };

        _mockService.Setup(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()))
                    .Returns<Operacao, Portfolio>((op, p) => new ImpostoGanhoCapital(preco * quantidade));

        // Act
        var resultado = useCase.Execute(operacoes);
        var lista = new List<ImpostoGanhoCapital>(resultado);

        // Assert
        Assert.Single(lista);
        Assert.Equal(valorEsperado, lista[0].Valor);
        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Once);
    }
}
