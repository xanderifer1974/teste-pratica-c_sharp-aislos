using Microsoft.AspNetCore.Mvc;
using Moq;
using Questao5.Businness.Interfaces;
using Questao5.Domain.Responses;
using Questao5.Infrastructure.Services.Controllers;

namespace Questao5.Questao5Api.Tests.Infraestructure.Services.Controllers;

public class ContaCorrenteControllerTests
{
  private readonly Mock<IContaCorrenteService> _mockService;
        private readonly ContaCorrenteController _controller;

        public ContaCorrenteControllerTests()
        {
            _mockService = new Mock<IContaCorrenteService>();
            _controller = new ContaCorrenteController(_mockService.Object);
        }

        [Fact]
        public async Task ConsultarSaldo_ReturnsOkResult_WhenIdIsValid()
        {
            // Arrange
            var contaCorrenteId = "valid-id";
            var expectedResponse = new SaldoResponse
            {
                NumeroContaCorrente = "12345",
                NomeTitular = "João Silva",
                DataHoraResposta = DateTime.Now,
                ValorSaldo = 100.0m
            };
            _mockService.Setup(service => service.ConsultarSaldoAsync(contaCorrenteId))
                        .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ConsultarSaldo(contaCorrenteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<SaldoResponse>(okResult.Value);
            Assert.Equal(expectedResponse.NumeroContaCorrente, actualResponse.NumeroContaCorrente);
            Assert.Equal(expectedResponse.NomeTitular, actualResponse.NomeTitular);
            Assert.Equal(expectedResponse.ValorSaldo, actualResponse.ValorSaldo);
            _mockService.Verify(service => service.ConsultarSaldoAsync(contaCorrenteId), Times.Once);
        }
        
}
