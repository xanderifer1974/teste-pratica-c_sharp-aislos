
namespace Questao5.Domain.Request;

public class MovimentacaoRequest
{
    public string? RequisicaoId { get; set; }
    public string? ContaCorrenteId { get; set; }
    public decimal Valor { get; set; }
    public char TipoMovimento { get; set; }
}
