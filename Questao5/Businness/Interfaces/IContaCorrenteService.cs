using Questao5.Domain.Responses;

namespace Questao5.Businness.Interfaces;

public interface IContaCorrenteService
{
    Task<SaldoResponse> ConsultarSaldoAsync(string contaCorrenteId);
}
