namespace Orm.EntityFramework.DataAccess.Models
{
	/// <summary>
	/// Order detail information.
	/// </summary>
	public class OrderDetailInfo
	{
		/// <summary>
		/// Unit price.
		/// </summary>
		public decimal UnitPrice { get; set; }

		/// <summary>
		/// Quantity.
		/// </summary>
		public short Quantity { get; set; }

		/// <summary>
		/// Discount.
		/// </summary>
		public float Discount { get; set; }
	}
}