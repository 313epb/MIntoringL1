using System;

namespace Caching.Task1.FibonacciProviding.Cache
{
	/// <summary>
	/// Base implementation of the <see cref="ICacheManager" />.
	/// </summary>
	public abstract class BaseCacheManager : ICacheManager
	{
		/// <summary>
		/// Sets <paramref name="value" /> to cache.
		/// </summary>
		/// <param name="key">Key of the saved cache entry.</param>
		/// <param name="value">Value to be saved.</param>
		/// <typeparam name="TValue">Type of the <paramref name="value" />.</typeparam>
		public abstract void SetEntry<TValue>(string key, TValue value);

		/// <summary>
		/// Gets a cache entry by the <paramref name="key" />.
		/// </summary>
		/// <param name="key">Key of the retrieving cache entry.</param>
		/// <typeparam name="TValue">Type of the retrieving cache entry.</typeparam>
		/// <returns>Retrieved cache value.</returns>
		public abstract TValue GetEntry<TValue>(string key);

		/// <summary>
		/// Determines whether cache contains an entry with the specified <paramref name="key" /> or not.
		/// </summary>
		/// <param name="key">Key of an cache entry.</param>
		/// <returns>An cache entiry exists in cache or not.</returns>
		public abstract bool Contains(string key);

		/// <summary>
		/// Ensures <paramref name="key" /> of a cache entry is valid.
		/// </summary>
		/// <param name="key">Key of a cache entry.</param>
		/// <exception cref="ArgumentNullException"></exception>
		protected static void EnsureValidKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentException("Cache entry key must not be empty or contan white spaces.", nameof(key));
			}
		}

		/// <summary>
		/// Ensures <paramref name="value" /> of the cache entry is valid.
		/// </summary>
		/// <param name="value">Value of a cache entry.</param>
		/// <typeparam name="TValue">Type of the <paramref name="value" />.</typeparam>
		/// <exception cref="ArgumentNullException"></exception>
		protected static void EnsureValidValue<TValue>(TValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}
		}

		/// <summary>
		/// Ensures <paramref name="key" /> and <paramref name="value" /> are valid.
		/// </summary>
		/// <param name="key">Key of a cache entry.</param>
		/// <param name="value">Value of a cache entry.</param>
		/// <typeparam name="TValue">Type of the <paramref name="value" />.</typeparam>
		protected static void EnsureValid<TValue>(string key, TValue value)
		{
			EnsureValidKey(key);
			EnsureValidValue(value);
		}
	}
}