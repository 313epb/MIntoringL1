using Caching.Task1.Part2.Northwind.Tables;
using System.Collections.Generic;

namespace Caching.Task1.Part2.NorthwindCaching.Products.ProductsCaching
{
	/// <summary>
	/// Represents <see cref="Product" /> cache repository.
	/// </summary>
	public interface IProductsCache
	{
		/// <summary>
		/// Gets enumerable of the <see cref="Product" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		IEnumerable<Product> GetProducts(string forUser);

		/// <summary>
		/// Sets collection of the <see cref="Product" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="products">Collection of the <see cref="Product" /> to set to cache source.</param>
		void SetProducts(string forUser, IEnumerable<Product> products);
	}
}