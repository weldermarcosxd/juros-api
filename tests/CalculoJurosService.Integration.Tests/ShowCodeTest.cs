using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CalculoJurosService.Integration.Tests
{
    // exige que TaxaJurosService esteja rodando na porta especificada no appsetiings de CalculoJurosService
    public class ShowCodeTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ShowCodeTest()
        {
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Development")
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
        public async Task GetShowCode_ValidInput_UriCorreta()
        {
            var request = "/showmethecode";

            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("\"https://github.com/weldermarcosxd/juros-api\"", responseString);
        }

        [Fact]
        public async Task GetShowCode_InvalidRepoUrl_BadRequest()
        {
            var invalidServer = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Development")
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile(
                  "C:\\Users\\welde\\source\\repos\\weldermarcosxd\\juros-api\\tests\\CalculoJurosService.Integration.Tests\\local.settings.json",
                  optional: true,
                  reloadOnChange: false);
            }));
            var invalidClient = invalidServer.CreateClient();

            var request = "/showmethecode";

            var response = await invalidClient.GetAsync(request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
