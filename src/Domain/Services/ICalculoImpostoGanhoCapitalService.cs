using Domain.Entities;

namespace Domain.Services;

public interface ICalculoImpostoGanhoCapitalService
{
    ImpostoGanhoCapital Calcular(Operacao operacao, Portfolio portfolio);
}
