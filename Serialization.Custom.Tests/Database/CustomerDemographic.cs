using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Task.Database
{
	public class CustomerDemographic
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public CustomerDemographic()
		{
			Customers = new HashSet<Customer>();
		}

		[Key]
		[StringLength(10)]
		[Column("CustomerTypeID")]
		public string CustomerTypeId { get; set; }

		[Column(TypeName = "ntext")]
		public string CustomerDesc { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Customer> Customers { get; set; }
	}
}