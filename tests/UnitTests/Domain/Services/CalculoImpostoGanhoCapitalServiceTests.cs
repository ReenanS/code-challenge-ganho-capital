using Domain.Entities;
using Domain.Enums;
using Domain.Services;
using Tests.Fixtures.Domain.Entities;

namespace UnitTests.Domain.Services;

public class CalculoImpostoGanhoCapitalServiceTests
{
    private readonly CalculoImpostoGanhoCapitalService _service;

    public CalculoImpostoGanhoCapitalServiceTests()
    {
        _service = new CalculoImpostoGanhoCapitalService();
    }

    [Fact(DisplayName = "Operação de compra retorna imposto zero")]
    public void Calcular_DeveRetornarZero_QuandoOperacaoEhCompra()
    {
        var operacao = new Operacao(TipoOperacaoEnum.Buy, 10m, 100);
        var portfolio = new Portfolio();

        ImpostoGanhoCapital resultado = _service.Calcular(operacao, portfolio);

        Assert.Equal(0m, resultado.Valor);
        Assert.Equal(100, portfolio.QuantidadeAcoesAtual);
    }

    [Theory(DisplayName = "Operações de venda devem calcular imposto corretamente")]
    [MemberData(nameof(GetCasosOperacoes))]
    public void Calcular_DeveCalcularImposto_CasosDefinidos(List<Operacao> operacoes, decimal[] resultadoEsperado)
    {

        var portfolio = new Portfolio();

        for (int i = 0; i < operacoes.Count; i++)
        {
            ImpostoGanhoCapital resultado = _service.Calcular(operacoes[i], portfolio);

            Assert.Equal(resultadoEsperado[i], resultado.Valor);
        }
    }

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
