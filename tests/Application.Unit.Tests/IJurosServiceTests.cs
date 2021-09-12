using Application.Interfaces;
using Application.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Unit.Tests
{
    public class IJurosServiceTests
    {
        private readonly IJurosService _jurosService;
        private readonly Mock<ITaxaJurosService> _taxaJurosMockService;

        public IJurosServiceTests()
        {
            _taxaJurosMockService = new Mock<ITaxaJurosService>();
            _taxaJurosMockService.Setup(c => c.ObterTaxaJurosAsync()).ReturnsAsync(0.01);
            _jurosService = new JurosService(_taxaJurosMockService.Object);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(100, 5, 105.1)]
        [InlineData(100, 0, 100)]
        [InlineData(0, 5, 0)]
        [InlineData(0.1, 12, 0.11)]
        public async Task CalculaValorJurosAsync_ValidInput_MontanteComSucesso(decimal valorBase, int meses, decimal result)
        {
            var montante = await _jurosService.CalculaValorJurosAsync(valorBase, meses);
            Assert.Equal(result, montante);
        }


        // não foi especificado que entradas não seriam válidas então não apliquei nenhuma validação nos parâmetros, caso fosse teria aplicado fluent validation a request
        // e aqui esperaria que as validações fossem lancadas
        [Theory]
        [InlineData(-10, 0, -10)]
        [InlineData(10, -1, 9.90)]
        [InlineData(-10, -1, -9.90)]
        public async Task CalculaValorJurosAsync_InvalidInput_MontanteComSucesso(decimal valorBase, int meses, decimal result)
        {
            var montante = await _jurosService.CalculaValorJurosAsync(valorBase, meses);
            Assert.Equal(result, montante);
        }
    }
}
