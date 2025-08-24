namespace Domain.Entities;

public class Portfolio
{

    public decimal PrecoMedioPonderado { get; private set; }

    public int QuantidadeAcoesAtual { get; private set; }

    public decimal PrejuizoAcumulado { get; private set; }

    public Portfolio(decimal precoMedioPonderado = 0, int quantidadeAcoesAtual = 0, decimal prejuizoAcumulado = 0)
    {
        PrecoMedioPonderado = precoMedioPonderado;
        QuantidadeAcoesAtual = quantidadeAcoesAtual;
        PrejuizoAcumulado = prejuizoAcumulado;
    }

    public void AplicarCompra(Operacao operacao)
    {
        PrecoMedioPonderado = (QuantidadeAcoesAtual * PrecoMedioPonderado +
                                operacao.QuantidadeAcoesNegociadas * operacao.PrecoUnitarioAcao) /
                               (QuantidadeAcoesAtual + operacao.QuantidadeAcoesNegociadas);

        QuantidadeAcoesAtual += operacao.QuantidadeAcoesNegociadas;
    }

    public void RegistrarPrejuizo(decimal valor)
    {
        PrejuizoAcumulado += valor;
    }

    public void DeduzirPrejuizo(decimal valor)
    {
        PrejuizoAcumulado = Math.Max(0, PrejuizoAcumulado - valor);
    }

    public void LimparPrejuizo()
    {
        PrejuizoAcumulado = 0;
    }

    public void ReduzirQuantidade(int quantidade) => QuantidadeAcoesAtual -= quantidade;
}

