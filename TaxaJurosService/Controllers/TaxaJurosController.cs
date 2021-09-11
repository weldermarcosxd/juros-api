using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaxaJurosService.Models;

namespace TaxaJurosService.Controllers
{
    [ApiController]
    [Route("taxaJuros")]
    public class TaxaJurosController : ControllerBase
    {
        private readonly ILogger<TaxaJurosController> _logger;
        private readonly IConfiguration _configuration;

        public TaxaJurosController(ILogger<TaxaJurosController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            _logger.LogInformation("Obtendo taxa de juros");
            var configuracoesFinanceiras = _configuration.GetSection("ConfiguracoesFinanceiras").Get<ConfiguracoesFinanceiras>();
            if (configuracoesFinanceiras is null)
                return BadRequest("As configurações financeiras não foram informadas");

            return Ok(configuracoesFinanceiras.PercentualJuros);
        }
    }
}
