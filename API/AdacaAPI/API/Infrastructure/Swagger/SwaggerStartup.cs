using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Adaca.Api.Infrastructure.Swagger
{
    public static class SwaggerStartup
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {               
               
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Adaca Core Api", Version = "v1" });
            });
            return services;
        }
    }
}
