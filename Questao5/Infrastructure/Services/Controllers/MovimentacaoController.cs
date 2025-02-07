using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Questao5.Businness.Interfaces;
using Questao5.Domain.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoService _movimentacaoService;

        public MovimentacaoController(IMovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        /// <summary>
        /// Movimenta a conta corrente.
        /// </summary>
        /// <param name="request">Dados da movimentação.</param>
        /// <returns>O resultado da movimentação.</returns>
        /// <response code="200">Retorna o resultado da movimentação.</response>
        /// <response code="400">Se ocorrer um erro durante a movimentação.</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Movimenta a conta corrente", Description = "No request do método para movimentação da conta corrente, deve ser informado  como parâmetro o ID da conta corrente, referente à coluna idcontacorrente da tabela contacorrente do SQLite. Copie o idcontacorrente da tabela para fazer o teste da API.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o resultado da movimentação.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Se ocorrer um erro durante a movimentação.")]
        public async Task<IActionResult> MovimentarContaCorrente([FromBody] MovimentacaoRequest request)
        {
            try
            {
                // Gerar a chave de idempotência
                request.RequisicaoId = GerarChaveIdempotencia(request.ContaCorrenteId, request.Valor, request.TipoMovimento);

                var movimento = await _movimentacaoService.MovimentarContaCorrenteAsync(request.RequisicaoId, request.ContaCorrenteId, request.Valor, request.TipoMovimento);
                return Ok(movimento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        private string GerarChaveIdempotencia(string contaCorrenteId, decimal valor, char tipoMovimento)
        {
            var data = $"{contaCorrenteId}-{valor}-{tipoMovimento}-{DateTime.UtcNow.Ticks}";
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
