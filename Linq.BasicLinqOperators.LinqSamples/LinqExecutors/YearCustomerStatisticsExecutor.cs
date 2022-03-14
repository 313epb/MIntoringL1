using System;
using System.Collections.Generic;
using System.Linq;
using Task.Data;

namespace Task.LinqExecutors
{
	/// <summary>
	/// Executes statistics (number of orders) of clients by years.
	/// </summary>
	public class YearCustomerStatisticsExecutor :
		ILinqExecutor<IEnumerable<Customer>, IEnumerable<GroupedOrdersCustomers<int>>>
	{
		/// <summary>
		/// Execute a linq operation.
		/// </summary>
		/// <param name="source">Source data to execute.</param>
		/// <returns>Result data.</returns>
		public IEnumerable<GroupedOrdersCustomers<int>> Execute(IEnumerable<Customer> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			return from customer in source
				select new GroupedOrdersCustomers<int>
				{
					Customer = new Customer
					{
						Address = customer.Address,
						City = customer.City,
						CompanyName = customer.CompanyName,
						Country = customer.Country,
						CustomerId = customer.CustomerId,
						Fax = customer.Fax,
						Phone = customer.Phone,
						PostalCode = customer.PostalCode,
						Region = customer.Region
					},
					GroupedOrders = from order in customer.Orders
						group order by order.OrderDate.Year
						into groupedOrders
						orderby groupedOrders.Key
						select groupedOrders
				};
		}
	}
}