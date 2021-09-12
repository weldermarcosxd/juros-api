using Application.Interfaces;
using Infrastructure.Config;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Infrastructure.Refit
{
    public static class RefitConfig
    {
        public static void ConfigureRefit(this IServiceCollection services, IConfiguration configuration)
        {
            var taxaJurosServiceConfig = configuration.GetSection("TaxaJurosServiceConfig").Get<TaxaJurosServiceConfig>();
            if (string.IsNullOrEmpty(taxaJurosServiceConfig?.BaseURl))
                throw new InvalidTaxaJurosServiceUrlException();

            services.AddRefitClient<ITaxaJurosService>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(taxaJurosServiceConfig.BaseURl);
            });
        }
    }
}
