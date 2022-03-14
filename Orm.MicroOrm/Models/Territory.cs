using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Territory.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Territories.Name)]
	public class Territory
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Territories.Columns.TerritoryId)]
		[PrimaryKey]
		[NotNull]
		public string Id { get; set; }

		/// <summary>
		/// Territory description.
		/// </summary>
		[Column(NorthwindConstants.Tables.Territories.Columns.TerritoryDescription)]
		[NotNull]
		public string Description { get; set; }

		/// <summary>
		/// Region identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Territories.Columns.RegionId)]
		[NotNull]
		public int RegionId { get; set; }

		/// <summary>
		/// Referenced region.
		/// </summary>
		[Association(ThisKey = nameof(RegionId), OtherKey = nameof(Models.Region.Id), CanBeNull = false)]
		public Region Region { get; set; }

		/// <summary>
		/// Referenced employee territories.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(EmployeeTerritory.TerritoryId
			), CanBeNull = false)]
		public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
	}
}