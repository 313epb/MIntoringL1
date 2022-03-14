using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Order detail.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.OrderDetails.Name)]
	public class OrderDetail
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.OrderDetails.Columns.OrderId)]
		[PrimaryKey(1)]
		[NotNull]
		public int OrderId { get; set; }

		/// <summary>
		/// Product identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.OrderDetails.Columns.ProductId)]
		[PrimaryKey(2)]
		[NotNull]
		public int ProductId { get; set; }

		/// <summary>
		/// Unit price.
		/// </summary>
		[Column(NorthwindConstants.Tables.OrderDetails.Columns.UnitPrice)]
		[NotNull]
		public decimal UnitPrice { get; set; }

		/// <summary>
		/// Quantity.
		/// </summary>
		[Column(NorthwindConstants.Tables.OrderDetails.Columns.Quantity)]
		[NotNull]
		public short Quantity { get; set; }

		/// <summary>
		/// Discount.
		/// </summary>
		[Column(NorthwindConstants.Tables.OrderDetails.Columns.Discount)]
		[NotNull]
		public float Discount { get; set; }

		/// <summary>
		/// Referenced order.
		/// </summary>
		[Association(ThisKey = nameof(OrderId), OtherKey = nameof(Models.Order.Id), CanBeNull = false)]
		public Order Order { get; set; }

		/// <summary>
		/// Referenced product.
		/// </summary>
		[Association(ThisKey = nameof(ProductId), OtherKey = nameof(Models.Product.Id), CanBeNull = false)]
		public Product Product { get; set; }
	}
}