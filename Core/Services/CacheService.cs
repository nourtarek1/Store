using Domian.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepsitory cacheRepsitory) : ICacheService
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
            var value = await cacheRepsitory.GetAsync(key);
            return value == null ? null : value;
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
            await cacheRepsitory.SetAsync(key, value, duration);
        }
    }
}
