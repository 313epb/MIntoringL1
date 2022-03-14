using Caching.Task1.Part2.Northwind;
using Caching.Task1.Part2.Northwind.Tables;
using Caching.Task1.Part2.NorthwindCaching.Customers;
using Caching.Task1.Part2.NorthwindCaching.Customers.CustomersCaching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Threading;

namespace Caching.Task1.Part2.Tests.Customers
{
	[TestClass]
	public class CustomersManagerTests
	{
		/// <summary>
		/// Scenario Creates a new instance of the <see cref="CustomersManager" /> class with invalid cache parameter
		/// Given Null reference of the cache parameter
		/// When Creates a new instance of the <see cref="CustomersManager" /> class
		/// Then Throws an exception of the type <see cref="ArgumentNullException" />
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CustomersManager_InvalidCache()
		{
			//Arrange
			ICustomersCache customersCache = null;

			//Act
			var customersManager = new CustomersManager(customersCache);

			//Assert is hanled by exception
		}

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="CustomersManager" /> class with valid cache parameter
		/// Given Valid instance of the cache parameter
		/// When Creates a new instance of the <see cref="CustomersManager" /> class
		/// Then Created instance is nor null
		/// </summary>
		public void CustomersManager_ValidCache()
		{
			//Arrange
			var customersCache = new CustomersCache();

			//Act
			var customersManager = new CustomersManager(customersCache);

			//Assert
			Assert.IsNotNull(customersManager);
		}

		/// <summary>
		/// Scenario Gets collection of the <see cref="Customer" /> and sets to the cache
		/// Given Created instance of the <see cref="CustomersCache" /> class
		/// When Gets collection of the <see cref="Customer" /> with the property <see cref="CustomersManager.Customers" /> and
		/// sets the collection to the cache
		/// Then After the collection is recieved there must be the same collection in the cache
		/// </summary>
		[TestMethod]
		public void CustomersFromCache()
		{
			//Arrange
			var customersManager = new CustomersManager(new CustomersCache());
			var memoryCache = MemoryCache.Default;
			string userName = Thread.CurrentPrincipal.Identity.Name;

			//Act
			var customers = customersManager.Customers;

			//Assert
			var cached = memoryCache.Get(CustomersCache.CacheKeyPrefix + userName);
			var cachedCustomers = cached as IEnumerable<Customer>;

			Assert.IsNotNull(cached);
			Assert.IsNotNull(cachedCustomers);
			Assert.AreEqual(customers.Count(), cachedCustomers.Count());
		}

		/// <summary>
		/// Scenario Checks that cached customers will be removed after <see cref="DateTimeOffset" /> expiration starts
		/// Given Created instance of the <see cref="CustomersCache" /> class
		/// When Gets collection of the <see cref="Customer" /> by the first time and gets again after a period of time
		/// Then There will no cached customers in the cache
		/// </summary>
		[TestMethod]
		public void RemovingCustomersFromCache_Expiration()
		{
			//Arrange
			const int exirationSeconds = 3;
			var customersManager =
				new CustomersManager(new CustomersCache(DateTimeOffset.Now.AddSeconds(exirationSeconds), string.Empty));
			var memoryCache = MemoryCache.Default;
			string userName = Thread.CurrentPrincipal.Identity.Name;
			string key = CustomersCache.CacheKeyPrefix + userName;

			if (memoryCache.Contains(key))
			{
				memoryCache.Remove(key);
			}

			//Act
			var customers = customersManager.Customers;
			Thread.Sleep(TimeSpan.FromSeconds(exirationSeconds + 1));

			//Assert
			var cached = memoryCache.Get(key);

			Assert.IsNull(cached);
		}

		/// <summary>
		/// Scenario Checks that cached customers will be removed after some changes to table associated with the
		/// <see cref="Customer" /> class
		/// Given Created instance of the <see cref="CustomersCache" /> class with the specified connection string
		/// When Gets collection of the <see cref="Customer" /> by the first time then add a new <see cref="Customer" /> to the
		/// database
		/// Then There will no cached customers in the cache
		/// </summary>
		[TestMethod]
		public void RemovingCustomersFromCache_SqlChangeMonitor()
		{
			//Arrange
			string connectionString = ConfigurationManager.ConnectionStrings["Caching.Task1.Part2.Northwind"].ConnectionString;
			var customerCache = new CustomersCache(DateTimeOffset.MaxValue, connectionString);
			var customersManager = new CustomersManager(customerCache);
			string userName = Thread.CurrentPrincipal.Identity.Name;

			//Act
			var customers = customersManager.Customers;

			using (var northwindContext = new NorthwindContext())
			{
				northwindContext.Customers.Add(new Customer
				{
					CompanyName = "Test company name " + northwindContext.Customers.Count()
				});
			}

			//Assert
			var cached = customerCache.GetCustomers(userName);

			Assert.IsNull(cached);
		}

		/// <summary>
		/// Scenario Gets collection of the <see cref="Customer" /> and sets to the Redis cache
		/// Given Created instance of the <see cref="CustomersRedis" /> class
		/// When Gets collection of the <see cref="Customer" /> with the property <see cref="CustomersManager.Customers" /> and
		/// sets the collection to the Redis cache
		/// Then After the collection is recieved there must be the same collection in the Redis cache
		/// </summary>
		[TestMethod]
		public void CustomersFromRedis()
		{
			//Arrange
			const string configuration = "localhost";
			var customersManager =
				new CustomersManager(new CustomersRedis(TimeSpan.FromSeconds(1), configuration));
			string userName = Thread.CurrentPrincipal.Identity.Name;

			//Act
			var customers = customersManager.Customers;

			//Assert
			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();
				var cached = database.StringGet(CustomersRedis.CacheKeyPrefix + userName);
				var dataContractSerializer = new DataContractSerializer(typeof(IEnumerable<Customer>));
				IEnumerable<Customer> cachedCustomers;

				using (var memoryStream = new MemoryStream(cached))
				{
					cachedCustomers = dataContractSerializer.ReadObject(memoryStream) as IEnumerable<Customer>;
				}

				Assert.IsNotNull(cached);
				Assert.IsNotNull(cachedCustomers);
				Assert.AreEqual(customers.Count(), cachedCustomers.Count());
			}
		}

		/// <summary>
		/// Scenario Checks that cached customers will be removed after <see cref="DateTimeOffset" /> expiration starts
		/// Given Created instance of the <see cref="CustomersRedis" /> class
		/// When Gets collection of the <see cref="Customer" /> by the first time and gets again after a period of time
		/// Then There will no cached customers in the Redis cache
		/// </summary>
		[TestMethod]
		public void RemovingCustomersFromRedis_Expiration()
		{
			//Arrange
			const string configuration = "localhost";
			const int exirationSeconds = 3;
			var customersManager =
				new CustomersManager(new CustomersRedis(TimeSpan.FromSeconds(exirationSeconds), configuration));
			string userName = Thread.CurrentPrincipal.Identity.Name;
			string key = CustomersCache.CacheKeyPrefix + userName;

			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();

				if (database.KeyExists(key))
				{
					database.KeyDelete(key);
				}
			}

			//Act
			var customers = customersManager.Customers;
			Thread.Sleep(TimeSpan.FromSeconds(exirationSeconds*2));

			//Assert
			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();

				byte[] cached = database.StringGet(key);

				Assert.IsNull(cached);
			}
		}
	}
}