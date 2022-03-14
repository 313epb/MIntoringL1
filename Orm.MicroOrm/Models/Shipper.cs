using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Shipper.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Shippers.Name)]
	public class Shipper
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Shippers.Columns.ShipperId)]
		[PrimaryKey]
		[Identity]
		public int Id { get; set; }

		/// <summary>
		/// Company name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Shippers.Columns.CompanyName)]
		[NotNull]
		public string CompanyName { get; set; }

		/// <summary>
		/// Phone.
		/// </summary>
		[Column(NorthwindConstants.Tables.Shippers.Columns.Phone)]
		[Nullable]
		public string Phone { get; set; }

		/// <summary>
		/// Referenced orders.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Order.ShipVia), CanBeNull = false)]
		public IEnumerable<Order> Orders { get; set; }
	}
}