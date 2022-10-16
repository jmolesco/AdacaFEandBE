using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Adaca.Api.Infrastructure.Database
{
    public static class DatabaseStartup
    {
        public static IServiceCollection ConfigureDatabaseSqlServer(this IServiceCollection services, IConfiguration Configuration)
        {           
            return services;
        }
    }
}