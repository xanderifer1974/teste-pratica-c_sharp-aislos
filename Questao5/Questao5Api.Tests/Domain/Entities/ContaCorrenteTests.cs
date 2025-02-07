using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Xunit;

namespace Questao5.Questao5Api.Tests.Domain.Entities;

public class ContaCorrenteTests
{
    [Fact]
    public void ContaCorrente_ShouldInitializeWithDefaultValues()
    {
        // Arrange
        var contaCorrente = new ContaCorrente();

        // Act & Assert
        Assert.Null(contaCorrente.IdContaCorrente);
        Assert.Equal(0, contaCorrente.Numero);
        Assert.Null(contaCorrente.Nome);
        Assert.Equal(AtivoEnum.Ativa, contaCorrente.Ativo);
    }

    [Fact]
    public void ContaCorrente_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var contaCorrente = new ContaCorrente
        {
            IdContaCorrente = "123",
            Numero = 456,
            Nome = "Conta Teste",
            Ativo = AtivoEnum.Inativa
        };

        // Act & Assert
        Assert.Equal("123", contaCorrente.IdContaCorrente);
        Assert.Equal(456, contaCorrente.Numero);
        Assert.Equal("Conta Teste", contaCorrente.Nome);
        Assert.Equal(AtivoEnum.Inativa, contaCorrente.Ativo);
    }
}
