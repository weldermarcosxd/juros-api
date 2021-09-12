using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TaxaJurosService.Integration.Tests
{
    public class TaxaJurosTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TaxaJurosTests()
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
        public async Task TaxaJuros_Valid_Input_Sucess()
        {
            var request = "/taxaJuros";

            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("0.01", responseString);
        }
    }
}
