using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Orm.EntityFramework.DataAccess.Models;
using Orm.EntityFramework.Northwind;

namespace Orm.EntityFramework.DataAccess
{
	/// <summary>
	/// Data access repository to the Northwind database.
	/// </summary>
	public class NorthwindRepository : INorthwindRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NorthwindRepository" /> class.
		/// </summary>
		/// <param name="connectionString">Connection string</param>
		/// <exception cref="ArgumentException" />
		public NorthwindRepository(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException("The specified connection string is invalid.", nameof(connectionString));
			}

			ConnectionString = connectionString;
		}

		/// <summary>
		/// Connection string to the Northwind database.
		/// </summary>
		public string ConnectionString { get; }

		/// <summary>
		/// Gets order information for the specified category identifier.
		/// </summary>
		/// <param name="categoryId">Category identifier.</param>
		/// <returns>Enumerates orders information.</returns>
		public IEnumerable<OrderInfo> GetOrdersInfoByCategoryId(int categoryId)
		{
			var orderInfos = new List<OrderInfo>();

			using (var dbContext = new NorthwindDbContext(ConnectionString))
			{
				var orderDetailGroups =
					dbContext.OrderDetails.Where(od => od.Product.CategoryId == categoryId).GroupBy(od => od.OrderId);

				foreach (var orderDetailGroup in orderDetailGroups)
				{
					var productNames = new HashSet<string>();
					var orderDetailInfos = new List<OrderDetailInfo>();

					foreach (var orderDetail in orderDetailGroup)
					{
						productNames.Add(orderDetail.Product.ProductName);
						orderDetailInfos.Add(new OrderDetailInfo
						{
							Quantity = orderDetail.Quantity,
							UnitPrice = orderDetail.UnitPrice,
							Discount = orderDetail.Discount
						});
					}

					orderInfos.Add(new OrderInfo
					{
						OrderDetails = orderDetailInfos,
						ProductNames = productNames,
						CustomerName =
							dbContext.Orders.Include(o => o.Customer)
								.SingleOrDefault(o => o.Id == orderDetailGroup.Key)?.Customer?.ContactName
					});
				}
			}

			return orderInfos;
		}
	}
}