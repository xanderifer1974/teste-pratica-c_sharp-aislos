using Questao5.Businness.Interfaces;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Responses;

namespace Questao5.Businness;

public class ContaCorrenteService : IContaCorrenteService
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
    }
    public async Task<SaldoResponse> ConsultarSaldoAsync(string contaCorrenteId)
    {
         return await _contaCorrenteRepository.ConsultarSaldoAsync(contaCorrenteId);
    }
}
