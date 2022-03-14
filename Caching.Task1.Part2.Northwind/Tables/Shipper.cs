using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Caching.Task1.Part2.Northwind.Tables
{
	public class Shipper
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Order> Orders { get; set; }
	}
}