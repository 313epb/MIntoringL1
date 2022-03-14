using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.Data;
using Task.LinqExecutors;

namespace Linq.BasicLinqOperators.LinqSamples.Tests.LinqExecutors
{
	[TestClass]
	public class MonthCustomerStatisticsExecutorTests
	{
		/// <summary>
		/// Scenario Executes grouping by month customer statistics
		/// Given A list of customers information
		/// When Executes <see cref="MonthCustomerStatisticsExecutor.Execute" />
		/// Then Grouped customer orders must be correct
		/// </summary>
		[TestMethod]
		public void GroupCustomerOrderCountByMonth()
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
							OrderDate = new DateTime(2000, 1, 1)
						},
						new Order
						{
							OrderDate = new DateTime(2000, 1, 2)
						},
						new Order
						{
							OrderDate = new DateTime(2000, 2, 1)
						},
						new Order
						{
							OrderDate = new DateTime(2000, 1, 4)
						}
					}
				}
			};
			var linqExecutor = new MonthCustomerStatisticsExecutor();

			//Act
			int groupCount = linqExecutor.Execute(customers).First().GroupedOrders.Count();

			//Assert
			Assert.IsTrue(groupCount == 2);
		}
	}
}