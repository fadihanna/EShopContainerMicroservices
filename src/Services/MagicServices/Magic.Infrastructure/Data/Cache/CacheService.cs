using Microsoft.Extensions.Caching.Memory;

namespace Magic.Infrastructure.Data.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            // Try to retrieve the value from the cache
            if (_memoryCache.TryGetValue(key, out var value))
            {
                return Task.FromResult((T?)value);
            }

            return Task.FromResult(default(T));
        }

        public Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default)
        {
            // Set the value in the cache with an expiration time
            _memoryCache.Set(key, value, expiration);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            // Remove the value from the cache
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}