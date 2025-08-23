using Domain.Enums;

namespace Domain.Tests.Enums;

/// <summary>
/// Testes unitários para o enum <see cref="TipoOperacaoEnum"/>.
/// Cobre validação de valores, conversões e igualdade.
/// </summary>
[Collection("TipoOperacaoEnumTests Collection")]
public class TipoOperacaoEnumTests
{
    /// <summary>
    /// Verifica se o enum contém o valor Buy com o valor inteiro correto.
    /// </summary>
    [Fact(DisplayName = "Enum deve conter valor Buy")]
    public void Enum_ConterValorBuy()
    {
        // Arrange & Act
        var valor = TipoOperacaoEnum.Buy;

        // Assert
        Assert.Equal(0, (int)valor); // Buy deve ser 0
    }

    /// <summary>
    /// Verifica se o enum contém o valor Sell com o valor inteiro correto.
    /// </summary>
    [Fact(DisplayName = "Enum deve conter valor Sell")]
    public void Enum_ConterValorSell()
    {
        // Arrange & Act
        var valor = TipoOperacaoEnum.Sell;

        // Assert
        Assert.Equal(1, (int)valor); // Sell deve ser 1
    }

    /// <summary>
    /// Testa a conversão de string para enum com valor válido Buy.
    /// </summary>
    [Fact(DisplayName = "Conversão de string 'Buy' para enum deve funcionar corretamente")]
    public void ConversaoStringParaEnum_Buy_DeveFuncionar()
    {
        // Arrange
        var valorStr = "Buy";

        // Act
        var valorEnum = Enum.Parse<TipoOperacaoEnum>(valorStr);

        // Assert
        Assert.Equal(TipoOperacaoEnum.Buy, valorEnum);
    }

    /// <summary>
    /// Testa a conversão de string para enum com valor válido Sell.
    /// </summary>
    [Fact(DisplayName = "Conversão de string 'Sell' para enum deve funcionar corretamente")]
    public void ConversaoStringParaEnum_Sell_DeveFuncionar()
    {
        // Arrange
        var valorStr = "Sell";

        // Act
        var valorEnum = Enum.Parse<TipoOperacaoEnum>(valorStr);

        // Assert
        Assert.Equal(TipoOperacaoEnum.Sell, valorEnum);
    }

    /// <summary>
    /// Testa a conversão de enum para string com valor Buy.
    /// </summary>
    [Fact(DisplayName = "Conversão de enum Buy para string deve retornar 'Buy'")]
    public void ConversaoEnumParaString_Buy_DeveFuncionar()
    {
        // Arrange
        var valorEnum = TipoOperacaoEnum.Buy;

        // Act
        var valorStr = valorEnum.ToString();

        // Assert
        Assert.Equal("Buy", valorStr);
    }

    /// <summary>
    /// Testa a conversão de enum para string com valor Sell.
    /// </summary>
    [Fact(DisplayName = "Conversão de enum Sell para string deve retornar 'Sell'")]
    public void ConversaoEnumParaString_Sell_DeveFuncionar()
    {
        // Arrange
        var valorEnum = TipoOperacaoEnum.Sell;

        // Act
        var valorStr = valorEnum.ToString();

        // Assert
        Assert.Equal("Sell", valorStr);
    }

}
