using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.Data;
using Task.LinqExecutors;

namespace Linq.BasicLinqOperators.LinqSamples.Tests.LinqExecutors
{
	[TestClass]
	public class MonthAndYearCustomerStatisticsExecutorTests
	{
		/// <summary>
		/// Scenario Executes grouping by a month and a year customer statistics
		/// Given A list of customers information
		/// When Executes <see cref="MonthAndYearCustomerStatisticsExecutor.Execute" />
		/// Then Customer orders group count must be correct
		/// </summary>
		[TestMethod]
		public void ExecuteTest()
		{
			//Arrange
			var customers = new List<Customer>
			{
				new Customer
				{
					CustomerId = "ALFKI",
					Country = "Germany",
					Orders = new[]
					{
						new Order
						{
							OrderDate = new DateTime(1990, 1, 1)
						},
						new Order
						{
							OrderDate = new DateTime(1990, 1, 2)
						},
						new Order
						{
							OrderDate = new DateTime(1990, 2, 1)
						},
						new Order
						{
							OrderDate = new DateTime(1990, 2, 1)
						},
						new Order
						{
							OrderDate = new DateTime(2000, 2, 1)
						},
						new Order
						{
							OrderDate = new DateTime(2001, 2, 1)
						},
						new Order
						{
							OrderDate = new DateTime(2001, 2, 4)
						}
					}
				}
			};
			var linqExecutor = new MonthAndYearCustomerStatisticsExecutor();

			//Act
			int groupCount = linqExecutor.Execute(customers).First().GroupedOrders.Count();

			//Assert
			Assert.IsTrue(groupCount == 4);
		}
	}
}