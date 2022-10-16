using Finaps.EventBus.Core.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Adaca.Api.Infrastructure.KeepAlive.Event;
using Adaca.Api.Infrastructure.KeepAlive.Handler;
using Adaca.Api.Infrastructure.KeepAlive.Publisher;
using Adaca.Api.Infrastructure.KeepAlive.Scheduler;

namespace Adaca.Api.Infrastructure.KeepAlive.Startup
{
    public static class KeepAliveServiceStartup
    {
        public static IServiceCollection RegisterKeepAlive(this IServiceCollection services)
        {
            ConfigureEventHandlers(services);
            ConfigureEventPublishers(services);
            RegisterKeepAliveScheduler(services);
            return services;
        }

        private static IServiceCollection ConfigureEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<KeepAliveEventHandler>();
            return services;
        }

        private static IServiceCollection ConfigureEventPublishers(this IServiceCollection services)
        {
            services.AddScoped<IKeepAliveEventPublisher, KeepAliveEventPublisher>();
            return services;
        }

        private static IServiceCollection RegisterKeepAliveScheduler(this IServiceCollection services)
        {
            services.AddHostedService<KeepAliveScheduler>();
            return services;
        }

        public static IApplicationBuilder AddKeepAlive(this IApplicationBuilder app)
        {
            app.AddEventHandler<RegisKeepAliveEvent, KeepAliveEventHandler>();
            return app;
        }
    }
}
