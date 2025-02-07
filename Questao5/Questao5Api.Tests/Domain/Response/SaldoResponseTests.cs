using Questao5.Domain.Responses;

namespace Questao5.Questao5Api.Tests.Domain.Response;

public class SaldoResponseTests
{
    [Fact]
    public void SaldoResponse_ShouldInitializeWithDefaultValues()
    {
        // Arrange
        var saldoResponse = new SaldoResponse();

        // Act & Assert
        Assert.Null(saldoResponse.NumeroContaCorrente);
        Assert.Null(saldoResponse.NomeTitular);
        Assert.Equal(default(DateTime), saldoResponse.DataHoraResposta);
        Assert.Equal(0m, saldoResponse.ValorSaldo);
    }

    [Fact]
    public void SaldoResponse_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var saldoResponse = new SaldoResponse
        {
            NumeroContaCorrente = "123456",
            NomeTitular = "João Silva",
            DataHoraResposta = new DateTime(2025, 2, 7, 12, 0, 0),
            ValorSaldo = 1500.75m
        };

        // Act & Assert
        Assert.Equal("123456", saldoResponse.NumeroContaCorrente);
        Assert.Equal("João Silva", saldoResponse.NomeTitular);
        Assert.Equal(new DateTime(2025, 2, 7, 12, 0, 0), saldoResponse.DataHoraResposta);
        Assert.Equal(1500.75m, saldoResponse.ValorSaldo);
    }
}
