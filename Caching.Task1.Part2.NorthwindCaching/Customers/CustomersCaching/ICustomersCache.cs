using Caching.Task1.Part2.Northwind.Tables;
using System.Collections.Generic;

namespace Caching.Task1.Part2.NorthwindCaching.Customers.CustomersCaching
{
	/// <summary>
	/// Represents <see cref="Customer" /> cache repository.
	/// </summary>
	public interface ICustomersCache
	{
		/// <summary>
		/// Gets enumerable of the <see cref="Customer" /> class from cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <returns></returns>
		IEnumerable<Customer> GetCustomers(string forUser);

		/// <summary>
		/// Sets collection of the <see cref="Customer" /> to cache source.
		/// </summary>
		/// <param name="forUser">User name cache will be stored for.</param>
		/// <param name="customers">Collection of the <see cref="Customer" /> to set to cache source.</param>
		void SetCustomers(string forUser, IEnumerable<Customer> customers);
	}
}