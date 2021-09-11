using Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JurosService : IJurosService
    {
        private readonly ITaxaJurosService _taxaJurosService;

        public JurosService(ITaxaJurosService taxaJurosService)
        {
            _taxaJurosService = taxaJurosService;
        }

        public async Task<decimal> CalculaValorJurosAsync(decimal valorBase, int meses)
        {
            var taxaJuros = await _taxaJurosService.ObterTaxaJurosAsync();
            var montante = valorBase * (decimal)Math.Pow(1 + taxaJuros, meses);
            return Math.Round(montante, 2);
        }
    }
}
