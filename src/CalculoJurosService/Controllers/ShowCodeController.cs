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
        public IActionResult Get()
        {
            _logger.LogInformation("Obtendo repositorio git");
            var gitRepo = _configuration.GetValue<Uri>("GithubRepo");
            if (gitRepo is null)
                return BadRequest("O repositório não foi informado na app.settings");
            
            return Ok(gitRepo);
        }
    }
}
