using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class Shipper
	{
		public Shipper()
		{
			Orders = new HashSet<Order>();
		}

		[Column("ShipperID")]
		public int ShipperId { get; set; }

		[Required]
		[StringLength(40)]
		public string CompanyName { get; set; }

		[StringLength(24)]
		public string Phone { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}