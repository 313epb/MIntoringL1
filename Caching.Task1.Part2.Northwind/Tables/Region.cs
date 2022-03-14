using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Caching.Task1.Part2.Northwind.Tables
{
	[Table("Region")]
	public class Region
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Region()
		{
			Territories = new HashSet<Territory>();
		}

		[Column("RegionID")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int RegionId { get; set; }

		[Required]
		[StringLength(50)]
		public string RegionDescription { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Territory> Territories { get; set; }
	}
}