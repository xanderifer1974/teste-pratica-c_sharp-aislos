namespace Questao5.Domain.Responses;

public class SaldoResponse
{
    public string? NumeroContaCorrente { get; set; }
    public string? NomeTitular { get; set; }
    public DateTime DataHoraResposta { get; set; }
    public decimal ValorSaldo { get; set; }
}
