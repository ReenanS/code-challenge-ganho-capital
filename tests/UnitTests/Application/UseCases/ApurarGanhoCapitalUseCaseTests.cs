using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Moq;

namespace UnitTests.Application.UseCases;

[Collection("ApurarGanhoCapitalUseCaseTests Collection")]
public class ApurarGanhoCapitalUseCaseTests
{
    private readonly Mock<ICalculoImpostoGanhoCapitalService> _mockService;

    public ApurarGanhoCapitalUseCaseTests()
    {
        _mockService = new Mock<ICalculoImpostoGanhoCapitalService>();
    }

    [Fact(DisplayName = "Execute com lista vazia deve retornar lista vazia")]
    public void Execute_ListaVazia_RetornaListaVazia()
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao>();

        // Act
        IEnumerable<ImpostoGanhoCapital> resultado = useCase.Execute(operacoes);

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Never);
    }

    [Theory(DisplayName = "Execute com operações de diferentes tipos deve retornar valores corretos")]
    [InlineData(TipoOperacaoEnum.Buy, 10, 5, 50)]
    [InlineData(TipoOperacaoEnum.Sell, 15, 3, 45)]
    public void Execute_OperacoesDiversas_RetornaValoresCalculados(TipoOperacaoEnum tipo, decimal preco, int quantidade, decimal valorEsperado)
    {
        // Arrange
        var useCase = new ApurarGanhoCapitalUseCase(_mockService.Object);
        var operacoes = new List<Operacao> { new Operacao(tipo, preco, quantidade) };

        _mockService.Setup(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()))
                    .Returns<Operacao, Portfolio>((op, p) => new ImpostoGanhoCapital(preco * quantidade));

        // Act
        IEnumerable<ImpostoGanhoCapital> resultado = useCase.Execute(operacoes);
        var lista = new List<ImpostoGanhoCapital>(resultado);

        // Assert
        Assert.Single(lista);
        Assert.Equal(valorEsperado, lista[0].Valor);
        _mockService.Verify(s => s.Calcular(It.IsAny<Operacao>(), It.IsAny<Portfolio>()), Times.Once);
    }
}
