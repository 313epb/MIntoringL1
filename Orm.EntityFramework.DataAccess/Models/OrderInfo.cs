using System.Collections.Generic;

namespace Orm.EntityFramework.DataAccess.Models
{
	/// <summary>
	/// Order information.
	/// </summary>
	public class OrderInfo
	{
		/// <summary>
		/// Order details.
		/// </summary>
		public IEnumerable<OrderDetailInfo> OrderDetails { get; set; }

		/// <summary>
		/// Customer name.
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// Product names.
		/// </summary>
		public IEnumerable<string> ProductNames { get; set; }
	}
}