using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Order.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Orders.Name)]
	public class Order
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.OrderId)]
		[PrimaryKey]
		[Identity]
		public int Id { get; set; }

		/// <summary>
		/// Customer identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.CustomerId)]
		[Nullable]
		public string CustomerId { get; set; }

		/// <summary>
		/// Employee identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.EmployeeId)]
		[Nullable]
		public int? EmployeeId { get; set; }

		/// <summary>
		/// Order date.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.OrderId)]
		[Nullable]
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// Required date.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.RequiredDate)]
		[Nullable]
		public DateTime? RequiredDate { get; set; }

		/// <summary>
		/// Shipped date.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShippedDate)]
		[Nullable]
		public DateTime? ShippedDate { get; set; }

		/// <summary>
		/// Ship via.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipVia)]
		[Nullable]
		public int? ShipVia { get; set; }

		/// <summary>
		/// Freight.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.Freight)]
		[Nullable]
		public decimal? Freight { get; set; }

		/// <summary>
		/// Ship name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipName)]
		[Nullable]
		public string ShipName { get; set; }

		/// <summary>
		/// Ship address.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipAddress)]
		[Nullable]
		public string ShipAddress { get; set; }

		/// <summary>
		/// Ship city.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipCity)]
		[Nullable]
		public string ShipCity { get; set; }

		/// <summary>
		/// Ship region.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipRegion)]
		[Nullable]
		public string ShipRegion { get; set; }

		/// <summary>
		/// Ship postal code.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipPostalCode)]
		[Nullable]
		public string ShipPostalCode { get; set; }

		/// <summary>
		/// Ship country.
		/// </summary>
		[Column(NorthwindConstants.Tables.Orders.Columns.ShipCountry)]
		[Nullable]
		public string ShipCountry { get; set; }

		/// <summary>
		/// Referenced customer.
		/// </summary>
		[Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Models.Customer.Id), CanBeNull = true)]
		public Customer Customer { get; set; }

		/// <summary>
		/// Referenced employee.
		/// </summary>
		[Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(Models.Employee.Id), CanBeNull = true)]
		public Employee Employee { get; set; }

		/// <summary>
		/// Referenced shipper.
		/// </summary>
		[Association(ThisKey = nameof(ShipVia), OtherKey = nameof(Models.Shipper.Id), CanBeNull = true)]
		public Shipper Shipper { get; set; }

		/// <summary>
		/// Referenced order details.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(OrderDetail.OrderId), CanBeNull = false)]
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}