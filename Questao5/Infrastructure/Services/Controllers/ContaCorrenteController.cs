using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Businness.Interfaces;

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

        [HttpGet("{contaCorrenteId}")]
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
