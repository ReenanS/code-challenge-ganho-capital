using Domain.Entities;
using Domain.Enums;

namespace Tests.Fixtures.Domain.Entities;

public class OperacoesFixture
{
    // ---------------------- Caso 1 ----------------------
    public List<Operacao> Caso1 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 100),
        new Operacao(TipoOperacaoEnum.Sell, 15.00m, 50),
        new Operacao(TipoOperacaoEnum.Sell, 15.00m, 50)
    };
    public decimal[] Caso1_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 0m };

    // ---------------------- Caso 2 ----------------------
    public List<Operacao> Caso2 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 20.00m, 5000),
        new Operacao(TipoOperacaoEnum.Sell, 5.00m, 5000)
    };
    public decimal[] Caso2_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 10000m, 0m };

    // ---------------------- Caso 3 ----------------------
    public List<Operacao> Caso3 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 5.00m, 5000),
        new Operacao(TipoOperacaoEnum.Sell, 20.00m, 3000)
    };
    public decimal[] Caso3_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 1000m };

    // ---------------------- Caso 4 ----------------------
    public List<Operacao> Caso4 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Buy, 25.00m, 5000),
        new Operacao(TipoOperacaoEnum.Sell, 15.00m, 10000)
    };
    public decimal[] Caso4_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 0m };

    // ---------------------- Caso 5 ----------------------
    public List<Operacao> Caso5 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Buy, 25.00m, 5000),
        new Operacao(TipoOperacaoEnum.Sell, 15.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 25.00m, 5000)
    };
    public decimal[] Caso5_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 0m, 10000m };

    // ---------------------- Caso 6 ----------------------
    public List<Operacao> Caso6 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 2.00m, 5000),
        new Operacao(TipoOperacaoEnum.Sell, 20.00m, 2000),
        new Operacao(TipoOperacaoEnum.Sell, 20.00m, 2000),
        new Operacao(TipoOperacaoEnum.Sell, 25.00m, 1000)
    };
    public decimal[] Caso6_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 0m, 0m, 3000m };

    // ---------------------- Caso 7 ----------------------
    public List<Operacao> Caso7 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 2.00m, 5000),
        new Operacao(TipoOperacaoEnum.Sell, 20.00m, 2000),
        new Operacao(TipoOperacaoEnum.Sell, 20.00m, 2000),
        new Operacao(TipoOperacaoEnum.Sell, 25.00m, 1000)
    };
    public decimal[] Caso7_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 0m, 0m, 3000m };

    // ---------------------- Caso 8 ----------------------
    public List<Operacao> Caso8 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 10.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 50.00m, 10000),
        new Operacao(TipoOperacaoEnum.Buy, 20.00m, 10000),
        new Operacao(TipoOperacaoEnum.Sell, 50.00m, 10000)
    };
    public decimal[] Caso8_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 80000m, 0m, 60000m };

    // ---------------------- Caso 9 ----------------------
    public List<Operacao> Caso9 => new()
    {
        new Operacao(TipoOperacaoEnum.Buy, 5000.00m, 10),
        new Operacao(TipoOperacaoEnum.Sell, 4000.00m, 5),
        new Operacao(TipoOperacaoEnum.Buy, 15000.00m, 5),
        new Operacao(TipoOperacaoEnum.Buy, 4000.00m, 2),
        new Operacao(TipoOperacaoEnum.Buy, 23000.00m, 2),
        new Operacao(TipoOperacaoEnum.Sell, 20000.00m, 1),
        new Operacao(TipoOperacaoEnum.Sell, 12000.00m, 10),
        new Operacao(TipoOperacaoEnum.Sell, 15000.00m, 3)
    };
    public decimal[] Caso9_ImpostoGanhoCapitalEsperado => new decimal[] { 0m, 0m, 0m, 0m, 0m, 0m, 1000m, 2400m };
}
