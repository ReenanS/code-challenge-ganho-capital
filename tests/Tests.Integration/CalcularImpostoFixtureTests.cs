using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Tests.Integration.Fixtures;

namespace Tests.Integration;

public class CalcularImpostoFixtureTests : IClassFixture<OperacoesFixture>
{
    private readonly OperacoesFixture _fixture;
    private readonly CalcularImpostoUseCase _useCase;

    public CalcularImpostoFixtureTests(OperacoesFixture fixture)
    {
        _fixture = fixture;
        _useCase = new CalcularImpostoUseCase(new ImpostoService());
    }

    private void ValidarImposto(List<Operacao> operacoes, decimal[] expected)
    {
        // Converte o IEnumerable<ImpostoPago> retornado para List<ImpostoPago>
        var taxes = new List<ImpostoPago>(_useCase.Execute(operacoes));

        // Verifica a quantidadeAcoesNegociadas de elementos
        Assert.Equal(expected.Length, taxes.Count);

        // Verifica os valores de imposto
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], taxes[i].Imposto);
        }
    }

    [Fact] public void Test_Caso1() => ValidarImposto(_fixture.Caso1, _fixture.Caso1Expected);
    [Fact] public void Test_Caso2() => ValidarImposto(_fixture.Caso2, _fixture.Caso2Expected);
    [Fact] public void Test_Caso3() => ValidarImposto(_fixture.Caso3, _fixture.Caso3Expected);
    [Fact] public void Test_Caso4() => ValidarImposto(_fixture.Caso4, _fixture.Caso4Expected);
    [Fact] public void Test_Caso5() => ValidarImposto(_fixture.Caso5, _fixture.Caso5Expected);
    [Fact] public void Test_Caso6() => ValidarImposto(_fixture.Caso6, _fixture.Caso6Expected);
    [Fact] public void Test_Caso7() => ValidarImposto(_fixture.Caso7, _fixture.Caso7Expected);
    [Fact] public void Test_Caso8() => ValidarImposto(_fixture.Caso8, _fixture.Caso8Expected);
    [Fact] public void Test_Caso9() => ValidarImposto(_fixture.Caso9, _fixture.Caso9Expected);
}