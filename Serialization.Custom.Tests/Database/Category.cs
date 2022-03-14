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
	public class Category : ISerializable
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Category()
		{
			Products = new HashSet<Product>();
		}

		public Category(SerializationInfo info, StreamingContext context)
		{
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

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(CategoryId), CategoryId);
			info.AddValue(nameof(CategoryName), CategoryName);
			info.AddValue(nameof(Description), Description);
			info.AddValue(nameof(Products), Products);
		}

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			var northwindContext = context.Context as NorthwindContext;

			if (northwindContext == null)
			{
				return;
			}

			Products = northwindContext.Products.Where(p => p.CategoryId == CategoryId).ToList();
		}
	}
}