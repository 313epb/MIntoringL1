using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using Orm.MicroOrm.Models;

namespace Orm.MicroOrm
{
	/// <summary>
	/// Connection to the Northwind database.
	/// </summary>
	public class NorthwindDataConnection : DataConnection
	{
		/// <summary>
		/// Initializes a new instance of the type <see cref="NorthwindDataConnection" />.
		/// </summary>
		/// <param name="configuration">Configuration string.</param>
		public NorthwindDataConnection(string configuration) : base(configuration)
		{
		}

		/// <summary>
		/// Initializes a new instance of the type <see cref="NorthwindDataConnection" />.
		/// </summary>
		/// <param name="configuration">Configuration string.</param>
		/// <param name="allowMultipleQuery">Allow multiple query.</param>
		public NorthwindDataConnection(string configuration, bool allowMultipleQuery) : base(configuration)
		{
			Configuration.Linq.AllowMultipleQuery = allowMultipleQuery;
		}

		/// <summary>
		/// Gets categories.
		/// </summary>
		public ITable<Category> Categories => GetTable<Category>();

		/// <summary>
		/// Gets customers.
		/// </summary>
		public ITable<Customer> Customers => GetTable<Customer>();

		/// <summary>
		/// Gets employees.
		/// </summary>
		public ITable<Employee> Employees => GetTable<Employee>();

		/// <summary>
		/// Gets products.
		/// </summary>
		public ITable<Product> Products => GetTable<Product>();

		/// <summary>
		/// Gets shippers.
		/// </summary>
		public ITable<Shipper> Shippers => GetTable<Shipper>();

		/// <summary>
		/// Gets order details.
		/// </summary>
		public ITable<OrderDetail> OrderDetails => GetTable<OrderDetail>();

		/// <summary>
		/// Gets orders.
		/// </summary>
		public ITable<Order> Orders => GetTable<Order>();

		/// <summary>
		/// Gets territories.
		/// </summary>
		public ITable<Territory> Territories => GetTable<Territory>();

		/// <summary>
		/// Gets suppliers.
		/// </summary>
		public ITable<Supplier> Suppliers => GetTable<Supplier>();

		/// <summary>
		/// Gets employee territories.
		/// </summary>
		public ITable<EmployeeTerritory> EmployeeTerritories => GetTable<EmployeeTerritory>();
	}
}