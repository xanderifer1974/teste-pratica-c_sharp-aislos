using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Xunit;

namespace Questao5.Questao5Api.Tests.Domain.Entities;

public class MovimentoTests
{
    [Fact]
    public void Movimento_ShouldInitializeWithDefaultValues()
    {
        // Arrange
        var movimento = new Movimento();

        // Act & Assert
        Assert.Null(movimento.IdMovimento);
        Assert.Null(movimento.NumeroConta);
        Assert.Null(movimento.DataMovimento);
        Assert.Equal(default(TipoMovimentoEnum), movimento.TipoMovimentacao);
        Assert.Equal(0m, movimento.Valor);
    }

    [Fact]
    public void Movimento_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var movimento = new Movimento
        {
            IdMovimento = "Mov123",
            NumeroConta = "Conta456",
            DataMovimento = "2025-02-07",
            TipoMovimentacao = TipoMovimentoEnum.Credito,
            Valor = 1000.50m
        };

        // Act & Assert
        Assert.Equal("Mov123", movimento.IdMovimento);
        Assert.Equal("Conta456", movimento.NumeroConta);
        Assert.Equal("2025-02-07", movimento.DataMovimento);
        Assert.Equal(TipoMovimentoEnum.Credito, movimento.TipoMovimentacao);
        Assert.Equal(1000.50m, movimento.Valor);
    }
}
