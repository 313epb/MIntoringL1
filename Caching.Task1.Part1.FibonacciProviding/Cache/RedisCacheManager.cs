using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Caching.Task1.FibonacciProviding.Cache
{
	/// <summary>
	/// Implementation of the <see cref="ICacheManager" /> for Redis.
	/// </summary>
	public class RedisCacheManager : BaseCacheManager
	{
		private readonly string _configuration = "localhost";

		/// <summary>
		/// Initializes a new instance of the <see cref="RedisCacheManager" /> class.
		/// </summary>
		public RedisCacheManager()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RedisCacheManager" /> class.
		/// </summary>
		/// <param name="configuration">Configuration to Redis.</param>
		public RedisCacheManager(string configuration)
		{
			if (string.IsNullOrWhiteSpace(configuration))
			{
				throw new ArgumentException("Configuration must not be empty or contain white spaces.", nameof(configuration));
			}

			_configuration = configuration;
		}

		/// <summary>
		/// Sets <paramref name="value" /> to cache.
		/// </summary>
		/// <param name="key">Key of the saved cache entry.</param>
		/// <param name="value">Value to be saved.</param>
		/// <typeparam name="TValue">Type of the <paramref name="value" />.</typeparam>
		public override void SetEntry<TValue>(string key, TValue value)
		{
			EnsureValid(key, value);

			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();

				database.StringSet(key, JsonConvert.SerializeObject(value));
			}
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

			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();

				return JsonConvert.DeserializeObject<TValue>(database.StringGet(key));
			}
		}

		/// <summary>
		/// Determines whether cache contains an entry with the specified <paramref name="key" /> or not.
		/// </summary>
		/// <param name="key">Key of an cache entry.</param>
		/// <returns>An cache entiry exists in cache or not.</returns>
		public override bool Contains(string key)
		{
			EnsureValidKey(key);

			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();

				return database.KeyExists(key);
			}
		}
	}
}