using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class Movimento
{
    public string? IdMovimento { get; set; }
    public string? NumeroConta { get; set; }
    public string? DataMovimento { get; set; }
    public TipoMovimentoEnum TipoMovimentacao { get; set; }
    public decimal Valor { get; set; }

}
