using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class RedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void SetData<T>(string key, T data, TimeSpan expirationTime)
        {
            var serializedData = JsonSerializer.Serialize(data);
            _cache.SetString(key, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            });
        }

        public T? GetData<T>(string key)
        {
            var serializedData = _cache.GetString(key);
            if (serializedData != null)
            {
                return JsonSerializer.Deserialize<T>(serializedData);
            }
            return default;
        }

        public void RemoveData(string key)
        {
            _cache.Remove(key);
        }
    }
}
