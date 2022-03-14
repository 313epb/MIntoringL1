using System.Collections.Generic;
using Orm.EntityFramework.DataAccess.Models;

namespace Orm.EntityFramework.DataAccess
{
	/// <summary>
	/// Data access repository to the Northwind source of data.
	/// </summary>
	public interface INorthwindRepository
	{
		/// <summary>
		/// Gets order information for the specified category identifier.
		/// </summary>
		/// <param name="categoryId">Category identifier.</param>
		/// <returns>Enumerates orders information.</returns>
		IEnumerable<OrderInfo> GetOrdersInfoByCategoryId(int categoryId);
	}
}