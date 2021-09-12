using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CalculoJurosService.Controllers
{
    [ApiController]
    [Route("calculaJuros")]
    public class CalculaJurosController : ControllerBase
    {
        private readonly ILogger<CalculaJurosController> _logger;
        private readonly IJurosService _jurosService;

        public CalculaJurosController(ILogger<CalculaJurosController> logger, IJurosService jurosService)
        {
            _logger = logger;
            _jurosService = jurosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] decimal valorInicial, [FromQuery] int meses)
        {
            _logger.LogInformation("Calculando juros");
            var montante = await _jurosService.CalculaValorJurosAsync(valorInicial, meses);
            return Ok(montante);
        }
    }
}
