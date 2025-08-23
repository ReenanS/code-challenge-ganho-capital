namespace Domain.Entities;

/// <summary>
/// Representa o portfólio de ações de um investidor, armazenando informações
/// sobre quantidade de ações, preço médio ponderado e prejuízos acumulados.
/// </summary>
public class Portfolio
{
    /// <summary>
    /// Preço médio ponderado das ações no portfólio.
    /// </summary>
    public decimal PrecoMedioPonderado { get; private set; }

    /// <summary>
    /// Quantidade atual de ações no portfólio.
    /// </summary>
    public int QuantidadeAcoesAtual { get; private set; }

    /// <summary>
    /// Valor total dos prejuízos acumulados que podem ser compensados em vendas futuras.
    /// </summary>
    public decimal PrejuizoAcumulado { get; private set; }

    /// <summary>
    /// Inicializa um novo portfólio de ações.
    /// </summary>
    /// <param name="precoMedioPonderado">Preço médio ponderado inicial (opcional).</param>
    /// <param name="quantidadeAcoesAtual">Quantidade inicial de ações (opcional).</param>
    /// <param name="prejuizoAcumulado">Prejuízo acumulado inicial (opcional).</param>
    public Portfolio(decimal precoMedioPonderado = 0, int quantidadeAcoesAtual = 0, decimal prejuizoAcumulado = 0)
    {
        PrecoMedioPonderado = precoMedioPonderado;
        QuantidadeAcoesAtual = quantidadeAcoesAtual;
        PrejuizoAcumulado = prejuizoAcumulado;
    }

    /// <summary>
    /// Aplica uma operação de compra no portfólio, ajustando o preço médio ponderado e a quantidade de ações.
    /// </summary>
    /// <param name="operacao">Operação de compra a ser aplicada.</param>
    public void AplicarCompra(Operacao operacao)
    {
        PrecoMedioPonderado = ((QuantidadeAcoesAtual * PrecoMedioPonderado) +
                                (operacao.QuantidadeAcoesNegociadas * operacao.PrecoUnitarioAcao)) /
                               (QuantidadeAcoesAtual + operacao.QuantidadeAcoesNegociadas);

        QuantidadeAcoesAtual += operacao.QuantidadeAcoesNegociadas;
    }

    /// <summary>
    /// Registra um prejuízo acumulado no portfólio.
    /// </summary>
    /// <param name="valor">Valor do prejuízo a ser registrado.</param>
    public void RegistrarPrejuizo(decimal valor)
    {
        PrejuizoAcumulado += valor;
    }

    /// <summary>
    /// Deduz um valor do prejuízo acumulado.
    /// </summary>
    /// <param name="valor">Valor a ser deduzido.</param>
    public void DeduzirPrejuizo(decimal valor)
    {
        PrejuizoAcumulado = Math.Max(0, PrejuizoAcumulado - valor);
    }

    /// <summary>
    /// Limpa todos os prejuízos acumulados do portfólio.
    /// </summary>
    public void LimparPrejuizo()
    {
        PrejuizoAcumulado = 0;
    }

    /// <summary>
    /// Reduz a quantidade de ações no portfólio.
    /// </summary>
    /// <param name="quantidade">Quantidade de ações a reduzir.</param>
    public void ReduzirQuantidade(int quantidade) => QuantidadeAcoesAtual -= quantidade;
}

