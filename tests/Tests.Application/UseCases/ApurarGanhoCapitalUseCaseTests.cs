using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;

namespace Application.Tests.UseCases;

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
    /// Verifica que Execute retorna lista com resultados corretos para operações de compra.
    /// </summary>
    [Fact(DisplayName = "Execute com lista de compras deve retornar valores calculados")]
    public void Execute_ListaCompras_RetornaValoresCalculados()
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao>
        {
            new Operacao(Domain.Enums.TipoOperacaoEnum.Buy, 10m, 5),
            new Operacao(Domain.Enums.TipoOperacaoEnum.Buy, 20m, 10)
        };

        _mockService.Setup(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()))
                    .Returns<Operacao, Portfolio>((op, p) => new ImpostoGanhoCapital(op.PrecoUnitarioAcao * op.QuantidadeAcoesNegociadas * 0.1m));

        // Act
        var resultado = useCase.Execute(operacoes);

        // Assert
        Assert.NotNull(resultado);
        var lista = new List<ImpostoGanhoCapital>(resultado);
        Assert.Equal(2, lista.Count);
        Assert.Equal(5m, lista[0].Valor);  // 10*5*0.1
        Assert.Equal(20m, lista[1].Valor); // 20*10*0.1

        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Exactly(2));
    }

    /// <summary>
    /// Verifica que Execute retorna lista com resultados corretos para operações de venda.
    /// </summary>
    [Fact(DisplayName = "Execute com lista de vendas deve retornar valores calculados")]
    public void Execute_ListaVendas_RetornaValoresCalculados()
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao>
        {
            new Operacao(Domain.Enums.TipoOperacaoEnum.Sell, 15m, 3),
            new Operacao(Domain.Enums.TipoOperacaoEnum.Sell, 25m, 7)
        };

        _mockService.Setup(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()))
                    .Returns<Operacao, Portfolio>((op, p) => new ImpostoGanhoCapital(op.PrecoUnitarioAcao * op.QuantidadeAcoesNegociadas * 0.2m));

        // Act
        var resultado = useCase.Execute(operacoes);

        // Assert
        Assert.NotNull(resultado);
        var lista = new List<ImpostoGanhoCapital>(resultado);
        Assert.Equal(2, lista.Count);
        Assert.Equal(9m, lista[0].Valor);  // 15*3*0.2
        Assert.Equal(35m, lista[1].Valor); // 25*7*0.2

        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Exactly(2));
    }

    /// <summary>
    /// Verifica que Execute retorna lista correta para lista mista de compras e vendas.
    /// </summary>
    [Fact(DisplayName = "Execute com lista mista deve retornar valores calculados corretamente")]
    public void Execute_ListaMista_RetornaValoresCalculados()
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao>
        {
            new Operacao(Domain.Enums.TipoOperacaoEnum.Buy, 10m, 5),
            new Operacao(Domain.Enums.TipoOperacaoEnum.Sell, 20m, 4)
        };

        _mockService.Setup(s => s.Calcular(It.Is<Operacao>(o => o.TipoOperacao == Domain.Enums.TipoOperacaoEnum.Buy), It.IsAny<Portfolio>()))
                    .Returns(new ImpostoGanhoCapital(5m));

        _mockService.Setup(s => s.Calcular(It.Is<Operacao>(o => o.TipoOperacao == Domain.Enums.TipoOperacaoEnum.Sell), It.IsAny<Portfolio>()))
                    .Returns(new ImpostoGanhoCapital(8m));

        // Act
        var resultado = useCase.Execute(operacoes);

        // Assert
        Assert.NotNull(resultado);
        var lista = new List<ImpostoGanhoCapital>(resultado);
        Assert.Equal(2, lista.Count);
        Assert.Equal(5m, lista[0].Valor);
        Assert.Equal(8m, lista[1].Valor);

        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Exactly(2));
    }
}
