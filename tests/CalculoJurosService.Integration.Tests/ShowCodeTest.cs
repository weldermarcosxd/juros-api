using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CalculoJurosService.Integration.Tests
{
    // exige que TaxaJurosService esteja rodando na porta especificada no appsetiings de CalculoJurosService
    public class ShowCodeTest : IClassFixture<IntegrationTestAppFactory<Startup>>
    {
        private readonly IntegrationTestAppFactory<Startup> _factory;

        public ShowCodeTest(IntegrationTestAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetShowCode_ValidInput_UriCorreta()
        {
            var request = "/showmethecode";

            var client = _factory.CreateClient();
            var response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("\"https://github.com/weldermarcosxd/juros-api\"", responseString);
        }

        [Fact]
        public async Task GetShowCode_InvalidRepoUrl_BadRequest()
        {
            var baseDir = Path.GetDirectoryName(typeof(ShowCodeTest).Assembly.Location);
            var invalidServer = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Development")
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile(
                  $"{baseDir}{Path.DirectorySeparatorChar}local.settings.json",
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
