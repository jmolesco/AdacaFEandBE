using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adaca.Api.Infrastructure.Routes
{
    public static class RouteStartup
    {
        public static IServiceCollection ConfigureRouteOptions(this IServiceCollection services, IConfiguration Configuration)
        {            
            return services;
        }
    }
}
