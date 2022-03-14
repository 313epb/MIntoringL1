using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	[Table("Regions")]
	public sealed class Region
	{
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

		public ICollection<Territory> Territories { get; set; }
	}
}