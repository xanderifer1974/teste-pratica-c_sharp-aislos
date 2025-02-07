using Questao5.Domain.Entities;
using Xunit;

namespace Questao5.Questao5Api.Tests.Domain.Entities;

public class IdemPotenciaTests
{
    [Fact]
    public void IdemPotencia_ShouldInitializeWithDefaultValues()
    {
        // Arrange
        var idemPotencia = new IdemPotencia();

        // Act & Assert
        Assert.Null(idemPotencia.ChaveIdemPotencia);
        Assert.Null(idemPotencia.Requisicao);
        Assert.Null(idemPotencia.Resultado);
    }

    [Fact]
    public void IdemPotencia_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var idemPotencia = new IdemPotencia
        {
            ChaveIdemPotencia = "Chave123",
            Requisicao = "RequisicaoTeste",
            Resultado = "ResultadoTeste"
        };

        // Act & Assert
        Assert.Equal("Chave123", idemPotencia.ChaveIdemPotencia);
        Assert.Equal("RequisicaoTeste", idemPotencia.Requisicao);
        Assert.Equal("ResultadoTeste", idemPotencia.Resultado);
    }

}
