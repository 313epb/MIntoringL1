using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Caching.Task1.Part2.Northwind.Tables
{
	public class Product
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Product()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}

		[Column("ProductID")]
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
	}
}