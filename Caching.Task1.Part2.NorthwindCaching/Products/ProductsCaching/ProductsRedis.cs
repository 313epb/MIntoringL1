using Caching.Task1.Part2.Northwind.Tables;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Caching.Task1.Part2.NorthwindCaching.Products.ProductsCaching
{
	/// <summary>
	/// Implementation of the <see cref="IProductsCache" /> interface with Redis cache.
	/// </summary>
	public class ProductsRedis : IProductsCache
	{
		/// <summary>
		/// Cache key prefix for Redis cache.
		/// </summary>
		public const string CacheKeyPrefix = "Cache_Products";

		private readonly string _configuration;

		private readonly TimeSpan _expiration;

		private readonly XmlObjectSerializer _serializer;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductsRedis" /> class.
		/// </summary>
		/// <param name="expiration">Expiration time period to remove an cache entry from Redis.</param>
		/// <param name="configuration">Configuration to Redis.</param>
		public ProductsRedis(TimeSpan expiration, string configuration)
		{
			if (string.IsNullOrWhiteSpace(configuration))
			{
				throw new ArgumentException("Configuration must not be empty or contain white spaces.", nameof(configuration));
			}

			_expiration = expiration;
			_configuration = configuration;
			_serializer = new DataContractSerializer(typeof(IEnumerable<Product>));
		}

		/// <summary>
		/// Gets enumerable of the <see cref="Product" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		public IEnumerable<Product> GetProducts(string forUser)
		{
			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();
				byte[] serializedProducts = database.StringGet(CacheKeyPrefix + forUser);

				if (serializedProducts == null)
				{
					return null;
				}

				using (var memoryStream = new MemoryStream(serializedProducts))
				{
					return (IEnumerable<Product>) _serializer.ReadObject(memoryStream);
				}
			}
		}

		/// <summary>
		/// Sets collection of the <see cref="Product" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="products">Collection of the <see cref="Product" /> to set to cache source.</param>
		public void SetProducts(string forUser, IEnumerable<Product> products)
		{
			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();
				string key = CacheKeyPrefix + forUser;

				if (products == null)
				{
					database.StringSet(key, RedisValue.Null, _expiration);
				}
				else
				{
					using (var memoryStream = new MemoryStream())
					{
						_serializer.WriteObject(memoryStream, products);
						database.StringSet(key, memoryStream.ToArray(), _expiration);
					}
				}
			}
		}
	}
}