using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Task.Database
{
	[Table("Order Details")]
	[Serializable]
	public class OrderDetail : ISerializable
	{
		public OrderDetail()
		{
		}

		public OrderDetail(SerializationInfo info, StreamingContext context)
		{
		}

		[Key]
		[Column("OrderID", Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int OrderId { get; set; }

		[Key]
		[Column("ProductID", Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ProductId { get; set; }

		[Column(TypeName = "money")]
		public decimal UnitPrice { get; set; }

		public short Quantity { get; set; }

		public float Discount { get; set; }

		public virtual Order Order { get; set; }

		public virtual Product Product { get; set; }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(OrderId), OrderId);
			info.AddValue(nameof(ProductId), ProductId);
			info.AddValue(nameof(UnitPrice), UnitPrice);
			info.AddValue(nameof(Quantity), Quantity);
			info.AddValue(nameof(Discount), Discount);
		}
	}
}