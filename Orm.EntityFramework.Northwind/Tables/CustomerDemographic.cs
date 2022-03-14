using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class CustomerDemographic
	{
		public CustomerDemographic()
		{
			Customers = new HashSet<Customer>();
		}

		[Column("CustomerTypeID")]
		[Key]
		[StringLength(10)]
		public string CustomerTypeId { get; set; }

		[Column(TypeName = "ntext")]
		public string CustomerDesc { get; set; }

		public ICollection<Customer> Customers { get; set; }
	}
}