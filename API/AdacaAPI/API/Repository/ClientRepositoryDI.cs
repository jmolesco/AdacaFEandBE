using Adaca.Api.DataModels;
using Adaca.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adaca.Api.Repository
{
    public static class ClientRepositoryDI
    {
        public static IServiceCollection AddClientRepositoryDI(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IClientUnitOfWork, ClientUnitOfWork>();
            return services;
        }
    }
}
