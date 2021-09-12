using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CalculoJurosService.Integration.Tests
{
    // exige que TaxaJurosService esteja rodando na porta especificada no appsetiings de CalculoJurosService
    public class CalculaJurosTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CalculaJurosTest()
        {
            var projectDir = "";
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Development")
            .UseContentRoot(projectDir)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile(
                  "appsettings.json",
                  optional: true,
                  reloadOnChange: false);
            }));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetCalculoJuros_ValidInput_MontanteValido()
        {
            var request = "/calculaJuros";
                request += $"?valorInicial=100&meses=5";

            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("105.10", responseString);
        }

        [Fact]
        public async Task GetCalculoJuros_InvalidInput_MontanteValido()
        {
            var request = "/calculaJuros";

            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("0", responseString);
        }
    }
}
