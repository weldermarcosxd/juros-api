using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        public decimal GetAsync()
        {
            _logger.LogInformation("obtendo taxa de juros");
            return _configuration.GetValue<decimal>("PercentualJuros");
        }
    }
}
