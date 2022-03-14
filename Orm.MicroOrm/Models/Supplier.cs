using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Supplier.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Suppliers.Name)]
	public class Supplier
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.SupplierId)]
		[PrimaryKey]
		[Identity]
		public int Id { get; set; }

		/// <summary>
		/// Company name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.CompanyName)]
		[NotNull]
		public string CompanyName { get; set; }

		/// <summary>
		/// Contact name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.ContactName)]
		[Nullable]
		public string ContactName { get; set; }

		/// <summary>
		/// Contact title.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.ContactTitle)]
		[Nullable]
		public string ContactTitle { get; set; }

		/// <summary>
		/// Address.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.Address)]
		[Nullable]
		public string Address { get; set; }

		/// <summary>
		/// City.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.City)]
		[Nullable]
		public string City { get; set; }

		/// <summary>
		/// Region.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.Region)]
		[Nullable]
		public string Region { get; set; }

		/// <summary>
		/// Postal code.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.PostalCode)]
		[Nullable]
		public string PostalCode { get; set; }

		/// <summary>
		/// Country.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.Country)]
		[Nullable]
		public string Country { get; set; }

		/// <summary>
		/// Phone.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.Phone)]
		[Nullable]
		public string Phone { get; set; }

		/// <summary>
		/// Fax.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.Fax)]
		[Nullable]
		public string Fax { get; set; }

		/// <summary>
		/// Home page.
		/// </summary>
		[Column(NorthwindConstants.Tables.Suppliers.Columns.HomePage)]
		[Nullable]
		public string Homepage { get; set; }

		/// <summary>
		/// Referenced products.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Product.SupplierId), CanBeNull = false)]
		public IEnumerable<Product> Products { get; set; }
	}
}