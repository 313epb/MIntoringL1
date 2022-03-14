using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Task.Database
{
	public class Order
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Order()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}

		[Column("OrderID")]
		public int OrderId { get; set; }

		[Column("CustomerID")]
		[StringLength(5)]
		public string CustomerId { get; set; }

		[Column("EmployeeID")]
		public int? EmployeeId { get; set; }

		public DateTime? OrderDate { get; set; }

		public DateTime? RequiredDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public int? ShipVia { get; set; }

		[Column(TypeName = "money")]
		public decimal? Freight { get; set; }

		[StringLength(40)]
		public string ShipName { get; set; }

		[StringLength(60)]
		public string ShipAddress { get; set; }

		[StringLength(15)]
		public string ShipCity { get; set; }

		[StringLength(15)]
		public string ShipRegion { get; set; }

		[StringLength(10)]
		public string ShipPostalCode { get; set; }

		[StringLength(15)]
		public string ShipCountry { get; set; }

		public virtual Customer Customer { get; set; }

		public virtual Employee Employee { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }

		public virtual Shipper Shipper { get; set; }
	}
}