using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services;

/// <summary>
/// Serviço responsável pelo cálculo do imposto sobre ganho de capital em operações financeiras de ações.
/// </summary>
public class CalculoImpostoGanhoCapitalService : ICalculoImpostoGanhoCapitalService
{
    private const decimal LIMITE_ISENCAO = 20000.00m;
    private const decimal ALIQUOTA_IMPOSTO = 0.20m;

    public ResultadoOperacao Calcular(Operacao operacao, Portfolio portfolio)
    {
        return operacao switch
        {
            _ when operacao.EhCompra() => ProcessarCompra(operacao, portfolio),
            _ when operacao.EhVenda() => ProcessarVenda(operacao, portfolio),
            _ => throw new InvalidOperationException(
                $"Tipo de operação inválido para cálculo de imposto: {operacao.TipoOperacao}")
        };

    }

    private ResultadoOperacao ProcessarCompra(Operacao operacao, Portfolio portfolio)
    {
        portfolio.AplicarCompra(operacao);
        return new ImpostoGanhoCapital(0.00m);
    }

    private ResultadoOperacao ProcessarVenda(Operacao operacao, Portfolio portfolio)
    {

        if (operacao.QuantidadeAcoesNegociadas > portfolio.QuantidadeAcoesAtual)
        {
            return new ErroOperacao("Cant sell more stocks than you have");
        }

        decimal valorTotalOperacao = CalcularValorTotalOperacao(operacao);
        decimal lucroOperacao = CalcularLucroOperacao(operacao, portfolio);

        portfolio.ReduzirQuantidade(operacao.QuantidadeAcoesNegociadas);

        if (lucroOperacao < 0)
        {
            return RegistrarPrejuizo(lucroOperacao, portfolio);
        }

        if (valorTotalOperacao <= LIMITE_ISENCAO)
        {
            return new ImpostoGanhoCapital(0.00m);
        }

        decimal lucroAcumulado = lucroOperacao - portfolio.PrejuizoAcumulado;

        if (lucroAcumulado <= 0)
        {
            return CompensarPrejuizo(lucroOperacao, portfolio);
        }

        portfolio.LimparPrejuizo();
        decimal valorImposto = CalcularImposto(lucroAcumulado);

        return new ImpostoGanhoCapital(valorImposto);
    }

    private decimal CalcularValorTotalOperacao(Operacao operacao)
        => operacao.PrecoUnitarioAcao * operacao.QuantidadeAcoesNegociadas;

    private decimal CalcularLucroOperacao(Operacao operacao, Portfolio portfolio)
        => (operacao.PrecoUnitarioAcao - portfolio.PrecoMedioPonderado) * operacao.QuantidadeAcoesNegociadas;

    private ResultadoOperacao RegistrarPrejuizo(decimal lucroOperacao, Portfolio portfolio)
    {
        portfolio.RegistrarPrejuizo(Math.Abs(lucroOperacao));
        return new ImpostoGanhoCapital(0.00m);
    }

    private ResultadoOperacao CompensarPrejuizo(decimal lucroOperacao, Portfolio portfolio)
    {
        portfolio.DeduzirPrejuizo(lucroOperacao);
        return new ImpostoGanhoCapital(0.00m);
    }

    private decimal CalcularImposto(decimal lucroAcumulado)
        => Math.Round(lucroAcumulado * ALIQUOTA_IMPOSTO, 2, MidpointRounding.AwayFromZero);
}
