using System;
using Questao5.Businness.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Businness;

public class MovimentacaoService : IMovimentacaoService
{
   private readonly IMovimentacaoRepository _movimentacaoRepository;

    public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository)
    {
        _movimentacaoRepository = movimentacaoRepository;
    }
    public async Task<Movimento> MovimentarContaCorrenteAsync(string requisicaoId, string contaCorrenteId, decimal valor, char tipoMovimento)
    {
        return await _movimentacaoRepository.MovimentarContaCorrenteAsync(requisicaoId, contaCorrenteId, valor, tipoMovimento);
    }
}
