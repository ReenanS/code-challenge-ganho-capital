using Domain.Entities;

namespace Domain.Services;

/// <summary>
/// Serviço responsável pelo cálculo do imposto sobre ganho de capital
/// em operações financeiras de ações.
/// </summary>
/// <remarks>
/// Implementa a interface <see cref="ICalculoImpostoGanhoCapitalService"/> 
/// e encapsula a lógica de negócios para determinar o imposto devido
/// em operações de venda, considerando prejuízos acumulados, limites de isenção e operações de compra.
/// </remarks>
public class CalculoImpostoGanhoCapitalService : ICalculoImpostoGanhoCapitalService
{
    /// <summary>
    /// Calcula o imposto sobre ganho de capital de uma operação, atualizando o portfólio conforme necessário.
    /// </summary>
    /// <param name="operacao">A operação financeira (compra ou venda) a ser processada.</param>
    /// <param name="portfolio">O portfólio do investidor, utilizado para cálculo de preço médio ponderado, quantidade de ações e prejuízos acumulados.</param>
    /// <returns>
    /// Um objeto <see cref="ImpostoGanhoCapital"/> representando o valor do imposto a ser pago.
    /// </returns>
    public ImpostoGanhoCapital Calcular(Operacao operacao, Portfolio portfolio)
    {
        // Nenhum imposto é pago em operações de compra
        if (operacao.EhCompra())
        {
            portfolio.AplicarCompra(operacao);
            return new ImpostoGanhoCapital(0.00m);
        }

        // Valor total da operação (custo unitário da ação x quantidade)
        decimal valorTotalOperacao = operacao.PrecoUnitarioAcao * operacao.QuantidadeAcoesNegociadas;

        // Cálculo do lucro da operação
        decimal lucroOperacao = (operacao.PrecoUnitarioAcao - portfolio.PrecoMedioPonderado) * operacao.QuantidadeAcoesNegociadas;

        portfolio.ReduzirQuantidade(operacao.QuantidadeAcoesNegociadas);

        // Se houver prejuízo, registra e não cobra imposto
        if (lucroOperacao < 0)
        {
            portfolio.RegistrarPrejuizo(Math.Abs(lucroOperacao));
            return new ImpostoGanhoCapital(0.00m);
        }

        // Isenção para operações de valor total <= 20.000
        if (valorTotalOperacao <= 20000.00m)
            return new ImpostoGanhoCapital(0.00m);

        // Calcula lucro acumulado após compensar prejuízos acumulados
        decimal lucroAcumulado = lucroOperacao - portfolio.PrejuizoAcumulado;

        if (lucroAcumulado <= 0)
        {
            portfolio.DeduzirPrejuizo(lucroOperacao);
            return new ImpostoGanhoCapital(0.00m);
        }

        portfolio.LimparPrejuizo();

        // Calcula o  (20% do lucroOperacao líquido)
        decimal valorImpostoGanhoCapital = Math.Round(lucroAcumulado * 0.20m, 2, MidpointRounding.AwayFromZero);

        // Retorna o imposto calculado
        return new ImpostoGanhoCapital(valorImpostoGanhoCapital);
    }
}
