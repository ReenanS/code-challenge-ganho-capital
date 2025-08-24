using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class Operacao
{

    private TipoOperacaoEnum _tipoOperacao;
    private decimal _precoUnitarioAcao;
    private int _quantidadeAcoesNegociadas;

    [JsonPropertyName("operation")]
    [Required]
    public TipoOperacaoEnum TipoOperacao
    {
        get => _tipoOperacao;
        set
        {
            if (!Enum.IsDefined(typeof(TipoOperacaoEnum), value))
            {
                throw new ArgumentException($"Tipo de operação inválido: {value}. Apenas 'Buy' ou 'Sell' são aceitos.");
            }

            _tipoOperacao = value;
        }
    }

    [JsonPropertyName("unit-cost")]
    [Range(0, double.MaxValue, ErrorMessage = "O preço unitário da ação não pode ser menor que 0.")]
    public decimal PrecoUnitarioAcao
    {
        get => _precoUnitarioAcao;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(PrecoUnitarioAcao), value, "O preço unitário da ação não pode ser menor que 0.");
            }

            _precoUnitarioAcao = value;
        }
    }

    [JsonPropertyName("quantity")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de ações negociadas não pode ser menor que 0.")]
    public int QuantidadeAcoesNegociadas
    {
        get => _quantidadeAcoesNegociadas;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(QuantidadeAcoesNegociadas), value, "A quantidade de ações negociadas não pode ser menor que 0.");
            }

            _quantidadeAcoesNegociadas = value;
        }
    }

    public Operacao() { }

    public Operacao(TipoOperacaoEnum tipoOperacao, decimal precoUnitarioAcao, int quantidadeAcoesNegociadas)
    {
        TipoOperacao = tipoOperacao;
        PrecoUnitarioAcao = precoUnitarioAcao;
        QuantidadeAcoesNegociadas = quantidadeAcoesNegociadas;
    }

    public bool EhCompra() => TipoOperacao == TipoOperacaoEnum.Buy;

    public bool EhVenda() => TipoOperacao == TipoOperacaoEnum.Sell;

}
