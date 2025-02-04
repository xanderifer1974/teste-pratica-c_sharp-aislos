using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class ContaCorrente
{
 public string? IdContaCorrente { get; set; }
 public int Numero { get; set; }
 public string? Nome { get; set;}
 public AtivoEnum Ativo { get; set; } = AtivoEnum.Ativa;

}
