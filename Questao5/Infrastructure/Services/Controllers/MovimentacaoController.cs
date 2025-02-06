using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Questao5.Businness.Interfaces;
using Questao5.Domain.Request;

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

        [HttpPost]
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
