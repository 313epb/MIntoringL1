using Caching.Task1.Part2.Northwind.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Caching;

namespace Caching.Task1.Part2.NorthwindCaching.Products.ProductsCaching
{
	/// <summary>
	/// Implementation of the <see cref="IProductsCache" /> interface using <see cref="ObjectCache" />.
	/// </summary>
	public class ProductsCache : IProductsCache
	{
		/// <summary>
		/// Cache key prefix.
		/// </summary>
		public const string CacheKeyPrefix = "Cache_Products";

		private readonly ObjectCache _cache = MemoryCache.Default;

		private readonly string _connection;

		private readonly DateTimeOffset _expiration;

		private bool _hasChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductsCache" /> class.
		/// </summary>
		public ProductsCache()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductsCache" /> class.
		/// </summary>
		/// <param name="expiration">Expiration date time.</param>
		/// <param name="connection">Connection string.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public ProductsCache(DateTimeOffset expiration, string connection)
		{
			if (expiration < DateTimeOffset.Now)
			{
				throw new ArgumentOutOfRangeException(nameof(expiration), expiration,
					"Expiration date time must be greater then now.");
			}

			_expiration = expiration;
			_connection = connection;

			if (!string.IsNullOrWhiteSpace(connection))
			{
				SqlDependency.Start(connection);
			}
		}

		/// <summary>
		/// Gets enumerable of the <see cref="Product" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns>Enumerable of the <see cref="Product" />.</returns>
		public IEnumerable<Product> GetProducts(string forUser)
		{
			if (_hasChanged)
			{
				return null;
			}

			return (IEnumerable<Product>) _cache.Get(CacheKeyPrefix + forUser);
		}

		/// <summary>
		/// Sets collection of the <see cref="Product" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="products">Collection of the <see cref="Product" /> to set to cache source.</param>
		public void SetProducts(string forUser, IEnumerable<Product> products)
		{
			var cacheItemPolicy = new CacheItemPolicy
			{
				AbsoluteExpiration = _expiration == default(DateTimeOffset) ? ObjectCache.InfiniteAbsoluteExpiration : _expiration
			};

			if (!string.IsNullOrWhiteSpace(_connection))
			{
				using (var sqlConnection = new SqlConnection(_connection))
				{
					sqlConnection.Open();

					using (var sqlCommand = sqlConnection.CreateCommand())
					{
						sqlCommand.CommandType = CommandType.Text;
						sqlCommand.Notification = null;
						sqlCommand.CommandText = "SELECT TOP 1000 [ProductID],[ProductName],[SupplierID]," +
												"[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock]," +
												"[UnitsOnOrder],[ReorderLevel],[Discontinued] " +
												"FROM [Caching.Task1.Part2.Northwind].[dbo].[Products]";

						var sqlDependency = new SqlDependency();

						sqlDependency.AddCommandDependency(sqlCommand);
						sqlDependency.OnChange += (sender, args) => { _hasChanged = true; };

						cacheItemPolicy.ChangeMonitors.Add(new SqlChangeMonitor(sqlDependency));

						sqlCommand.ExecuteReader();
					}
				}
			}

			_cache.Set(CacheKeyPrefix + forUser, products, cacheItemPolicy);
		}
	}
}