using Domain.Entities;

namespace Domain.Interfaces;

public interface ICalculoImpostoGanhoCapitalService
{
    ResultadoOperacao Calcular(Operacao operacao, Portfolio portfolio);
}
