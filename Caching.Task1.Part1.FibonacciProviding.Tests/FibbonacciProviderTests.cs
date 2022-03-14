using Caching.Task1.FibonacciProviding.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Runtime.Caching;

namespace Caching.Task1.FibonacciProviding.Tests
{
	[TestClass]
	public class FibbonacciProviderTests
	{
		/// <summary>
		/// Scenario Creates a new instance of the <see cref="FibonacciProvider" /> class
		/// with an invalid <see cref="ICacheManager" /> class instance
		/// Given Null instance of the <see cref="ICacheManager" /> class
		/// When Creates a new instance of the <see cref="FibonacciProvider" /> class
		/// Then Throws an exception of the <see cref="ArgumentNullException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void FibbonacciProvider_InvalidCacheManager()
		{
			//Arrange
			ICacheManager cacheManager = null;

			//Act
			var fibbonacciProvider = new FibonacciProvider(cacheManager);

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Gets a Fibbonacci sequence and saves to the cache
		/// Given Empty cache, legth of the desired Fibbonacci sequence
		/// When Gets sequence by the <see cref="FibonacciProvider" />
		/// Then Returned sequence must be saved to the cache and they are equal
		/// </summary>
		[TestMethod]
		public void GetSequence_SaveToCache()
		{
			//Arrange
			var fibbonacciProvider = new FibonacciProvider(new CacheManager());
			var cache = MemoryCache.Default;
			const int length = 5;

			//Act
			var sequence = fibbonacciProvider.GetSequence(length);

			//Assert
			var cachedSequence = cache.Get(string.Format(FibonacciProvider.FibonacciKeyFormat, length)) as int[];
			Assert.IsNotNull(cachedSequence);
			Assert.IsTrue(sequence.SequenceEqual(cachedSequence));
		}

		/// <summary>
		/// Scenario Gets a Fibbonacci sequence from cache
		/// Given Legth of the sequence, empty cache
		/// When Gets a sequence once by the <see cref="FibonacciProvider" /> and saves
		/// the sequence to the cache, and gets again a sequence with the length and
		/// returns the same sequence from the cache
		/// Then Returned sequence equals to the firstly saved sequence and
		/// the cache contains only once the sequence
		/// </summary>
		[TestMethod]
		public void GetSequence_FromCache()
		{
			//Arrange
			var fibbonacciProvider = new FibonacciProvider(new CacheManager());
			const int length = 5;
			var sequenceToSave = fibbonacciProvider.GetSequence(length);
			var cache = MemoryCache.Default;

			//Act
			var cachedSequence = fibbonacciProvider.GetSequence(length);

			//Assert
			Assert.IsTrue(sequenceToSave.SequenceEqual(cachedSequence));
			Assert.IsTrue(cache.Contains(string.Format(FibonacciProvider.FibonacciKeyFormat, length)));
			Assert.AreEqual(1, cache.GetCount());
		}

		/// <summary>
		/// Scenario Gets a Fibbonacci sequence and saves to the Redis
		/// Given Empty cache in Redis, legth of the desired Fibbonacci sequence
		/// When Gets sequence by the <see cref="FibonacciProvider" />
		/// Then Returned sequence must be saved to the Redis and they are equal
		/// </summary>
		[TestMethod]
		public void GetSequence_SaveToRedis()
		{
			//Arrange
			const string configuration = "localhost";
			var fibbonacciProvider = new FibonacciProvider(new RedisCacheManager(configuration));
			const int length = 5;

			//Act
			var sequence = fibbonacciProvider.GetSequence(length);

			//Assert
			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();

				var cachedSequence =
					JsonConvert.DeserializeObject<int[]>(database.StringGet(string.Format(FibonacciProvider.FibonacciKeyFormat, length)));
				Assert.IsNotNull(cachedSequence);
				Assert.IsTrue(sequence.SequenceEqual(cachedSequence));
			}
		}

		/// <summary>
		/// Scenario Gets a Fibbonacci sequence from Redis
		/// Given Legth of the sequence, empty cache in Redis
		/// When Gets a sequence once by the <see cref="FibonacciProvider" /> and saves
		/// the sequence to the Redis, and gets again a sequence with the length and
		/// returns the same sequence from the Redis
		/// Then Returned sequence equals to the firstly saved sequence
		/// </summary>
		[TestMethod]
		public void GetSequence_FromRedis()
		{
			//Arrange
			const string configuration = "localhost";
			var fibbonacciProvider = new FibonacciProvider(new RedisCacheManager(configuration));
			const int length = 5;
			var sequenceToSave = fibbonacciProvider.GetSequence(length);

			//Act
			var cachedSequence = fibbonacciProvider.GetSequence(length);

			//Assert
			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();

				Assert.IsTrue(sequenceToSave.SequenceEqual(cachedSequence));
				Assert.IsTrue(database.KeyExists(string.Format(FibonacciProvider.FibonacciKeyFormat, length)));
			}
		}
	}
}