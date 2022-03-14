using Caching.Task1.Part2.Northwind.Tables;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Caching.Task1.Part2.NorthwindCaching.Customers.CustomersCaching
{
	/// <summary>
	/// Implementation of the <see cref="ICustomersCache" /> interface with Redis cache.
	/// </summary>
	public class CustomersRedis : ICustomersCache
	{
		/// <summary>
		/// Cache key prefix for Redis cache.
		/// </summary>
		public const string CacheKeyPrefix = "Cache_Customers";

		private readonly string _configuration;

		private readonly TimeSpan _expiration;

		private readonly XmlObjectSerializer _serializer;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomersRedis" /> class.
		/// </summary>
		/// <param name="expiration">Expiration time period to remove an cache entry from Redis.</param>
		/// <param name="configuration">Configuration to Redis.</param>
		public CustomersRedis(TimeSpan expiration, string configuration)
		{
			if (string.IsNullOrWhiteSpace(configuration))
			{
				throw new ArgumentException("Configuration must not be empty or contain white spaces.", nameof(configuration));
			}

			_expiration = expiration;
			_configuration = configuration;
			_serializer = new DataContractSerializer(typeof(IEnumerable<Customer>));
		}

		/// <summary>
		/// Gets enumerable of the <see cref="Customer" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		public IEnumerable<Customer> GetCustomers(string forUser)
		{
			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();
				byte[] serializedCustomers = database.StringGet(CacheKeyPrefix + forUser);

				if (serializedCustomers == null)
				{
					return null;
				}

				using (var memoryStream = new MemoryStream(serializedCustomers))
				{
					return (IEnumerable<Customer>) _serializer.ReadObject(memoryStream);
				}
			}
		}

		/// <summary>
		/// Sets collection of the <see cref="Customer" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="customers">Collection of the <see cref="Customer" /> to set to cache source.</param>
		public void SetCustomers(string forUser, IEnumerable<Customer> customers)
		{
			using (var connection = ConnectionMultiplexer.Connect(_configuration))
			{
				var database = connection.GetDatabase();
				string key = CacheKeyPrefix + forUser;

				if (customers == null)
				{
					database.StringSet(key, RedisValue.Null, _expiration);
				}
				else
				{
					using (var memoryStream = new MemoryStream())
					{
						_serializer.WriteObject(memoryStream, customers);
						database.StringSet(key, memoryStream.ToArray(), _expiration);
					}
				}
			}
		}
	}
}