using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Caching.Task1.Part2.Northwind.Tables
{
	public class Category
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Category()
		{
			Products = new HashSet<Product>();
		}

		[Column("CategoryID")]
		public int CategoryId { get; set; }

		[Required]
		[StringLength(15)]
		public string CategoryName { get; set; }

		[Column(TypeName = "ntext")]
		public string Description { get; set; }

		[Column(TypeName = "image")]
		public byte[] Picture { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Product> Products { get; set; }
	}
}