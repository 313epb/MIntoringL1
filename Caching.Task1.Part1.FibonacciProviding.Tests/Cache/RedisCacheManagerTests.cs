using Caching.Task1.FibonacciProviding.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Caching.Task1.FibonacciProviding.Tests.Cache
{
	[TestClass]
	public class RedisCacheManagerTests
	{
		/// <summary>
		/// Scenario Creates a new instance of the <see cref="RedisCacheManager" /> class with invalid configuration
		/// Given Invalid value for configuration
		/// When Creates a new instance of the <see cref="RedisCacheManager" /> class
		/// Then Throws an exception of the <see cref="ArgumentNullException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void RedisCacheManagerTest()
		{
			//Arrange
			const string configuration = "   ";

			//Act
			var redisCacheManager = new RedisCacheManager(configuration);

			//Assert is handled by exception
		}
	}
}