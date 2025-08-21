using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Tests.Integration.Fixtures;

namespace Tests.Integration;

public class CalculateTaxesFixtureTests : IClassFixture<OperationsFixture>
{
    private readonly OperationsFixture _fixture;
    private readonly CalculateTaxesUseCase _useCase;

    public CalculateTaxesFixtureTests(OperationsFixture fixture)
    {
        _fixture = fixture;
        _useCase = new CalculateTaxesUseCase(new TaxDomainService());
    }

    private void ValidateTaxes(List<Operation> operations, decimal[] expected)
    {
        // Converte o IEnumerable<TaxResult> retornado para List<TaxResult>
        var taxes = new List<TaxResult>(_useCase.Execute(operations));

        // Verifica a quantidade de elementos
        Assert.Equal(expected.Length, taxes.Count);

        // Verifica os valores de imposto
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], taxes[i].Tax);
        }
    }

    [Fact] public void Test_Caso1() => ValidateTaxes(_fixture.Caso1, _fixture.Caso1Expected);
    [Fact] public void Test_Caso2() => ValidateTaxes(_fixture.Caso2, _fixture.Caso2Expected);
    [Fact] public void Test_Caso3() => ValidateTaxes(_fixture.Caso3, _fixture.Caso3Expected);
    [Fact] public void Test_Caso4() => ValidateTaxes(_fixture.Caso4, _fixture.Caso4Expected);
    [Fact] public void Test_Caso5() => ValidateTaxes(_fixture.Caso5, _fixture.Caso5Expected);
    [Fact] public void Test_Caso6() => ValidateTaxes(_fixture.Caso6, _fixture.Caso6Expected);
    [Fact] public void Test_Caso7() => ValidateTaxes(_fixture.Caso7, _fixture.Caso7Expected);
    [Fact] public void Test_Caso8() => ValidateTaxes(_fixture.Caso8, _fixture.Caso8Expected);
    [Fact] public void Test_Caso9() => ValidateTaxes(_fixture.Caso9, _fixture.Caso9Expected);
}