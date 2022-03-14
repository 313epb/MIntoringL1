using Caching.Task1.Part2.Northwind;
using Caching.Task1.Part2.Northwind.Tables;
using Caching.Task1.Part2.NorthwindCaching.Customers.CustomersCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Caching.Task1.Part2.NorthwindCaching.Customers
{
	/// <summary>
	/// <see cref="Customer" /> entities cache manager.
	/// </summary>
	public class CustomersManager
	{
		private readonly ICustomersCache _cache;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomersManager" />.
		/// </summary>
		/// <param name="cache">Implementation of the <see cref="ICustomersCache" />.</param>
		public CustomersManager(ICustomersCache cache)
		{
			if (cache == null)
			{
				throw new ArgumentNullException(nameof(cache));
			}

			_cache = cache;
		}

		/// <summary>
		/// Gets Customers from the <see cref="ICustomersCache" /> repository.
		/// </summary>
		public IEnumerable<Customer> Customers
		{
			get
			{
				string userName = Thread.CurrentPrincipal.Identity.Name;
				var customers = _cache.GetCustomers(userName)?.ToList();

				if (customers != null)
				{
					return customers;
				}

				using (var northwindContext = new NorthwindContext())
				{
					northwindContext.Configuration.LazyLoadingEnabled = false;
					northwindContext.Configuration.ProxyCreationEnabled = false;

					customers = northwindContext.Customers.ToList();

					_cache.SetCustomers(userName, customers);
				}

				return Customers;
			}
		}
	}
}