using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace CalculoJurosService.Integration.Tests
{
    public class CalculaJurosTest : IClassFixture<IntegrationTestAppFactory<Startup>>
    {
        private readonly IntegrationTestAppFactory<Startup> _factory;
        private readonly WireMockServer _wireMockServer;

        public CalculaJurosTest(IntegrationTestAppFactory<Startup> factory)
        {
            _factory = factory;
            _wireMockServer = factory.Services.GetRequiredService<WireMockServer>();
        }

        [Fact]
        public async Task GetCalculoJuros_ValidInput_MontanteValido()
        {
            var request = "/calculaJuros";
            request += $"?valorInicial=100&meses=5";

            _wireMockServer
                .Given(Request.Create().WithPath("/taxaJuros"))
                .RespondWith(Response.Create()
                    .WithBody("0.01")
                    .WithStatusCode(HttpStatusCode.OK));

            var client = _factory.CreateClient();
            var response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("105.10", responseString);
        }

        [Fact]
        public async Task GetCalculoJuros_InvalidInput_MontanteValido()
        {
            var request = "/calculaJuros";

            _wireMockServer
                .Given(Request.Create().WithPath("/taxaJuros"))
                .RespondWith(Response.Create()
                    .WithBody("0.01")
                    .WithStatusCode(HttpStatusCode.OK));

            var client = _factory.CreateClient();
            var response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("0", responseString);
        }

        private static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;
            var applicationBasePath = System.AppContext.BaseDirectory;
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;

                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                {
                    var projectFileInfo = new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj"));
                    if (projectFileInfo.Exists)
                    {
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                    }
                }
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }
    }
}
