using Moq;
using Questao5.Businness;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Responses;
using Xunit;

namespace Questao5.Questao5Api.Tests.Business;

public class ContaCorrenteServiceTests
{
    private readonly Mock<IContaCorrenteRepository> _mockContaCorrenteRepository;
        private readonly ContaCorrenteService _service;

        public ContaCorrenteServiceTests()
        {
            _mockContaCorrenteRepository = new Mock<IContaCorrenteRepository>();
            _service = new ContaCorrenteService(_mockContaCorrenteRepository.Object);
        }

        [Fact]
        public async Task ConsultarSaldoAsync_ShouldReturnSaldoResponse_WhenContaCorrenteIsValid()
        {
            // Arrange
            var contaCorrenteId = "123";
            var saldoResponse = new SaldoResponse
            {
                NumeroContaCorrente = "456",
                NomeTitular = "João Silva",
                DataHoraResposta = DateTime.Now,
                ValorSaldo = 1000.50m
            };

            _mockContaCorrenteRepository.Setup(repo => repo.ConsultarSaldoAsync(contaCorrenteId))
                .ReturnsAsync(saldoResponse);

            // Act
            var result = await _service.ConsultarSaldoAsync(contaCorrenteId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("456", result.NumeroContaCorrente);
            Assert.Equal("João Silva", result.NomeTitular);
            Assert.Equal(1000.50m, result.ValorSaldo);
        }

        [Fact]
        public async Task ConsultarSaldoAsync_ShouldThrowException_WhenContaCorrenteIsInvalid()
        {
            // Arrange
            var contaCorrenteId = "123";

            _mockContaCorrenteRepository.Setup(repo => repo.ConsultarSaldoAsync(contaCorrenteId))
                .ThrowsAsync(new Exception("INVALID_ACCOUNT"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.ConsultarSaldoAsync(contaCorrenteId));
            Assert.Equal("INVALID_ACCOUNT", exception.Message);
        }

        [Fact]
        public async Task ConsultarSaldoAsync_ShouldThrowException_WhenContaCorrenteIsInactive()
        {
            // Arrange
            var contaCorrenteId = "123";

            _mockContaCorrenteRepository.Setup(repo => repo.ConsultarSaldoAsync(contaCorrenteId))
                .ThrowsAsync(new Exception("INACTIVE_ACCOUNT"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.ConsultarSaldoAsync(contaCorrenteId));
            Assert.Equal("INACTIVE_ACCOUNT", exception.Message);
        }
}
