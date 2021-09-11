using Infrastructure.ResponsesDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Infrastructure.Validation
{
    public static class ApplicationServiceExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext => {
                    var erros = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = erros
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }
    }
}
