namespace Orm.MicroOrm
{
	internal static class NorthwindConstants
	{
		internal const string DefaultScheme = "dbo";

		internal static class Tables
		{
			internal static class Categories
			{
				internal const string Name = "Categories";

				internal static class Columns
				{
					internal const string CategoryId = "CategoryID";
					internal const string CategoryName = "CategoryName";
					internal const string Description = "Description";
				}
			}

			internal static class Customers
			{
				internal const string Name = "Customers";

				internal static class Columns
				{
					internal const string CustomerId = "CustomerID";
					internal const string CompanyName = "CompanyName";
					internal const string ContactName = "ContactName";
					internal const string ContactTitle = "ContactTitle";
					internal const string Address = "Address";
					internal const string City = "City";
					internal const string Region = "Region";
					internal const string PostalCode = "PostalCode";
					internal const string Country = "Country";
					internal const string Phone = "Phone";
					internal const string Fax = "Fax";
				}
			}

			internal static class Employees
			{
				internal const string Name = "Employees";

				internal static class Columns
				{
					internal const string EmployeeId = "EmployeeID";
					internal const string LastName = "LastName";
					internal const string FirstName = "FirstName";
					internal const string Title = "Title";
					internal const string TitleOfCourtesy = "TitleOfCourtesy";
					internal const string BirthDate = "BirthDate";
					internal const string HireDate = "HireDate";
					internal const string Address = "Address";
					internal const string City = "City";
					internal const string Region = "Region";
					internal const string PostalCode = "PostalCode";
					internal const string Country = "Country";
					internal const string HomePhone = "HomePhone";
					internal const string Extension = "Extension";
					internal const string Notes = "Notes";
					internal const string ReportsTo = "ReportsTo";
					internal const string PhotoPath = "PhotoPath";
				}
			}

			internal static class Products
			{
				internal const string Name = "Products";

				internal static class Columns
				{
					internal const string ProductId = "ProductID";
					internal const string ProductName = "ProductName";
					internal const string SupplierId = "SupplierID";
					internal const string CategoryId = "CategoryID";
					internal const string QuantityPerUnit = "QuantityPerUnit";
					internal const string UnitPrice = "UnitPrice";
					internal const string UnitsInStock = "UnitsInStock";
					internal const string UnitsOnOrder = "UnitsOnOrder";
					internal const string ReorderLevel = "ReorderLevel";
					internal const string Discontinued = "Discontinued";
				}
			}

			internal static class Shippers
			{
				internal const string Name = "Shippers";

				internal static class Columns
				{
					internal const string ShipperId = "ShipperID";
					internal const string CompanyName = "CompanyName";
					internal const string Phone = "Phone";
				}
			}

			internal static class Orders
			{
				internal const string Name = "Orders";

				internal static class Columns
				{
					internal const string OrderId = "OrderID";
					internal const string CustomerId = "CustomerID";
					internal const string EmployeeId = "EmployeeID";
					internal const string OrderDate = "OrderDate";
					internal const string RequiredDate = "RequiredDate";
					internal const string ShippedDate = "ShippedDate";
					internal const string ShipVia = "ShipVia";
					internal const string Freight = "Freight";
					internal const string ShipName = "ShipName";
					internal const string ShipAddress = "ShipAddress";
					internal const string ShipCity = "ShipCity";
					internal const string ShipRegion = "ShipRegion";
					internal const string ShipPostalCode = "ShipPostalCode";
					internal const string ShipCountry = "ShipCountry";
				}
			}

			internal static class OrderDetails
			{
				internal const string Name = "Order Details";

				internal static class Columns
				{
					internal const string OrderId = "OrderID";
					internal const string ProductId = "ProductID";
					internal const string UnitPrice = "UnitPrice";
					internal const string Quantity = "Quantity";
					internal const string Discount = "Discount";
				}
			}

			internal static class Region
			{
				internal const string Name = "Region";

				internal static class Columns
				{
					internal const string RegionId = "RegionID";
					internal const string RegionDescription = "RegionDescription";
				}
			}

			internal static class Territories
			{
				internal const string Name = "Territories";

				internal static class Columns
				{
					internal const string TerritoryId = "TerritoryID";
					internal const string TerritoryDescription = "TerritoryDescription";
					internal const string RegionId = "RegionID";
				}
			}

			internal static class Suppliers
			{
				internal const string Name = "Suppliers";

				internal static class Columns
				{
					internal const string SupplierId = "SupplierID";
					internal const string CompanyName = "CompanyName";
					internal const string ContactName = "ContactName";
					internal const string ContactTitle = "ContactTitle";
					internal const string Address = "Address";
					internal const string City = "City";
					internal const string Region = "Region";
					internal const string PostalCode = "PostalCode";
					internal const string Country = "Country";
					internal const string Phone = "Phone";
					internal const string Fax = "Fax";
					internal const string HomePage = "HomePage";
				}
			}

			internal static class CustomerCustomerDemo
			{
				internal const string Name = "CustomerCustomerDemo";

				internal static class Columns
				{
					internal const string CustomerId = "CustomerID";
					internal const string CustomerTypeId = "CustomerTypeID";
				}
			}

			internal static class CustomerDemographics
			{
				internal const string Name = "CustomerDemographics";

				internal static class Columns
				{
					internal const string CustomerTypeId = "CustomerTypeID";
					internal const string CustomerDescription = "CustomerDesc";
				}
			}

			internal static class EmployeeTerritories
			{
				internal const string Name = "EmployeeTerritories";

				internal static class Columns
				{
					internal const string EmployeeId = "EmployeeID";
					internal const string TerritoryId = "TerritoryID";
				}
			}
		}
	}
}