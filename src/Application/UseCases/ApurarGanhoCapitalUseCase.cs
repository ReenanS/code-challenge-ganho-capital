using Domain.Entities;
using Domain.Services;

namespace Application.UseCases
{
    /// <summary>
    /// Caso de uso responsável por apurar o imposto sobre ganho de capital
    /// para uma lista de operações financeiras.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância do <see cref="ApurarGanhoCapitalUseCase"/>.
    /// </remarks>
    /// <param name="service">Serviço de cálculo do imposto sobre ganho de capital.</param>
    public class ApurarGanhoCapitalUseCase(ICalculoImpostoGanhoCapitalService service)
    {
        /// <summary>
        /// Serviço de domínio responsável pelo cálculo do imposto sobre ganho de capital.
        /// </summary>
        private readonly ICalculoImpostoGanhoCapitalService _service = service;

        /// <summary>
        /// Executa o cálculo do imposto sobre ganho de capital para as operações informadas.
        /// </summary>
        /// <param name="operacoes">Lista de operações financeiras (compra, venda).</param>
        /// <returns>
        /// Uma lista de <see cref="ImpostoGanhoCapital"/> representando o valor do imposto calculado
        /// para cada operação.
        /// </returns>
        public IEnumerable<ImpostoGanhoCapital> Execute(IEnumerable<Operacao> operacoes)
        {
            var resultado = new List<ImpostoGanhoCapital>();

            var portfolio = new Portfolio();

            foreach (var operacao in operacoes)
            {
                resultado.Add(_service.Calcular(operacao, portfolio));
            }

            return resultado;
        }
    }
}
