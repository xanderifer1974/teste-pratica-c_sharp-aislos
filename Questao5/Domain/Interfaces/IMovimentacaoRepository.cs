using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces;

public interface IMovimentacaoRepository
{
     Task<Movimento> MovimentarContaCorrenteAsync(string requisicaoId, string contaCorrenteId, decimal valor, char tipoMovimento);
}
