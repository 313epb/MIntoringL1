using System.Collections.Generic;
using System.Linq;

namespace Task.Data
{
	/// <summary>
	/// Grouped customer order model by key <typeparamref name="TK" />.
	/// </summary>
	/// <typeparam name="TK"></typeparam>
	public class GroupedOrdersCustomers<TK>
	{
		/// <summary>
		/// Customer inforamtion.
		/// </summary>
		public Customer Customer { get; set; }

		/// <summary>
		/// Grouped ordery collection by specified key of the type <typeparamref name="TK" />.
		/// </summary>
		public IEnumerable<IGrouping<TK, Order>> GroupedOrders { get; set; }
	}
}