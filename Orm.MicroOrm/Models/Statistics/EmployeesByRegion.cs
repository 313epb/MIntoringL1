namespace Orm.MicroOrm.Models.Statistics
{
	/// <summary>
	/// Employees information by region.
	/// </summary>
	public class EmployeesByRegion
	{
		/// <summary>
		/// Employee count.
		/// </summary>
		public int EmployeeCount { get; set; }

		/// <summary>
		/// Region.
		/// </summary>
		public string Region { get; set; }
	}
}