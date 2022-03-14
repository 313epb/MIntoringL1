// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Globalization;
using System.Linq;
using Task.Data;
using Task.LinqExecutors;
using Task.Properties;

// Version Mad01

namespace Task
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{
		private const string GroupingOperatorsCategoryName = "Grouping Operators";

		private readonly DataSource _dataSource = new DataSource();

		[Category(GroupingOperatorsCategoryName)]
		[Title("Task1")]
		[Description("Annual statistics (numbers of orders) of clients by months (without years).")]
		public void Linq1()
		{
			var linqExecutor = new MonthCustomerStatisticsExecutor();
			var groupedByMonthCustomersOrders = linqExecutor.Execute(_dataSource.Customers);

			foreach (var group in groupedByMonthCustomersOrders)
			{
				Console.WriteLine(string.Format(CultureInfo.CurrentCulture, Resources.CustomerFormat,
					group.Customer.CustomerId, group.Customer.CompanyName));

				foreach (var groupedOrder in group.GroupedOrders)
				{
					Console.WriteLine(Resources.CustomerOrderCountByFormat,
						CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[groupedOrder.Key - 1] ??
						groupedOrder.Key.ToString(), groupedOrder.Count());
				}

				Console.WriteLine();
			}
		}

		[Category(GroupingOperatorsCategoryName)]
		[Title("Task2")]
		[Description("Statistics (number of orders) of clients by years.")]
		public void Linq2()
		{
			var linqExecutor = new YearCustomerStatisticsExecutor();
			var groupedByYearCustomersOrders = linqExecutor.Execute(_dataSource.Customers);

			foreach (var group in groupedByYearCustomersOrders)
			{
				Console.WriteLine(string.Format(CultureInfo.CurrentCulture, Resources.CustomerFormat,
					group.Customer.CustomerId, group.Customer.CompanyName));

				foreach (var groupedOrder in group.GroupedOrders)
				{
					Console.WriteLine(Resources.CustomerOrderCountByFormat, groupedOrder.Key, groupedOrder.Count());
				}

				Console.WriteLine();
			}
		}

		[Category(GroupingOperatorsCategoryName)]
		[Title("Task3")]
		[Description("Statistics (number of orders) of clients by years and months.")]
		public void Linq3()
		{
			var linqExecutor = new MonthAndYearCustomerStatisticsExecutor();

			var groupedByMonthAndYearCustomersOrders = linqExecutor.Execute(_dataSource.Customers);

			foreach (var group in groupedByMonthAndYearCustomersOrders)
			{
				Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Customer {0} from {1}",
					group.Customer.CustomerId,
					group.Customer.CompanyName));

				foreach (var groupedOrder in group.GroupedOrders)
				{
					Console.WriteLine(Resources.CustomerOrderCountByFormat,
						new DateTime(groupedOrder.Key.Year, groupedOrder.Key.Month, 1).ToString("MMMM yyyy"),
						groupedOrder.Count());
				}

				Console.WriteLine();
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq4()
		{
			int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine(Resources.NumbersLessThanFiveMessage);
			foreach (int x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return all presented in market products.")]
		public void Linq5()
		{
			var products =
				from p in _dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}
	}
}