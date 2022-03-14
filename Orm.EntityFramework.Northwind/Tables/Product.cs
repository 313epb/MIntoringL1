using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class Product
	{
		public Product()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}

		[Column("ProductID")]
		public int Id { get; set; }

		[Required]
		[StringLength(40)]
		public string ProductName { get; set; }

		[Column("SupplierId")]
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

		public Category Category { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; }

		public Supplier Supplier { get; set; }
	}
}