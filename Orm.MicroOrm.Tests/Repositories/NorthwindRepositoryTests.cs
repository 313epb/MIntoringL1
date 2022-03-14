using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using LinqToDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orm.MicroOrm.Models;
using Orm.MicroOrm.Repositories;

namespace Orm.MicroOrm.Tests.Repositories
{
	[TestClass]
	public class NorthwindRepositoryTests
	{
		private const string Configuration = "Orm.Northwind";
		
		/// <summary>
		/// Scenario Returns a list of products with category and supplier info
		/// Given Connecton string to the database
		/// When Calls <see cref="INorthwindRepository.ProductsWithCategoryAndSupplier" /> property
		/// Then Returnes not empty results
		/// </summary>
		[TestMethod]
		public void ProductsWithCategoryAndSupplier()
		{
			//Arrange
			var northwindRepository =
				new NorthwindRepository(ConfigurationManager.ConnectionStrings[Configuration].Name);

			//Act
			var products = northwindRepository.ProductsWithCategoryAndSupplier.ToList();

			//Assert
			Assert.IsTrue(products.Any());
			Assert.IsFalse(products.TrueForAll(p => p.Category == null));
			Assert.IsFalse(products.TrueForAll(p => p.Supplier == null));

			products.ForEach(
				p => { Trace.WriteLine($"Product: {p.Name}, Category: {p.Category?.Name}, Supplier: {p.Supplier?.ContactName}"); });
		}

		/// <summary>
		/// Scenario Returns a list of employees with regions information
		/// Given Connection string to the database
		/// When Calls <see cref="INorthwindRepository.EmployeesWithRegion" /> property
		/// Then Returnes not empty results
		/// </summary>
		[TestMethod]
		public void EmployeesWithRegion()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(ConfigurationManager.ConnectionStrings[Configuration].Name);

			//Act
			var employees = northwindRepository.EmployeesWithRegion.ToList();

			//Assert
			Assert.IsTrue(employees.Any());
			Assert.IsFalse(employees.TrueForAll(e => e.EmployeeTerritories == null || !e.EmployeeTerritories.Any()));

			employees.ForEach(e =>
			{
				Trace.WriteLine($"Employee: {e.FirstName} {e.LastName} is responsible for the regions:");
				e.EmployeeTerritories?.ToList().ForEach(et => { Trace.WriteLine(et.Territory.Description); });
				Trace.WriteLine(string.Empty);
			});
		}

		/// <summary>
		/// Scenario Returns a list of employees grouped by regions
		/// Given Connection string to the database
		/// When Calls <see cref="INorthwindRepository.EmployeesByRegions" /> property
		/// Then Returned not empty results
		/// </summary>
		[TestMethod]
		public void EmployeesByRegions()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(ConfigurationManager.ConnectionStrings[Configuration].Name);

			//Act
			var employeesByRegions = northwindRepository.EmployeesByRegions.ToList();

			//Assert
			Assert.IsTrue(employeesByRegions.Any());
			Assert.IsFalse(employeesByRegions.TrueForAll(ebr => string.IsNullOrWhiteSpace(ebr.Region)));

			employeesByRegions.ForEach(
				ebr => { Trace.WriteLine($"The region {ebr.Region} contains {ebr.EmployeeCount} employees"); });
		}

		/// <summary>
		/// Scenario Returnes a list of employees information with orders and customers information
		/// Given Connection string to the database
		/// When Calls <see cref="INorthwindRepository.EmployeeCustomers" /> property
		/// Then Returned not empty results
		/// </summary>
		[TestMethod]
		public void EmployeesCustomers()
		{
			//Arrange
			var nortwindRepository = new NorthwindRepository(ConfigurationManager.ConnectionStrings[Configuration].Name);

			//Act
			var employeeCustomers = nortwindRepository.EmployeeCustomers.ToList();

			//Assert
			Assert.IsTrue(employeeCustomers.Any());
			Assert.IsFalse(employeeCustomers.TrueForAll(ec => ec.Customers == null || !ec.Customers.Any()));

			employeeCustomers.ForEach(ec =>
			{
				Trace.WriteLine($"{ec.EmployeeName} has worked with following customers:");
				ec.Customers?.ToList().ForEach(c => { Trace.WriteLine(c); });
				Trace.WriteLine(string.Empty);
			});
		}

		/// <summary>
		/// Scenario Inserts an new employee to the database to the table of the <see cref="Employee" />
		/// Given Connection string to the database, an employee with two new Territory
		/// When Calls <see cref="INorthwindRepository.AddEmployee" />
		/// Then The employee must be saved to the database
		/// </summary>
		[TestMethod]
		public void AddEmployee()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			var firstTerritory = new Territory
			{
				Description = "Samara",
				RegionId = 1
			};
			var secondTerritory = new Territory
			{
				Description = "Volgograd",
				RegionId = 3
			};
			var employee = new Employee
			{
				Orders = new List<Order>(),
				LastName = "Ivanov",
				FirstName = "Ivan"
			};
			employee.EmployeeTerritories = new List<EmployeeTerritory>
			{
				new EmployeeTerritory
				{
					Employee = employee,
					Territory = firstTerritory
				},
				new EmployeeTerritory
				{
					Employee = employee,
					Territory = secondTerritory
				}
			};

			//Act
			int savedEmployeeId = northwindRepository.AddEmployee(employee);

			//Assert
			Assert.IsTrue(savedEmployeeId != default(int));

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				var savedEmployee =
					dataConnection.Employees.LoadWith(e => e.EmployeeTerritories.ToArray()[0].Territory)
						.SingleOrDefault(e => e.Id == savedEmployeeId);

				Assert.IsNotNull(savedEmployee);
				Assert.AreEqual(savedEmployee.FirstName, employee.FirstName);
				Assert.AreEqual(savedEmployee.LastName, employee.LastName);
				Assert.AreEqual(savedEmployee.Address, employee.Address);

				Assert.IsNotNull(savedEmployee.EmployeeTerritories);
				Assert.IsTrue(savedEmployee.EmployeeTerritories.ToList().TrueForAll(et => et.Territory != null));
				Assert.AreEqual(savedEmployee.EmployeeTerritories.Count(), employee.EmployeeTerritories.Count());
			}
		}



		/// <summary>
		/// Scenario Changes category identifier of the specified products
		/// Given Connection string to the database, old category identifier, new category identifier, two products
		/// When Calls <see cref="INorthwindRepository.ChangeProductCategory" />
		/// Then Changed category identifier of the specified products
		/// </summary>
		[TestMethod]
		public void ChangeProductCategory()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			const int oldCategoryId = 1;
			const int newCategoryId = 2;
			int[] productIds;

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				productIds =
					dataConnection.Products.Where(p => p.CategoryId == oldCategoryId)
						.Take(2)
						.Select(p => p.Id)
						.ToArray();
			}

			//Act
			northwindRepository.ChangeProductCategory(newCategoryId, productIds);

			//Assert
			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				foreach (int productId in productIds)
				{
					var product = dataConnection.Products.SingleOrDefault(p => p.Id == productId);
					Assert.IsNotNull(product);
					Assert.IsTrue(product.CategoryId == newCategoryId);
				}
			}
		}

		/// <summary>
		/// Scenario Changes not existing category identifier of the specified products
		/// Given Connection string to the database, not existing category identifier
		/// When Calls <see cref="INorthwindRepository.ChangeProductCategory" />
		/// Then Throws an exception of the <see cref="InvalidOperationException"/> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ChangeProductCategory_WithNotExistingCategory()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			int categoryId;

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				categoryId = dataConnection.Categories.Max(c => c.Id) + 1;
			}

			//Act
			northwindRepository.ChangeProductCategory(categoryId, 1, 2);

			//Assert is handled by the exception
		}

		/// <summary>
		/// Scenario Changes category identifier on not specified product identifiers
		/// Given Connection string to the database, not specified product identifiers
		/// When Calls <see cref="INorthwindRepository.ChangeProductCategory"/>
		/// Then Throws an exception of the <see cref="ArgumentException"/> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ChangeProductCategory_NotSpecifiedProductId()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);

			//Act
			northwindRepository.ChangeProductCategory(1);

			//Assert is hanbled by the exception
		}

		/// <summary>
		/// Scenario Adds a product with not existed category and supplier
		/// Given Connection to the database, a product with not existed category and supplier
		/// When Calls <see cref="INorthwindRepository.AddProducts" />
		/// Then The product will be saved, the new customer and new supplier will be added to the database
		/// </summary>
		[TestMethod]
		public void AddProducts_CreatesNewCustomerAndSupplier()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			var random = new Random();
			const int maxValue = 10000;
			string supplierCompanyName = "Test supplier " + random.Next(maxValue);
			string categoryName = "Test " + random.Next(maxValue);
			string productName = "Test product " + random.Next(maxValue);
			var supplier = new Supplier
			{
				CompanyName = supplierCompanyName
			};
			var category = new Category
			{
				Name = categoryName
			};
			var products = new List<Product>
			{
				new Product
				{
					Name = productName,
					Supplier = supplier,
					Category = category
				}
			};
			int categoryCount;
			int supplierCount;

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				categoryCount = dataConnection.Categories.Count();
				supplierCount = dataConnection.Suppliers.Count();
			}

			//Act
			var productIds = northwindRepository.AddProducts(products).ToList();
			Assert.IsNotNull(productIds);
			Assert.AreEqual(productIds.Count, 1);

			//Assert
			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				var savedProduct = dataConnection.Products
					.LoadWith(p => p.Category)
					.LoadWith(p => p.Supplier)
					.SingleOrDefault(p => p.Id == productIds.FirstOrDefault());
				Assert.IsNotNull(savedProduct);
				Assert.AreEqual(savedProduct.Name, productName);
				Assert.IsNotNull(savedProduct.Category);
				Assert.IsNotNull(savedProduct.Supplier);

				var savedCategory = dataConnection.Categories.SingleOrDefault(c => c.Id == savedProduct.Category.Id);
				Assert.IsNotNull(savedCategory);
				Assert.AreEqual(savedCategory.Name, categoryName);
				Assert.AreEqual(categoryCount + 1, dataConnection.Categories.Count());

				var savedSupplier = dataConnection.Suppliers.SingleOrDefault(s => s.Id == savedProduct.Supplier.Id);
				Assert.IsNotNull(savedSupplier);
				Assert.AreEqual(savedSupplier.CompanyName, supplierCompanyName);
				Assert.AreEqual(supplierCount + 1, dataConnection.Suppliers.Count());
			}
		}

		/// <summary>
		/// Scenario Adds a product with existed supplier and category
		/// Given Connection string to the database, a product with existed category and supplier
		/// When Calls <see cref="INorthwindRepository.AddProducts" />
		/// Then The product will be saved, but the category and the supplier will not be saved
		/// </summary>
		[TestMethod]
		public void AddProducts_WithExistedCustomerAndSupplier()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			const int maxValue = 10000;
			var random = new Random();
			const string categoryName = "Grains/Cereals";
			const string supplierCompanyName = "Karkki Oy";
			string productName = "Test product " + random.Next(maxValue);
			var supplier = new Supplier
			{
				CompanyName = supplierCompanyName
			};
			var category = new Category
			{
				Name = categoryName
			};
			var products = new List<Product>
			{
				new Product
				{
					Name = productName,
					Supplier = supplier,
					Category = category
				}
			};
			int categoryCount;
			int supplierCount;

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				categoryCount = dataConnection.Categories.Count();
				supplierCount = dataConnection.Suppliers.Count();
			}

			//Act
			var productIds = northwindRepository.AddProducts(products).ToList();
			Assert.IsNotNull(productIds);
			Assert.AreEqual(productIds.Count, 1);

			//Assert
			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				var savedProduct = dataConnection.Products
					.LoadWith(p => p.Category)
					.LoadWith(p => p.Supplier)
					.SingleOrDefault(p => p.Id == productIds.FirstOrDefault());
				Assert.IsNotNull(savedProduct);
				Assert.AreEqual(savedProduct.Name, productName);
				Assert.IsNotNull(savedProduct.Category);
				Assert.IsNotNull(savedProduct.Supplier);

				var savedCategory = dataConnection.Categories.SingleOrDefault(c => c.Id == savedProduct.Category.Id);
				Assert.IsNotNull(savedCategory);
				Assert.AreEqual(savedCategory.Name, categoryName);
				Assert.AreEqual(categoryCount, dataConnection.Categories.Count());

				var savedSupplier = dataConnection.Suppliers.SingleOrDefault(s => s.Id == savedProduct.Supplier.Id);
				Assert.IsNotNull(savedSupplier);
				Assert.AreEqual(savedSupplier.CompanyName, supplierCompanyName);
				Assert.AreEqual(supplierCount, dataConnection.Suppliers.Count());
			}
		}

		/// <summary>
		/// Scenario Changes identifier of not existing product on unshipped orders
		/// Given Connection string to the database, identifier of not existing product
		/// When Calls <see cref="INorthwindRepository.ChangeProductOnUnshippedOrders" />
		/// Then Throws an exception of the <see cref="InvalidOperationException"/> class
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ChangeProductOnUnshippedOrders_WithNotExistingProduct()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			int notExisingProductId;

			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				notExisingProductId = dataConnection.Products.Max(p => p.Id) + 1;
			}
			
			//Act
			northwindRepository.ChangeProductOnUnshippedOrders(notExisingProductId);

			//Assert is handled by the exception
		}

		/// <summary>
		/// Scenario Changes product identifier on unshipped orders
		/// Given Connection string to the database, random product identifier that not above products count
		/// When Calls <see cref="INorthwindRepository.ChangeProductOnUnshippedOrders" />
		/// Then Product identifier on all unshipped order details must be changed
		/// </summary>
		[TestMethod]
		public void ChangeProductOnUnshippedOrders_WithExistingProduct()
		{
			//Arrange
			var northwindRepository = new NorthwindRepository(Configuration);
			var random = new Random();
			int productId = random.Next(1, 70);

			//Act
			northwindRepository.ChangeProductOnUnshippedOrders(productId);

			//Assert
			using (var dataConnection = new NorthwindDataConnection(Configuration))
			{
				Assert.IsTrue(
					dataConnection.OrderDetails.Where(od => od.Order.ShippedDate == null)
						.ToList()
						.TrueForAll(od => od.ProductId == productId));
			}
		}
	}
}