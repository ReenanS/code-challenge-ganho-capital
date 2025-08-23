using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

/// <summary>
/// Representa uma operação de negociação de ações, que pode ser compra ou venda.
/// </summary>
public class Operacao
{

    private TipoOperacaoEnum _tipoOperacao;
    private decimal _precoUnitarioAcao;
    private int _quantidadeAcoesNegociadas;

    /// <summary>
    /// Se a operação é do tipo compra (Buy) ou venda (Sell).
    /// </summary>
    [JsonPropertyName("operation")]
    [Required]
    public TipoOperacaoEnum TipoOperacao
    {
        get => _tipoOperacao;
        set
        {
            if (!Enum.IsDefined(typeof(TipoOperacaoEnum), value))
                throw new ArgumentException($"Tipo de operação inválido: {value}. Apenas 'Buy' ou 'Sell' são aceitos.");

            _tipoOperacao = value;
        }
    }

    /// <summary>
    /// Preço unitário da ação em moeda com duas casas decimais.
    /// Deve ser maior ou igual a zero.
    /// </summary>
    [JsonPropertyName("unit-cost")]
    [Range(0, double.MaxValue, ErrorMessage = "O preço unitário da ação não pode ser menor que 0.")]
    public decimal PrecoUnitarioAcao
    {
        get => _precoUnitarioAcao;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(PrecoUnitarioAcao), value, "O preço unitário da ação não pode ser menor que 0.");

            _precoUnitarioAcao = value;
        }
    }

    /// <summary>
    /// Quantidade de ações negociadas.
    /// Deve ser maior ou igual a zero.
    /// </summary>
    [JsonPropertyName("quantity")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de ações negociadas não pode ser menor que 0.")]
    public int QuantidadeAcoesNegociadas
    {
        get => _quantidadeAcoesNegociadas;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(QuantidadeAcoesNegociadas), value, "A quantidade de ações negociadas não pode ser menor que 0.");

            _quantidadeAcoesNegociadas = value;
        }
    }

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
