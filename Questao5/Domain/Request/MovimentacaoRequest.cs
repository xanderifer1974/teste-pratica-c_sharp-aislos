
using System.Text.Json.Serialization;

namespace Questao5.Domain.Request;

public class MovimentacaoRequest
{
    [JsonIgnore]
    public string? RequisicaoId { get; set; }
    public string? ContaCorrenteId { get; set; }
    public decimal Valor { get; set; }
    public char TipoMovimento { get; set; }
}
