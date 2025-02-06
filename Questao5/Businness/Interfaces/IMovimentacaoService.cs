using System;
using Questao5.Domain.Entities;

namespace Questao5.Businness.Interfaces;

public interface IMovimentacaoService
{
      Task<Movimento> MovimentarContaCorrenteAsync(string requisicaoId, string contaCorrenteId, decimal valor, char tipoMovimento);
}
