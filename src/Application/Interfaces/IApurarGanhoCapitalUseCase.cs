using Domain.Entities;

namespace Application.Interfaces;

public interface IApurarGanhoCapitalUseCase
{
    IEnumerable<ImpostoGanhoCapital> Execute(IEnumerable<Operacao> operacoes);
}
