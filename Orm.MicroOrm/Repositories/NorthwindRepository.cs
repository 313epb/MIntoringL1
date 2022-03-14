using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinqToDB;
using Orm.MicroOrm.Models;
using Orm.MicroOrm.Models.Statistics;

namespace Orm.MicroOrm.Repositories
{
	/// <summary>
	/// Tasks implementation repository.
	/// </summary>
	public class NorthwindRepository : INorthwindRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NorthwindRepository" /> class.
		/// </summary>
		public NorthwindRepository(string configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Connection string.
		/// </summary>
		public string Configuration { get; }

		/// <summary>
		/// Returns a list of products with a category and a supplier.
		/// </summary>
		/// <remarks>Part 2.1.</remarks>
		public IEnumerable<Product> ProductsWithCategoryAndSupplier
		{
			get
			{
				var products = new List<Product>();

				using (var dataConnection = new NorthwindDataConnection(Configuration))
				{
					products.AddRange(dataConnection.Products
						.LoadWith(p => p.Category)
						.LoadWith(p => p.Supplier));
				}

				return products;
			}
		}

		/// <summary>
		/// Returns a list of employees with a region.
		/// </summary>
		/// <remarks>Part 2.2.</remarks>
		public IEnumerable<Employee> EmployeesWithRegion
		{
			get
			{
				var employees = new List<Employee>();

				using (var dataConnection = new NorthwindDataConnection(Configuration, true))
				{
					employees.AddRange(dataConnection.Employees
						.LoadWith(e => e.EmployeeTerritories.ToArray()[0].Territory));
				}

				return employees;
			}
		}

		/// <summary>
		/// Returns a list of employees information by regions.
		/// </summary>
		/// <remarks>Part 2.3.</remarks>
		public IEnumerable<EmployeesByRegion> EmployeesByRegions
		{
			get
			{
				var employeesByRegions = new List<EmployeesByRegion>();

				using (var dataConnection = new NorthwindDataConnection(Configuration))
				{
					var employeeTerritories = dataConnection.EmployeeTerritories
						.LoadWith(t => t.Employee)
						.LoadWith(t => t.Territory)
						.GroupBy(t => t.TerritoryId);

					foreach (var employeeTerritory in employeeTerritories)
					{
						employeesByRegions.Add(new EmployeesByRegion
						{
							Region = employeeTerritory.First().Territory.Description,
							EmployeeCount = employeeTerritory.Count()
						});
					}
				}

				return employeesByRegions;
			}
		}

		/// <summary>
		/// Returns a list of employees served customers information.
		/// </summary>
		/// <remarks>Part 2.4.</remarks>
		public IEnumerable<EmployeeCustomersList> EmployeeCustomers
		{
			get
			{
				var employeeCustomers = new List<EmployeeCustomersList>();

				using (var dataConnection = new NorthwindDataConnection(Configuration, true))
				{
					var employeeWithOrdersAndCustomers = dataConnection.Employees
						.LoadWith(e => e.Orders.ToArray()[0].Customer);

					foreach (var employeeWithOrdersAndCustomer in employeeWithOrdersAndCustomers)
					{
						employeeCustomers.Add(new EmployeeCustomersList
						{
							EmployeeName =
								string.Format(CultureInfo.CurrentCulture, "{0} {1}", employeeWithOrdersAndCustomer.FirstName,
									employeeWithOrdersAndCustomer.LastName),
							Customers = employeeWithOrdersAndCustomer.Orders.Select(order => order.Customer.CompanyName).Distinct()
						});
					}
				}

				return employeeCustomers;
			}
		}

		/// <summary>
		/// Saves <paramref name="employee" /> to the collection of the <see cref="Employee" />.
		/// </summary>
		/// <param name="employee">Employee to be saved.</param>
		/// <returns>Saved employee identifier.</returns>
		/// <remarks>Part 3.1.</remarks>
		public int AddEmployee(Employee employee)
		{
			if (employee == null)
			{
				throw new ArgumentNullException(nameof(employee));
			}

			int employeeId;

			using (var dataConnection = new NorthwindDataConnection(Configuration, true))
			{
				employeeId = Convert.ToInt32(dataConnection.InsertWithIdentity(employee), CultureInfo.CurrentCulture);

				if (employee.EmployeeTerritories != null)
				{
					foreach (var employeeTerritory in employee.EmployeeTerritories)
					{
						if (!string.IsNullOrWhiteSpace(employeeTerritory.TerritoryId))
						{
							dataConnection.Insert(employeeTerritory);
							continue;
						}

						if (employeeTerritory.Territory == null)
						{
							throw new InvalidOperationException("There is no specified Territory info.");
						}
						
						int maxTerritoryId = dataConnection.Territories.Max(t => Convert.ToInt32(t.Id));
						employeeTerritory.Territory.Id = Convert.ToString(maxTerritoryId + 1, CultureInfo.CurrentCulture);

						dataConnection.Insert(employeeTerritory.Territory);

						employeeTerritory.TerritoryId = employeeTerritory.Territory.Id;
						employeeTerritory.EmployeeId = employeeId;

						dataConnection.Insert(employeeTerritory);
					}
				}
			}

			return employeeId;
		}

		/// <summary>
		/// Changes category of the products collection with the specified <paramref name="productIds" />.
		/// </summary>
		/// <param name="categoryId">Category identifier to be changed.</param>
		/// <param name="productIds">A list of the product identifiers to change the category.</param>
		/// <remarks>Part 3.2.</remarks>
		public void ChangeProductCategory(int categoryId, params int[] productIds)
		{
			if (productIds == null)
			{
				throw new ArgumentNullException(nameof(productIds));
			}

			if (productIds.Length == 0)
			{
				throw new ArgumentException("There are no specified product identifiers.", nameof(productIds));
			}

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				var category = dataConnection.Categories.SingleOrDefault(c => c.Id == categoryId);

				if (category == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
						"There is no any category with the identifier: {0}", categoryId));
				}

				dataConnection.Products.Where(p => productIds.Contains(p.Id)).Set(p => p.CategoryId, category.Id).Update();
			}
		}

		/// <summary>
		/// Adds a list of products with customers and suppliers.
		/// </summary>
		/// <param name="products">Colleciton of the <see cref="Product"/> to save.</param>
		/// <remarks>Part 3.3.</remarks>
		public IEnumerable<int> AddProducts(IEnumerable<Product> products)
		{
			if (products == null)
			{
				throw new ArgumentNullException(nameof(products));
			}

			products = products.ToList();
			var productIds = new List<int>();

			if (!products.Any())
			{
				throw new ArgumentException("The list of products is empty.", nameof(products));
			}

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				foreach (var product in products)
				{
					if (product.Supplier != null)
					{
						var existedSupplier = dataConnection.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
						int supplierId = existedSupplier?.Id ??
										Convert.ToInt32(dataConnection.InsertWithIdentity(product.Supplier), CultureInfo.CurrentCulture);

						product.SupplierId = supplierId;
					}

					if (product.Category != null)
					{
						var existedCategory = dataConnection.Categories.FirstOrDefault(c => c.Name == product.Category.Name);
						int categoryId = existedCategory?.Id ??
										Convert.ToInt32(dataConnection.InsertWithIdentity(product.Category), CultureInfo.CurrentCulture);

						product.CategoryId = categoryId;
					}

					productIds.Add(Convert.ToInt32(dataConnection.InsertWithIdentity(product), CultureInfo.CurrentCulture));
				}
			}

			return productIds;
		}

		/// <summary>
		/// Changes product on the unshipped orders
		/// </summary>
		/// <param name="productId">Product identifier.</param>
		/// <remarks>Part 3.4.</remarks>
		public void ChangeProductOnUnshippedOrders(int productId)
		{
			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				var product = dataConnection.Products.SingleOrDefault(p => p.Id == productId);

				if (product == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
						"There is no product with the specified identifier: {0}", productId));
				}

				dataConnection.OrderDetails.Where(od => od.Order.ShippedDate == null && od.ProductId == productId)
					.Set(od => od.Quantity, od => od.Quantity + 1)
					.Update();

				var otherProductOrderDetails =
					dataConnection.OrderDetails.Where(od => od.Order.ShippedDate == null && od.ProductId != productId).ToList();

				foreach (var otherProductOrderDetail in otherProductOrderDetails)
				{
					if (dataConnection.OrderDetails.Any(od => od.ProductId == productId && od.OrderId == otherProductOrderDetail.OrderId))
					{
						dataConnection.OrderDetails.Where(
							od => od.OrderId == otherProductOrderDetail.OrderId && od.ProductId == productId)
							.Set(od => od.Quantity, od => od.Quantity + 1)
							.Update();

						dataConnection.OrderDetails.Where(
							od => od.OrderId == otherProductOrderDetail.OrderId && od.ProductId == otherProductOrderDetail.ProductId)
							.Delete();
					}
					else
					{
						dataConnection.OrderDetails.Where(
							od => od.OrderId == otherProductOrderDetail.OrderId && od.ProductId == otherProductOrderDetail.ProductId)
							.Set(od => od.ProductId, productId)
							.Update();
					}
				}
			}
		}
	}
}