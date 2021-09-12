using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace CalculoJurosService.Controllers
{
    [ApiController]
    [Route("/showmethecode")]
    public class ShowCodeController : ControllerBase
    {
        private readonly ILogger<CalculaJurosController> _logger;
        private readonly IConfiguration _configuration;

        public ShowCodeController(ILogger<CalculaJurosController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public Uri Get()
        {
            _logger.LogInformation("Obtendo repositorio git");
            return _configuration.GetValue<Uri>("GithubRepo");
        }
    }
}
