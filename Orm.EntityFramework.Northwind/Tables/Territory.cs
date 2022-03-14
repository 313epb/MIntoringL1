using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class Territory
	{
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

		public Region Region { get; set; }

		public ICollection<Employee> Employees { get; set; }
	}
}