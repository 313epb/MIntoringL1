using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo.Tests
{
	[TestClass]
	public class CodeAuthorInfoProviderExtensionsTests
	{
		/// <summary>
		/// Scenario Tries to remove items from null source collection
		/// Given Null collection
		/// When Calls the extension <see cref="CodeAuthorInfoProviderExtensions.DistinctInfo" />
		/// Then Throws an <see cref="ArgumentNullException" /> exception
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DistinctInfo_NullSource()
		{
			//Arrange

			//Act
			((List<CodeAuthorInfoAttribute>) null).DistinctInfo();

			//Assert is handled by exception
		}

		/// <summary>
		/// Scenario Tries to remove items from collection of unique items of the type <see cref="CodeAuthorInfoAttribute" />
		/// Given A collection of the <see cref="CodeAuthorInfoAttribute" /> items with 3 items
		/// When Calls the extension <see cref="CodeAuthorInfoProviderExtensions.DistinctInfo" />
		/// Then The collection contains all the same 3 items
		/// </summary>
		[TestMethod]
		public void DistinctInfo_AllUniqueItems()
		{
			//Arrange
			var source = new List<CodeAuthorInfoAttribute>
			{
				new CodeAuthorInfoAttribute("Ivan Ivanov", "ivan.ivanov@outlook.com"),
				new CodeAuthorInfoAttribute("Ivan Petrov", "ivan.petrov@outlook.com"),
				new CodeAuthorInfoAttribute("Aleksey Alekseev", "aleksey.alekseev@outlook.com")
			};

			//Act
			var distinct = source.DistinctInfo().ToList();

			//Assert
			Assert.IsTrue(distinct.Count == source.Count);
		}

		/// <summary>
		/// Scenario Removes not unique items from collection of the <see cref="CodeAuthorInfoAttribute" /> items
		/// Given A collection of the <see cref="CodeAuthorInfoAttribute" /> items with 3 items
		/// When Calls the extension <see cref="CodeAuthorInfoProviderExtensions.DistinctInfo" />
		/// Then The collection contains only unique items with 2 items
		/// </summary>
		[TestMethod]
		public void DistinctInfo_NotAllUniqueItems()
		{
			//Arrange
			var source = new List<CodeAuthorInfoAttribute>
			{
				new CodeAuthorInfoAttribute("Ivan Ivanov", "ivan.ivanov@outlook.com"),
				new CodeAuthorInfoAttribute("Ivan Ivanov", "ivan.ivanov@outlook.com"),
				new CodeAuthorInfoAttribute("Aleksey Alekseev", "aleksey.alekseev@outlook.com")
			};

			//Act
			var distinct = source.DistinctInfo().ToList();

			//Assert
			Assert.IsTrue(distinct.Count == 2);
		}
	}
}