using Domain.Entities;
using Domain.Enums;
using Domain.Services;
using Tests.Fixtures.Domain.Entities;

namespace Domain.Tests.Services;

/// <summary>
/// Testes unitários para a classe <see cref="CalculoImpostoGanhoCapitalService"/> 
/// </summary>
public class CalculoImpostoGanhoCapitalServiceTests
{
    private readonly CalculoImpostoGanhoCapitalService _service;

    public CalculoImpostoGanhoCapitalServiceTests()
    {
        _service = new CalculoImpostoGanhoCapitalService();
    }

    [Fact(DisplayName = "Deve retornar imposto zero quando a operação é de compra")]
    public void Calcular_DeveRetornarZero_QuandoOperacaoEhCompra()
    {
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 10m, 100);

        var portfolio = new Portfolio();

        var resultado = _service.Calcular(operacao, portfolio);

        Assert.Equal(0m, resultado.Valor);
        Assert.Equal(100, portfolio.QuantidadeAcoesAtual);
    }

    [Theory(DisplayName = "Testes de todos os casos de OperacoesFixture")]
    [MemberData(nameof(GetCasosOperacoes))]
    public void Calcular_DeveCalcularImposto_CasosDefinidos(List<Operacao> operacoes, decimal[] resultadoEsperado)
    {

        var portfolio = new Portfolio();

        for (int i = 0; i < operacoes.Count; i++)
        {
            var operacao = operacoes[i];

            var resultado = _service.Calcular(operacao, portfolio);

            Assert.Equal(resultadoEsperado[i], resultado.Valor);
        }
    }

    /// <summary>
    /// Fornece os casos de operações do OperacoesFixture para testes parametrizados.
    /// </summary>
    public static IEnumerable<object[]> GetCasosOperacoes()
    {
        var fixture = new OperacoesFixture();
        yield return new object[] { fixture.Caso1, fixture.Caso1_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso2, fixture.Caso2_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso3, fixture.Caso3_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso4, fixture.Caso4_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso5, fixture.Caso5_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso6, fixture.Caso6_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso7, fixture.Caso7_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso8, fixture.Caso8_ImpostoGanhoCapitalEsperado };
        yield return new object[] { fixture.Caso9, fixture.Caso9_ImpostoGanhoCapitalEsperado };
    }
}
