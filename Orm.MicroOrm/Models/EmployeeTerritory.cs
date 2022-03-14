using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Employee territory.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.EmployeeTerritories.Name)]
	public class EmployeeTerritory
	{
		/// <summary>
		/// Employee identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.EmployeeTerritories.Columns.EmployeeId)]
		[PrimaryKey(1)]
		[NotNull]
		public int EmployeeId { get; set; }

		/// <summary>
		/// Territory identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.EmployeeTerritories.Columns.TerritoryId)]
		[PrimaryKey(2)]
		[NotNull]
		public string TerritoryId { get; set; }

		/// <summary>
		/// Referenced employee.
		/// </summary>
		[Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(Models.Employee.Id), CanBeNull = false)]
		public Employee Employee { get; set; }

		/// <summary>
		/// Referenced territory.
		/// </summary>
		[Association(ThisKey = nameof(TerritoryId), OtherKey = nameof(Models.Territory.Id), CanBeNull = false)]
		public Territory Territory { get; set; }
	}
}