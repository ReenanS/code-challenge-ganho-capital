using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain;

public class OperacaoTests
{
    [Fact]
    public void IsBuy_ShouldReturnTrue_WhenOperationIsBuy()
    {
        var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Buy };
        Assert.True(operacao.EhCompra());
        Assert.False(operacao.EhVenda());
    }

    [Fact]
    public void IsSell_ShouldReturnTrue_WhenOperationIsSell()
    {
        var operacao = new Operacao { TipoOperacao = TipoOperacaoEnum.Sell };
        Assert.True(operacao.EhVenda());
        Assert.False(operacao.EhCompra());
    }

    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        var operacao = new Operacao
        {
            TipoOperacao = TipoOperacaoEnum.Buy,
            PrecoUnitarioAcao = 15.5m,
            Quantidade = 100
        };

        Assert.Equal(TipoOperacaoEnum.Buy, operacao.TipoOperacao);
        Assert.Equal(15.5m, operacao.PrecoUnitarioAcao);
        Assert.Equal(100, operacao.Quantidade);
    }
}
