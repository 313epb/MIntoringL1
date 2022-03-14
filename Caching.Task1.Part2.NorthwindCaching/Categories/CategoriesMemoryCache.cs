using Caching.Task1.Part2.Northwind.Tables;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Caching.Task1.Part2.NorthwindCaching.Categories
{
	/// <summary>
	/// Implementation of the <see cref="ICategoriesCache" /> with <see cref="ObjectCache" />.
	/// </summary>
	public class CategoriesMemoryCache : ICategoriesCache
	{
		private const string Prefix = "Cache_Categories";

		private readonly ObjectCache _cache = MemoryCache.Default;

		/// <summary>
		/// Gets enumerable of the <see cref="Category" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		public IEnumerable<Category> GetCategories(string forUser)
		{
			return (IEnumerable<Category>) _cache.Get(Prefix + forUser);
		}

		/// <summary>
		/// Sets collection of the <see cref="Category" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="categories">Collection of the <see cref="Category" /> to set to cache source.</param>
		public void SetCategories(string forUser, IEnumerable<Category> categories)
		{
			_cache.Set(Prefix + forUser, categories, ObjectCache.InfiniteAbsoluteExpiration);
		}
	}
}