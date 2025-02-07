using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Questao5.Businness.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Request;
using Questao5.Infrastructure.Services.Controllers;
using Xunit;

namespace Questao5.Questao5Api.Tests.Infraestructure.Services.Controllers;

public class MovimentacaoControllerTests
{
    private readonly Mock<IMovimentacaoService> _mockService;
    private readonly MovimentacaoController _controller;

    public MovimentacaoControllerTests()
    {
        _mockService = new Mock<IMovimentacaoService>();
        _controller = new MovimentacaoController(_mockService.Object);
    }

    [Fact]
    public async Task MovimentarContaCorrente_ReturnsOkResult_WhenRequestIsValid()
    {
        // Arrange
        var request = new MovimentacaoRequest
        {
            ContaCorrenteId = "valid-id",
            Valor = 100.0m,
            TipoMovimento = 'C'
        };
        var expectedResponse = new Movimento
        {
            IdMovimento = "mov-123",
            NumeroConta = "12345",
            DataMovimento = "2025-02-07",
            TipoMovimentacao = TipoMovimentoEnum.Credito,
            Valor = 100.0m
        };
        _mockService.Setup(service => service.MovimentarContaCorrenteAsync(It.IsAny<string>(), request.ContaCorrenteId, request.Valor, request.TipoMovimento))
                    .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.MovimentarContaCorrente(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<Movimento>(okResult.Value);
        Assert.Equal(expectedResponse.IdMovimento, actualResponse.IdMovimento);
        Assert.Equal(expectedResponse.NumeroConta, actualResponse.NumeroConta);
        Assert.Equal(expectedResponse.DataMovimento, actualResponse.DataMovimento);
        Assert.Equal(expectedResponse.TipoMovimentacao, actualResponse.TipoMovimentacao);
        Assert.Equal(expectedResponse.Valor, actualResponse.Valor);
        _mockService.Verify(service => service.MovimentarContaCorrenteAsync(It.IsAny<string>(), request.ContaCorrenteId, request.Valor, request.TipoMovimento), Times.Once);
    }

    
}
