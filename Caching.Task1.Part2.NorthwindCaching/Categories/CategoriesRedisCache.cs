using Caching.Task1.Part2.Northwind.Tables;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;

namespace Caching.Task1.Part2.NorthwindCaching.Categories
{
	/// <summary>
	/// Implementation of the <see cref="ICategoriesCache" /> with Redis.
	/// </summary>
	public class CategoriesRedisCache : ICategoriesCache
	{
		private const string Prefix = "Cache_Categories";

		private readonly ConnectionMultiplexer _redisConnection;

		private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(IEnumerable<Category>));

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoriesRedisCache" /> class.
		/// </summary>
		/// <param name="hostName">Redis host name.</param>
		public CategoriesRedisCache(string hostName)
		{
			_redisConnection = ConnectionMultiplexer.Connect(hostName);
		}

		/// <summary>
		/// Gets enumerable of the <see cref="Category" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public IEnumerable<Category> GetCategories(string forUser)
		{
			var db = _redisConnection.GetDatabase();
			byte[] s = db.StringGet(Prefix + forUser);

			if (s == null)
			{
				return null;
			}

			return (IEnumerable<Category>) _serializer.ReadObject(new MemoryStream(s));
		}

		/// <summary>
		/// Sets collection of the <see cref="Category" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="categories">Collection of the <see cref="Category" /> to set to cache source.</param>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void SetCategories(string forUser, IEnumerable<Category> categories)
		{
			var db = _redisConnection.GetDatabase();
			string key = Prefix + forUser;

			if (categories == null)
			{
				db.StringSet(key, RedisValue.Null);
			}
			else
			{
				var stream = new MemoryStream();

				_serializer.WriteObject(stream, categories);
				db.StringSet(key, stream.ToArray());
			}
		}
	}
}