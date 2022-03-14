using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class Category
	{
		public Category()
		{
			Products = new HashSet<Product>();
		}

		[Column("CategoryID")]
		public int Id { get; set; }

		[Required]
		[StringLength(15)]
		public string CategoryName { get; set; }

		[Column(TypeName = "ntext")]
		public string Description { get; set; }

		[Column(TypeName = "image")]
		public byte[] Picture { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}