using Caching.Task1.Part2.Northwind;
using Caching.Task1.Part2.Northwind.Tables;
using Caching.Task1.Part2.NorthwindCaching.Products.ProductsCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Caching.Task1.Part2.NorthwindCaching.Products
{
	/// <summary>
	/// <see cref="Product" /> entities cache manager.
	/// </summary>
	public class ProductsManager
	{
		private readonly IProductsCache _cache;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductsManager" />.
		/// </summary>
		/// <param name="cache">Implementation of the <see cref="IProductsCache" />.</param>
		public ProductsManager(IProductsCache cache)
		{
			if (cache == null)
			{
				throw new ArgumentNullException(nameof(cache));
			}

			_cache = cache;
		}

		/// <summary>
		/// Gets products from the <see cref="IProductsCache" /> repository.
		/// </summary>
		public IEnumerable<Product> Products
		{
			get
			{
				string userName = Thread.CurrentPrincipal.Identity.Name;
				var products = _cache.GetProducts(userName)?.ToList();

				if (products != null)
				{
					return products;
				}

				using (var northwindContext = new NorthwindContext())
				{
					northwindContext.Configuration.LazyLoadingEnabled = false;
					northwindContext.Configuration.ProxyCreationEnabled = false;

					products = northwindContext.Products.ToList();

					_cache.SetProducts(userName, products);
				}

				return products;
			}
		}
	}
}