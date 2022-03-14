using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Region.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Region.Name)]
	public class Region
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Region.Columns.RegionId)]
		[PrimaryKey]
		[NotNull]
		public int Id { get; set; }

		/// <summary>
		/// Desciprtion.
		/// </summary>
		[Column(NorthwindConstants.Tables.Region.Columns.RegionDescription)]
		[NotNull]
		public string Description { get; set; }

		/// <summary>
		/// Referenced territories.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Territory.RegionId), CanBeNull = false)]
		public IEnumerable<Territory> Territories { get; set; }
	}
}