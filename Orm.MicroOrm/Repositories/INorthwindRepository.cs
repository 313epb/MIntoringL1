using System.Collections.Generic;
using Orm.MicroOrm.Models;
using Orm.MicroOrm.Models.Statistics;

namespace Orm.MicroOrm.Repositories
{
	/// <summary>
	/// Tasks implementation repository.
	/// </summary>
	public interface INorthwindRepository
	{
		/// <summary>
		/// Returns a list of products with a category and a supplier.
		/// </summary>
		/// <remarks>Part 2.1.</remarks>
		IEnumerable<Product> ProductsWithCategoryAndSupplier { get; }

		/// <summary>
		/// Returns a list of employees with a region.
		/// </summary>
		/// <remarks>Part 2.2.</remarks>
		IEnumerable<Employee> EmployeesWithRegion { get; }

		/// <summary>
		/// Returns a list of employees information by regions.
		/// </summary>
		/// <remarks>Part 2.3.</remarks>
		IEnumerable<EmployeesByRegion> EmployeesByRegions { get; }

		/// <summary>
		/// Returns a list of employees served customers information.
		/// </summary>
		/// <remarks>Part 2.4.</remarks>
		IEnumerable<EmployeeCustomersList> EmployeeCustomers { get; }

		/// <summary>
		/// Saves <paramref name="employee" /> to the collection of the <see cref="Employee" />.
		/// </summary>
		/// <param name="employee">Employee to be saved.</param>
		/// <returns>Saved employee identifier.</returns>
		/// <remarks>Part 3.1.</remarks>
		int AddEmployee(Employee employee);

		/// <summary>
		/// Changes category of the products collection with the specified <paramref name="productIds" />.
		/// </summary>
		/// <param name="categoryId">Category identifier to be changed.</param>
		/// <param name="productIds">A list of the product identifiers to change the category.</param>
		/// <remarks>Part 3.2.</remarks>
		void ChangeProductCategory(int categoryId, params int[] productIds);

		/// <summary>
		/// Adds a list of products with customers and suppliers.
		/// </summary>
		/// <param name="products">Colleciton of the <see cref="Product"/> to save.</param>
		/// <remarks>Part 3.3.</remarks>
		IEnumerable<int> AddProducts(IEnumerable<Product> products);

		/// <summary>
		/// Changes product on the unshipped orders
		/// </summary>
		/// <param name="productId">Product identifier.</param>
		/// <remarks>Part 3.4.</remarks>
		void ChangeProductOnUnshippedOrders(int productId);
	}
}