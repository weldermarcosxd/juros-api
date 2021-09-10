using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CalculoJurosService.Controllers
{
    [ApiController]
    [Route("calculaJuros")]
    public class CalculaJurosController : ControllerBase
    {
        private readonly ILogger<CalculaJurosController> _logger;

        public CalculaJurosController(ILogger<CalculaJurosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<double> Get([FromQuery] decimal valorInicial, [FromQuery] int meses)
        {
            _logger.LogInformation("Calculando juros");
            var x = (double)valorInicial * Math.Pow(1 + 0.01, meses);
            return Math.Round(x, 2);
        }
    }
}
