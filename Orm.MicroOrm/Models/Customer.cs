using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Customer.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Customers.Name)]
	public class Customer
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.CustomerId)]
		[PrimaryKey]
		[NotNull]
		public string Id { get; set; }

		/// <summary>
		/// Company name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.CompanyName)]
		[NotNull]
		public string CompanyName { get; set; }

		/// <summary>
		/// Contact name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.ContactName)]
		[Nullable]
		public string ContactName { get; set; }

		/// <summary>
		/// Contact title.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.ContactTitle)]
		[Nullable]
		public string ContactTitle { get; set; }

		/// <summary>
		/// Address.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.Address)]
		[Nullable]
		public string Address { get; set; }

		/// <summary>
		/// City.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.City)]
		[Nullable]
		public string City { get; set; }

		/// <summary>
		/// Region.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.Region)]
		[Nullable]
		public string Region { get; set; }

		/// <summary>
		/// Postal code.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.PostalCode)]
		[Nullable]
		public string PostalCode { get; set; }

		/// <summary>
		/// Country.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.Country)]
		[Nullable]
		public string Country { get; set; }

		/// <summary>
		/// Phone.
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.Phone)]
		[Nullable]
		public string Phone { get; set; }

		/// <summary>
		/// </summary>
		[Column(NorthwindConstants.Tables.Customers.Columns.Fax)]
		[Nullable]
		public string Fax { get; set; }

		/// <summary>
		/// Referenced orders.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Order.CustomerId), CanBeNull = false)]
		public IEnumerable<Order> Orders { get; set; }

		/// <summary>
		/// Referenced customer customer demos.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(CustomerCustomerDemo.CustomerId), CanBeNull = false)]
		public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
	}
}