using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Operacao
{
    [JsonPropertyName("operation")]
    public TipoOperacaoEnum TipoOperacao { get; set; }

    [JsonPropertyName("unit-cost")]
    public decimal PrecoUnitarioAcao { get; set; }

    [JsonPropertyName("quantity")]
    public int QuantidadeAcoesNegociadas { get; set; }

    public Operacao() { } // Construtor padrão necessário para deserialização

    // Construtor adicional para inicialização rápida
    public Operacao(TipoOperacaoEnum tipoOperacao, decimal precoUnitarioAcao, int quantidadeAcoesNegociadas)
    {
        TipoOperacao = tipoOperacao;
        PrecoUnitarioAcao = precoUnitarioAcao;
        QuantidadeAcoesNegociadas = quantidadeAcoesNegociadas;
    }

    // Métodos auxiliares para melhorar legibilidade
    public bool EhCompra() => TipoOperacao == TipoOperacaoEnum.Buy;
    public bool EhVenda() => TipoOperacao == TipoOperacaoEnum.Sell;

}
