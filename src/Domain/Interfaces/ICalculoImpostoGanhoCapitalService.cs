using Domain.Entities;

namespace Domain.Interfaces;

public interface ICalculoImpostoGanhoCapitalService
{
    ImpostoGanhoCapital Calcular(Operacao operacao, Portfolio portfolio);
}
