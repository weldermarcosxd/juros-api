using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJurosService
    {
        Task<decimal> CalculaValorJurosAsync(decimal valor, int meses);
    }
}
