using Domain.Entities;

namespace Domain.Services;

public class CalculoImpostoGanhoCapitalService
{
    public ImpostoGanhoCapital Execute(Operacao operacao, Portfolio portfolio)
    {
        /*Nenhum imposto é pago em operações de compra.*/
        if (operacao.EhCompra())
        {
            portfolio.AplicarCompra(operacao);
            return new ImpostoGanhoCapital(0.00m);
        }

        if (operacao.QuantidadeAcoesNegociadas > portfolio.QuantidadeAcoesAtual)
            throw new InvalidOperationException("QuantidadeAcoesNegociadas vendida maior que a disponível.");

        /*Valor total da operação (custo unitário da ação x quantidade) */
        decimal valorTotalOperacao = operacao.PrecoUnitarioAcao * operacao.QuantidadeAcoesNegociadas;

        decimal lucro = (operacao.PrecoUnitarioAcao - portfolio.PrecoMedioPonderado) * operacao.QuantidadeAcoesNegociadas;

        portfolio.ReduzirQuantidade(operacao.QuantidadeAcoesNegociadas);

        if (lucro < 0)
        {
            portfolio.RegistrarPrejuizo(Math.Abs(lucro));
            return new ImpostoGanhoCapital(0.00m);
        }

        /*Você não paga nenhum imposto e não deve deduzir o lucro obtido dos prejuízos acumulados se o valor
        total da operação (custo unitário da ação x quantidade) for menor ou igual a R$ 20000,00.*/

        if (valorTotalOperacao <= 20000.00m)
            return new ImpostoGanhoCapital(0.00m);

        decimal lucroAcumulado = lucro - portfolio.PrejuizoAcumulado;

        if (lucroAcumulado <= 0)
        {
            portfolio.DeduzirPrejuizo(lucro);
            return new ImpostoGanhoCapital(0.00m);
        }

        portfolio.LimparPrejuizo();

        return new ImpostoGanhoCapital(Math.Round(lucroAcumulado * 0.20m, 2, MidpointRounding.AwayFromZero));
    }
}
