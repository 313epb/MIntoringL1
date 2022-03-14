using Caching.Task1.Part2.Northwind.Tables;
using System.Collections.Generic;

namespace Caching.Task1.Part2.NorthwindCaching.Categories
{
	/// <summary>
	/// <see cref="Category" /> cache repository.
	/// </summary>
	public interface ICategoriesCache
	{
		/// <summary>
		/// Gets enumerable of the <see cref="Category" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		IEnumerable<Category> GetCategories(string forUser);

		/// <summary>
		/// Sets collection of the <see cref="Category" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="categories">Collection of the <see cref="Category" /> to set to cache source.</param>
		void SetCategories(string forUser, IEnumerable<Category> categories);
	}
}