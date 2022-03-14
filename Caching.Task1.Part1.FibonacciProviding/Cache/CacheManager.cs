using System;
using System.Runtime.Caching;

namespace Caching.Task1.FibonacciProviding.Cache
{
	/// <summary>
	/// Implementation of the <see cref="ICacheManager" /> for System.Runtime.Caching.
	/// </summary>
	public class CacheManager : BaseCacheManager
	{
		private readonly ObjectCache _cache = MemoryCache.Default;

		/// <summary>
		/// Sets <paramref name="value" /> to cache.
		/// </summary>
		/// <param name="key">Key of the saved cache entry.</param>
		/// <param name="value">Value to be saved.</param>
		/// <typeparam name="TValue">Type of the <paramref name="value" />.</typeparam>
		public override void SetEntry<TValue>(string key, TValue value)
		{
			EnsureValid(key, value);

			_cache.Set(key, value, DateTimeOffset.MaxValue);
		}

		/// <summary>
		/// Gets a cache entry by the <paramref name="key" />.
		/// </summary>
		/// <param name="key">Key of the retrieving cache entry.</param>
		/// <typeparam name="TValue">Type of the retrieving cache entry.</typeparam>
		/// <returns>Retrieved cache value.</returns>
		public override TValue GetEntry<TValue>(string key)
		{
			EnsureValidKey(key);

			return (TValue) _cache.Get(key);
		}

		/// <summary>
		/// Determines whether cache contains an entry with the specified <paramref name="key" /> or not.
		/// </summary>
		/// <param name="key">Key of an cache entry.</param>
		/// <returns>An cache entiry exists in cache or not.</returns>
		public override bool Contains(string key)
		{
			EnsureValidKey(key);

			return _cache.Contains(key);
		}
	}
}