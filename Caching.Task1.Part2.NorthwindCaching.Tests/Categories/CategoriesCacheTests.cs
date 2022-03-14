using Caching.Task1.Part2.NorthwindCaching.Categories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace Caching.Task1.Part2.Tests.Categories
{
	[TestClass]
	public class CategoriesCacheTests
	{
		[TestMethod]
		public void CategoriesCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesMemoryCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.Categories.Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void CategoriesRedis()
		{
			var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.Categories.Count());
				Thread.Sleep(100);
			}
		}
	}
}