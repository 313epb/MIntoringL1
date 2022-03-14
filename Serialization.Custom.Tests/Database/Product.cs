using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Task.Database
{
	[Serializable]
	public class Product : ISerializable
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Product()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}

		public Product(SerializationInfo info, StreamingContext context)
		{
		}

		[Column("ProductID")]
		[DataMember]
		public int ProductId { get; set; }

		[Required]
		[StringLength(40)]
		public string ProductName { get; set; }

		[Column("SupplierID")]
		public int? SupplierId { get; set; }

		[Column("CategoryID")]
		public int? CategoryId { get; set; }

		[StringLength(20)]
		public string QuantityPerUnit { get; set; }

		[Column(TypeName = "money")]
		public decimal? UnitPrice { get; set; }

		public short? UnitsInStock { get; set; }

		public short? UnitsOnOrder { get; set; }

		public short? ReorderLevel { get; set; }

		public bool Discontinued { get; set; }

		public virtual Category Category { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }

		public virtual Supplier Supplier { get; set; }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			var northwindContext = context.Context as NorthwindContext;

			if (northwindContext == null)
			{
				return;
			}

			info.AddValue(nameof(ProductId), ProductId);
			info.AddValue(nameof(ProductName), ProductName);
			info.AddValue(nameof(SupplierId), SupplierId);
			info.AddValue(nameof(CategoryId), CategoryId);
			info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
			info.AddValue(nameof(UnitPrice), UnitPrice);
			info.AddValue(nameof(UnitsInStock), UnitsInStock);
			info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);
			info.AddValue(nameof(ReorderLevel), ReorderLevel);
			info.AddValue(nameof(Discontinued), Discontinued);

			if (Category == null)
			{
				Category = northwindContext.Categories.SingleOrDefault(c => c.CategoryId == CategoryId);
			}

			info.AddValue(nameof(Category), Category);

			if (SupplierId.HasValue)
			{
				var supplier = northwindContext.Suppliers.SingleOrDefault(s => s.SupplierId == SupplierId);
				info.AddValue(nameof(Supplier), supplier);
			}

			if (OrderDetails.Count == 0)
			{
				OrderDetails = northwindContext.OrderDetails.Where(od => od.ProductId == ProductId).ToList();
				info.AddValue(nameof(OrderDetails), OrderDetails);
			}
		}
	}
}