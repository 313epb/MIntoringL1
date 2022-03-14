using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orm.EntityFramework.Northwind;

namespace Orm.EntityFramework.DataAccess.Tests
{
	[TestClass]
	public class NorthwindRepositoryTests
	{
		private const string ConnectionString = "Orm.Northwind";

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="NorthwindRepository" /> class with the valid connection string
		/// Given Valid connection string
		/// When Calls the constructor of the <see cref="NorthwindRepository" /> class
		/// Then The instance of the <see cref="NorthwindRepository" /> will be created
		/// </summary>
		[TestMethod]
		public void NorthwindRepository_ValidConnectionString()
		{
			//Arrange
			const string connectionString = ConnectionString;

			//Act
			var northwindRepository = new NorthwindRepository(connectionString);

			//Assert
			Assert.IsNotNull(northwindRepository);
		}

		/// <summary>
		/// Scenario Creates a new instance of the <see cref="NorthwindRepository" /> class with the invalid connection string
		/// Given Invalid (empty of contains white spaces string) connection string
		/// When Calls the constructor of the <see cref="NorthwindRepository" /> class
		/// Then Throws an exception of the <see cref="ArgumentException" /> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NorthwindRepository_InvalidConnectionString()
		{
			//Arrange
			const string connectionString = "  ";

			//Act
			var northwindRepository = new NorthwindRepository(connectionString);

			//Assert is handled by the exception
		}

		/// <summary>
		/// Scenario Gets order information by the <see cref="NorthwindRepository.GetOrdersInfoByCategoryId" /> by the not existing
		/// category identifier
		/// Given Not existing category identifier
		/// When Gets order information from the databases
		/// Then Returns an empty list of order information
		/// </summary>
		[TestMethod]
		public void GetOrdersInfoByCategoryId_NotExistingCategory()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(ConnectionString);
			int notExistingCategoryId;

			using (var dbContext = new NorthwindDbContext(ConnectionString))
			{
				notExistingCategoryId = dbContext.Categories.Max(c => c.Id) + 1;
			}

			//Act
			var orders = northwindRepository.GetOrdersInfoByCategoryId(notExistingCategoryId);

			//Assert
			Assert.IsNotNull(orders);
			Assert.IsTrue(orders.ToList().Count == 0);
		}

		/// <summary>
		/// Scenario Gets order information by the <see cref="NorthwindRepository.GetOrdersInfoByCategoryId" /> by the existing
		/// category identifier
		/// Given Existing category identifier
		/// When Gets order information from the database
		/// Then Returns a not empty list of order information
		/// </summary>
		[TestMethod]
		public void GetOrdersInfoByCategoryId_ExistingCategory()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(ConnectionString);
			const int categoryId = 1;

			//Act
			var orders = northwindRepository.GetOrdersInfoByCategoryId(categoryId).ToList();

			//Assert
			Assert.IsTrue(orders.Count != 0);

			foreach (var order in orders)
			{
				Trace.WriteLine($"Order customer name: {order.CustomerName}");

				Trace.WriteLine("Order details:");
				foreach (var orderDetail in order.OrderDetails)
				{
					Trace.WriteLine(
						$"Quantity: {orderDetail.Quantity}, UnitPrice: {orderDetail.UnitPrice}, Discount: {orderDetail.Discount}");
				}

				Trace.WriteLine("Product names:");
				foreach (string productName in order.ProductNames)
				{
					Trace.WriteLine(productName);
				}

				Trace.WriteLine(string.Empty);
			}
		}
	}
}