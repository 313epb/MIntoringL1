using Caching.Task1.Part2.Northwind;
using Caching.Task1.Part2.Northwind.Tables;
using Caching.Task1.Part2.NorthwindCaching.Products;
using Caching.Task1.Part2.NorthwindCaching.Products.ProductsCaching;
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

namespace Caching.Task1.Part2.Tests.Products
{
	[TestClass]
	public class ProductsManagerTests
	{
		/// <summary>
		/// Scenario Creates a new instance of the <see cref="ProductsManager" /> class with invalid cache parameter
		/// Given Null reference of the cache parameter
		/// When Creates a new instance of the <see cref="ProductsManager" /> class
		/// Then Throws an exception of the type <see cref="ArgumentNullException" />
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ProductsManager_InvalidCache()
		{
			//Arrange
			IProductsCache productsCache = null;

			//Act
			var productsManager = new ProductsManager(productsCache);

			//Assert is hanled by exception
		}

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="ProductsManager" /> class with valid cache parameter
		/// Given Valid instance of the cache parameter
		/// When Creates a new instance of the <see cref="ProductsManager" /> class
		/// Then Created instance is nor null
		/// </summary>
		public void ProductsManager_ValidCache()
		{
			//Arrange
			var productsCache = new ProductsCache();

			//Act
			var productsManager = new ProductsManager(productsCache);

			//Assert
			Assert.IsNotNull(productsManager);
		}

		/// <summary>
		/// Scenario Gets collection of the <see cref="Product" /> and sets to the cache
		/// Given Created instance of the <see cref="ProductsCache" /> class
		/// When Gets collection of the <see cref="Product" /> with the property <see cref="ProductsManager.Products" /> and
		/// sets the collection to the cache
		/// Then After the collection is recieved there must be the same collection in the cache
		/// </summary>
		[TestMethod]
		public void ProductsFromCache()
		{
			//Arrange
			var productsManager = new ProductsManager(new ProductsCache());
			var memoryCache = MemoryCache.Default;
			string userName = Thread.CurrentPrincipal.Identity.Name;

			//Act
			var products = productsManager.Products;

			//Assert
			var cached = memoryCache.Get(ProductsCache.CacheKeyPrefix + userName);
			var cachedProducts = cached as IEnumerable<Product>;

			Assert.IsNotNull(cached);
			Assert.IsNotNull(cachedProducts);
			Assert.AreEqual(products.Count(), cachedProducts.Count());
		}

		/// <summary>
		/// Scenario Checks that cached products will be removed after <see cref="DateTimeOffset" /> expiration starts
		/// Given Created instance of the <see cref="ProductsCache" /> class
		/// When Gets collection of the <see cref="Product" /> by the first time and gets again after a period of time
		/// Then There will no cached products in the cache
		/// </summary>
		[TestMethod]
		public void RemovingProductsFromCache_Expiration()
		{
			//Arrange
			const int exirationSeconds = 3;
			var productsManager =
				new ProductsManager(new ProductsCache(DateTimeOffset.Now.AddSeconds(exirationSeconds), string.Empty));
			var memoryCache = MemoryCache.Default;
			string userName = Thread.CurrentPrincipal.Identity.Name;
			string key = ProductsCache.CacheKeyPrefix + userName;

			if (memoryCache.Contains(key))
			{
				memoryCache.Remove(key);
			}

			//Act
			var products = productsManager.Products;
			Thread.Sleep(TimeSpan.FromSeconds(exirationSeconds + 1));

			//Assert
			var cached = memoryCache.Get(key);

			Assert.IsNull(cached);
		}

		/// <summary>
		/// Scenario Checks that cached products will be removed after some changes to table associated with the
		/// <see cref="Product" /> class
		/// Given Created instance of the <see cref="ProductsCache" /> class with the specified connection string
		/// When Gets collection of the <see cref="Product" /> by the first time then add a new <see cref="Product" /> to the
		/// database
		/// Then There will no cached products in the cache
		/// </summary>
		[TestMethod]
		public void RemovingProductsFromCache_SqlChangeMonitor()
		{
			//Arrange
			string connectionString = ConfigurationManager.ConnectionStrings["Caching.Task1.Part2.Northwind"].ConnectionString;
			var productsCache = new ProductsCache(DateTimeOffset.MaxValue, connectionString);
			var productsManager = new ProductsManager(productsCache);
			var memoryCache = MemoryCache.Default;
			string userName = Thread.CurrentPrincipal.Identity.Name;
			string key = ProductsCache.CacheKeyPrefix + userName;

			if (memoryCache.Contains(key))
			{
				memoryCache.Remove(key);
			}

			//Act
			var products = productsManager.Products;

			using (var northwindContext = new NorthwindContext())
			{
				northwindContext.Products.Add(new Product
				{
					ProductName = "Test product " + northwindContext.Products.Count()
				});
			}

			//Assert
			var cached = productsCache.GetProducts(userName);

			Assert.IsNull(cached);
		}

		/// <summary>
		/// Scenario Gets collection of the <see cref="Product" /> and sets to the Redis cache
		/// Given Created instance of the <see cref="ProductsRedis" /> class
		/// When Gets collection of the <see cref="Product" /> with the property <see cref="ProductsManager.Products" /> and
		/// sets the collection to the Redis cache
		/// Then After the collection is recieved there must be the same collection in the Redis cache
		/// </summary>
		[TestMethod]
		public void ProductsFromRedis()
		{
			//Arrange
			const string configuration = "localhost";
			var productsManager =
				new ProductsManager(new ProductsRedis(TimeSpan.FromSeconds(1), configuration));
			string userName = Thread.CurrentPrincipal.Identity.Name;

			//Act
			var products = productsManager.Products;

			//Assert
			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();
				var cached = database.StringGet(ProductsRedis.CacheKeyPrefix + userName);
				var dataContractSerializer = new DataContractSerializer(typeof(IEnumerable<Product>));
				IEnumerable<Product> cachedProducts;

				using (var memoryStream = new MemoryStream(cached))
				{
					cachedProducts = dataContractSerializer.ReadObject(memoryStream) as IEnumerable<Product>;
				}

				Assert.IsNotNull(cached);
				Assert.IsNotNull(cachedProducts);
				Assert.AreEqual(products.Count(), cachedProducts.Count());
			}
		}

		/// <summary>
		/// Scenario Checks that cached products will be removed after <see cref="DateTimeOffset" /> expiration starts
		/// Given Created instance of the <see cref="ProductsRedis" /> class
		/// When Gets collection of the <see cref="Product" /> by the first time and gets again after a period of time
		/// Then There will no cached products in the Redis cache
		/// </summary>
		[TestMethod]
		public void RemovingProductsFromRedis_Expiration()
		{
			//Arrange
			const string configuration = "localhost";
			const int exirationSeconds = 3;
			var productsManager =
				new ProductsManager(new ProductsRedis(TimeSpan.FromSeconds(exirationSeconds), configuration));
			string userName = Thread.CurrentPrincipal.Identity.Name;
			string key = ProductsCache.CacheKeyPrefix + userName;

			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();

				if (database.KeyExists(key))
				{
					database.KeyDelete(key);
				}
			}

			//Act
			var products = productsManager.Products;
			Thread.Sleep(TimeSpan.FromSeconds(exirationSeconds*2));

			//Assert
			using (var connection = ConnectionMultiplexer.Connect(configuration))
			{
				var database = connection.GetDatabase();

				byte[] cached = database.StringGet(ProductsCache.CacheKeyPrefix + userName);

				Assert.IsNull(cached);
			}
		}
	}
}