using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caching.Task1.Part2.Northwind.Tables
{
	[Table("Order Details")]
	public class OrderDetail
	{
		[Key]
		[Column("OrderID", Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int OrderId { get; set; }

		[Column("ProductID", Order = 1)]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ProductId { get; set; }

		[Column(TypeName = "money")]
		public decimal UnitPrice { get; set; }

		public short Quantity { get; set; }

		public float Discount { get; set; }

		public virtual Order Order { get; set; }

		public virtual Product Product { get; set; }
	}
}