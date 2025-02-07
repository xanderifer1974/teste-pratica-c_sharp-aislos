using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Businness.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteService _contaCorrenteService;

        public ContaCorrenteController(IContaCorrenteService contaCorrenteService)
        {
            _contaCorrenteService = contaCorrenteService;
        }

        /// <summary>
        /// Consulta o saldo da conta corrente.
        /// </summary>
        /// <param name="contaCorrenteId">ID da conta corrente.</param>
        /// <returns>O saldo da conta corrente.</returns>
        /// <response code="200">Retorna o saldo da conta corrente.</response>
        /// <response code="400">Se o ID da conta corrente for inválido.</response>
        [HttpGet("{contaCorrenteId}")]
        [SwaggerOperation(Summary = "Consulta o saldo da conta corrente", Description = "O endpoint consulta o saldo da conta corrente, passando como parâmetro o ID da conta corrente, referente à coluna idcontacorrente da tabela contacorrente do SQLite. Copie o idcontacorrente da tabela para fazer o teste da API.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o saldo da conta corrente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Se o ID da conta corrente for inválido.")]
        public async Task<IActionResult> ConsultarSaldo(string contaCorrenteId)
        {
            try
            {
                var saldo = await _contaCorrenteService.ConsultarSaldoAsync(contaCorrenteId);
                return Ok(saldo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
