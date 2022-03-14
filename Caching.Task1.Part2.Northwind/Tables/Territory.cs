using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Caching.Task1.Part2.Northwind.Tables
{
	public class Territory
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Territory()
		{
			Employees = new HashSet<Employee>();
		}

		[Column("TerritoryID")]
		[StringLength(20)]
		public string TerritoryId { get; set; }

		[Required]
		[StringLength(50)]
		public string TerritoryDescription { get; set; }

		[Column("RegionID")]
		public int RegionId { get; set; }

		public virtual Region Region { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Employee> Employees { get; set; }
	}
}