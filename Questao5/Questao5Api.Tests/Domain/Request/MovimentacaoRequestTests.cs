using Questao5.Domain.Request;

namespace Questao5.Questao5Api.Tests.Domain.Request;

public class MovimentacaoRequestTests
{
    [Fact]
        public void MovimentacaoRequest_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            var movimentacaoRequest = new MovimentacaoRequest();

            // Act & Assert
            Assert.Null(movimentacaoRequest.RequisicaoId);
            Assert.Null(movimentacaoRequest.ContaCorrenteId);
            Assert.Equal(0m, movimentacaoRequest.Valor);
            Assert.Equal('\0', movimentacaoRequest.TipoMovimento); // '\0' é o valor padrão para char
        }

        [Fact]
        public void MovimentacaoRequest_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var movimentacaoRequest = new MovimentacaoRequest
            {
                RequisicaoId = "Req123",
                ContaCorrenteId = "Conta456",
                Valor = 1000.50m,
                TipoMovimento = 'C'
            };

            // Act & Assert
            Assert.Equal("Req123", movimentacaoRequest.RequisicaoId);
            Assert.Equal("Conta456", movimentacaoRequest.ContaCorrenteId);
            Assert.Equal(1000.50m, movimentacaoRequest.Valor);
            Assert.Equal('C', movimentacaoRequest.TipoMovimento);
        }       
}
