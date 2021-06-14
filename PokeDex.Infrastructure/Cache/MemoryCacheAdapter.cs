using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
namespace PokeDex.Infrastructure.Cache
{
	public class MemoryCacheAdapter : ICacheStorage
	{
		public IMemoryCache _cache { get; }

		static readonly ConcurrentDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();
		public MemoryCacheAdapter(IMemoryCache  memoryCache)
		{
			_cache = memoryCache;
		}
		public void Remove(string key)
		{
			if (!_cache.TryGetValue(key, out object cacheEntry))
			{
				if (cacheEntry != null)
				{
					_cache.Remove(key);
				}
			}
		}
		public void Store(string key, int cacheTimeInMinutes, object data)
		{
			lock (_locks.GetOrAdd(key, _ => new object()))
			{
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(cacheTimeInMinutes));

				_cache.Set(key, data, cacheEntryOptions);
			}
		}
		public T Retrieve<T>(string key)
		{
			T cachedObject;

			lock (_locks.GetOrAdd(key, _ => new object()))
			{
				if (!_cache.TryGetValue(key, out cachedObject))
				{
					return default(T);
				}
			}

			return cachedObject;
		}
	}
}
