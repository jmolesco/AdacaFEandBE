using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace Adaca.Api.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10),
                SlidingExpiration = TimeSpan.FromDays(10)
            };

            _cache.SetString(key, JsonConvert.SerializeObject(value), options);

            return value;
        }

        public void Remove(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                _cache.Remove(key);
            }
        }
    }
}
