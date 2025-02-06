using System;
using Questao5.Domain.Responses;

namespace Questao5.Domain.Interfaces;

public interface IContaCorrenteRepository
{
    Task<SaldoResponse> ConsultarSaldoAsync(string contaCorrenteId);

}
