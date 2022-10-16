using Finaps.EventBus.AzureServiceBus;
using Finaps.EventBus.AzureServiceBus.DependencyInjection;
using Finaps.EventBus.Core.DependencyInjection;
using Finaps.EventBus.RabbitMQ;
using Finaps.EventBus.RabbitMQ.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adaca.Api.Infrastructure.EventBus
{
    public static class AzureServiceBusExtensions
    {
        public static IServiceCollection ConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusOptions = configuration.GetSection("EventBus");
            if (Boolean.Parse(eventBusOptions["AzureServiceBusEnabled"]))
            {
                services.ConfigureAzureServiceBus(eventBusOptions);
            }
            else
            {
                services.ConfigureRabbitMQ(eventBusOptions);
            }
            return services;
        }
        private static IServiceCollection ConfigureAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusOptions = new AzureServiceBusOptions();
            configuration.GetSection("AzureServiceBus")?.Bind(eventBusOptions);

            services.AddAzureServiceBus(eventBusOptions);
            services.ConfigureEventHandlers();
            return services;
        }
        private static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitConfig = new RabbitMQOptions();
            configuration.GetSection("RabbitMQ").Bind(rabbitConfig);

            services.AddRabbitMQ(rabbitConfig);
            services.ConfigureEventHandlers();
            return services;

        }
        public static IServiceCollection ConfigureEventHandlers(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder AddEventHandlers(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
