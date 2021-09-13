using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WireMock.Server;

namespace CalculoJurosService.Integration.Tests
{
    public class IntegrationTestAppFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var wiremockServer = WireMockServer.Start();
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("TaxaJurosServiceConfig:BaseURl", wiremockServer.Urls[0])
                });
            }).ConfigureServices(collection => collection.AddSingleton(wiremockServer));
        }
    }
}
