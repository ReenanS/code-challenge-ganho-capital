using Domain.Entities;

namespace Application.Interfaces;

public interface IApurarGanhoCapitalUseCase
{
    IEnumerable<ResultadoOperacao> Execute(IEnumerable<Operacao> operacoes);
}
