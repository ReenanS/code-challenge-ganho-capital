using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public class TaxDomainService
{
    public ImpostoPago Execute(Operacao operacao, Portfolio portfolio)
    {
        /*Nenhum imposto é pago em operações de compra.*/
        if (operacao.EhCompra())
        {
            portfolio.AplicarCompra(operacao);
            return new ImpostoPago(0.00m);
        }

        if (operacao.Quantidade > portfolio.QuantidadeAcoesAtual)
            throw new InvalidOperationException("Quantidade vendida maior que a disponível.");

        decimal valorTotalOperacao = operacao.PrecoUnitarioAcao * operacao.Quantidade;

        decimal lucro = (operacao.PrecoUnitarioAcao - portfolio.PrecoMedioPonderado) * operacao.Quantidade;

        portfolio.ReduzirQuantidade(operacao.Quantidade);

        if (lucro < 0)
        {
            portfolio.RegistrarPrejuizo(Math.Abs(lucro));
            return new ImpostoPago(0.00m);
        }

        /*Você não paga nenhum imposto e não deve deduzir o lucro obtido dos prejuízos acumulados se o valor
        total da operação (custo unitário da ação x quantidade) for menor ou igual a R$ 20000,00.*/

        if (valorTotalOperacao <= 20000.00m)
            return new ImpostoPago(0.00m);

        decimal lucroAcumulado = lucro - portfolio.PrejuizoAcumulado;

        if (lucroAcumulado <= 0)
        {
            portfolio.DeduzirPrejuizo(lucro);
            return new ImpostoPago(0.00m);
        }

        portfolio.LimparPrejuizo();

        return new ImpostoPago(Math.Round(lucroAcumulado * 0.20m, 2, MidpointRounding.AwayFromZero));
    }
}
