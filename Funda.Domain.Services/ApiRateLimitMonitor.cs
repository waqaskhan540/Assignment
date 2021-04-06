using Funda.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Services
{
    public class ApiRateLimitMonitor : IApiRateLimitMonitor
    {
        private readonly IMemoryCache _memoryCache;
        private const string EntryKey = "_RequestCount";

        public ApiRateLimitMonitor(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
       
        public int GetRequestCount()
        {
            int requestCount;
            if (!_memoryCache.TryGetValue(EntryKey, out requestCount))
                return 0;

            return requestCount;
        }
       
        public void UpdateRequestCount()
        {
            int requestCount;
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

            if (!_memoryCache.TryGetValue(EntryKey, out requestCount))
            {
                requestCount = 1;
                _memoryCache.Set(EntryKey, requestCount, cacheEntryOptions);
            }
            else
            {
                _memoryCache.Set(EntryKey, ++requestCount, cacheEntryOptions);
            }

        }
    }
}
