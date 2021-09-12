using Refit;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaxaJurosService
    {
        [Get("/taxaJuros")]
        Task<double> ObterTaxaJurosAsync();
    }
}
