using Caching.Task1.Part2.Northwind;
using Caching.Task1.Part2.Northwind.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Caching.Task1.Part2.NorthwindCaching.Categories
{
	/// <summary>
	/// <see cref="Category" /> entities cache manager.
	/// </summary>
	public class CategoriesManager
	{
		private readonly ICategoriesCache _cache;

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoriesManager" /> class.
		/// </summary>
		/// <param name="cache">Implementation of the <see cref="ICategoriesCache" />.</param>
		public CategoriesManager(ICategoriesCache cache)
		{
			_cache = cache;
		}

		/// <summary>
		/// Gets categories from the <see cref="ICategoriesCache" />.
		/// </summary>
		/// <returns>Retrieved categories.</returns>
		public IEnumerable<Category> Categories
		{
			get
			{
				string user = Thread.CurrentPrincipal.Identity.Name;
				var categories = _cache.GetCategories(user)?.ToList();

				if (categories == null)
				{
					using (var dbContext = new NorthwindContext())
					{
						dbContext.Configuration.LazyLoadingEnabled = false;
						dbContext.Configuration.ProxyCreationEnabled = false;
						categories = dbContext.Categories.ToList();
						_cache.SetCategories(user, categories);
					}
				}

				return categories;
			}
		}
	}
}