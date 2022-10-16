using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using StackExchange.Redis;

namespace Adaca.Api.Infrastructure.Cache
{
    public static class CacheStartup
    {
        public static IServiceCollection ConfigureCachingService(this IServiceCollection services, IConfiguration Configuration)
        {
            if (!String.IsNullOrEmpty(Configuration["CacheType"]) && Configuration["CacheType"].Equals("Redis", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration.GetConnectionString("RedisCacheConnection");
                });
            }
            else if (!String.IsNullOrEmpty(Configuration["CacheType"]) && Configuration["CacheType"].Equals("AzureRedis", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration.GetConnectionString("AzureRedisCacheConnection");
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            return services;       
        }
    }
}
