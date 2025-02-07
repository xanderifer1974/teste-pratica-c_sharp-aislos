using Moq;
using Questao5.Businness;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Xunit;

namespace Questao5.Questao5Api.Tests.Business;

public class MovimentacaoServiceTests
{
    private readonly Mock<IMovimentacaoRepository> _mockMovimentacaoRepository;
    private readonly MovimentacaoService _service;

    public MovimentacaoServiceTests()
    {
        _mockMovimentacaoRepository = new Mock<IMovimentacaoRepository>();
        _service = new MovimentacaoService(_mockMovimentacaoRepository.Object);
    }

    [Fact]
    public async Task MovimentarContaCorrenteAsync_ShouldReturnMovimento_WhenCalledWithValidParameters()
    {
        // Arrange
        var requisicaoId = "Req123";
        var contaCorrenteId = "Conta456";
        var valor = 1000.50m;
        var tipoMovimento = 'C';
        var movimento = new Movimento
        {
            IdMovimento = "Mov123",
            NumeroConta = contaCorrenteId,
            DataMovimento = DateTime.Now.ToString(),
            TipoMovimentacao = TipoMovimentoEnum.Credito,
            Valor = valor
        };

        _mockMovimentacaoRepository.Setup(repo => repo.MovimentarContaCorrenteAsync(requisicaoId, contaCorrenteId, valor, tipoMovimento))
            .ReturnsAsync(movimento);

        // Act
        var result = await _service.MovimentarContaCorrenteAsync(requisicaoId, contaCorrenteId, valor, tipoMovimento);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Mov123", result.IdMovimento);
        Assert.Equal(contaCorrenteId, result.NumeroConta);
        Assert.Equal(valor, result.Valor);
        Assert.Equal(TipoMovimentoEnum.Credito, result.TipoMovimentacao);
    }

    [Fact]
    public async Task MovimentarContaCorrenteAsync_ShouldThrowException_WhenRepositoryThrowsException()
    {
        // Arrange
        var requisicaoId = "Req123";
        var contaCorrenteId = "Conta456";
        var valor = 1000.50m;
        var tipoMovimento = 'C';

        _mockMovimentacaoRepository.Setup(repo => repo.MovimentarContaCorrenteAsync(requisicaoId, contaCorrenteId, valor, tipoMovimento))
            .ThrowsAsync(new Exception("Repository error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _service.MovimentarContaCorrenteAsync(requisicaoId, contaCorrenteId, valor, tipoMovimento));
        Assert.Equal("Repository error", exception.Message);
    }
}
