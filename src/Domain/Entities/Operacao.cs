using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities;

/// <summary>
/// Representa uma operação de negociação de ações, que pode ser compra ou venda.
/// </summary>
public class Operacao
{

    /// <summary>
    /// Se a operação é uma operação de compra (buy) ou venda (sell).
    /// </summary>
    [JsonPropertyName("operation")]
    public TipoOperacaoEnum TipoOperacao { get; set; }

    /// <summary>
    /// Preço unitário da ação em uma moeda com duas casas decimais.
    /// </summary>
    [JsonPropertyName("unit-cost")]
    public decimal PrecoUnitarioAcao { get; set; }

    /// <summary>
    /// Quantidade de ações negociadas.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int QuantidadeAcoesNegociadas { get; set; }

    /// <summary>
    /// Construtor padrão necessário para deserialização.
    /// </summary>
    public Operacao() { }

    /// <summary>
    /// Construtor para inicialização rápida de uma operação.
    /// </summary>
    /// <param name="tipoOperacao">Tipo da operação.</param>
    /// <param name="precoUnitarioAcao">Preço unitário da ação.</param>
    /// <param name="quantidadeAcoesNegociadas">Quantidade de ações negociadas.</param>
    public Operacao(TipoOperacaoEnum tipoOperacao, decimal precoUnitarioAcao, int quantidadeAcoesNegociadas)
    {
        TipoOperacao = tipoOperacao;
        PrecoUnitarioAcao = precoUnitarioAcao;
        QuantidadeAcoesNegociadas = quantidadeAcoesNegociadas;
    }

    /// <summary>
    /// Verifica se a operação é do tipo compra.
    /// </summary>
    /// <returns>True se for compra; caso contrário, false.</returns>
    public bool EhCompra() => TipoOperacao == TipoOperacaoEnum.Buy;

    /// <summary>
    /// Verifica se a operação é do tipo venda.
    /// </summary>
    /// <returns>True se for venda; caso contrário, false.</returns>
    public bool EhVenda() => TipoOperacao == TipoOperacaoEnum.Sell;

}
