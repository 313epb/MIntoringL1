using System.Collections.Generic;

namespace Orm.MicroOrm.Models.Statistics
{
	/// <summary>
	/// Employee information about served customers.
	/// </summary>
	public class EmployeeCustomersList
	{
		/// <summary>
		/// Employee.
		/// </summary>
		public string EmployeeName { get; set; }

		/// <summary>
		/// Served customers.
		/// </summary>
		public IEnumerable<string> Customers { get; set; }
	}
}