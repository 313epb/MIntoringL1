using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Task.Database
{
	[Serializable]
	public class Supplier : ISerializable
	{
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Supplier()
		{
			Products = new HashSet<Product>();
		}

		public Supplier(SerializationInfo info, StreamingContext context)
		{
		}

		[Column("SupplierID")]
		public int SupplierId { get; set; }

		[Required]
		[StringLength(40)]
		public string CompanyName { get; set; }

		[StringLength(30)]
		public string ContactName { get; set; }

		[StringLength(30)]
		public string ContactTitle { get; set; }

		[StringLength(60)]
		public string Address { get; set; }

		[StringLength(15)]
		public string City { get; set; }

		[StringLength(15)]
		public string Region { get; set; }

		[StringLength(10)]
		public string PostalCode { get; set; }

		[StringLength(15)]
		public string Country { get; set; }

		[StringLength(24)]
		public string Phone { get; set; }

		[StringLength(24)]
		public string Fax { get; set; }

		[Column(TypeName = "ntext")]
		public string HomePage { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Product> Products { get; set; }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(SupplierId), SupplierId);
			info.AddValue(nameof(CompanyName), CompanyName);
			info.AddValue(nameof(ContactName), ContactName);
			info.AddValue(nameof(ContactTitle), ContactTitle);
			info.AddValue(nameof(Address), Address);
			info.AddValue(nameof(City), City);
			info.AddValue(nameof(Region), Region);
			info.AddValue(nameof(PostalCode), PostalCode);
			info.AddValue(nameof(Country), Country);
			info.AddValue(nameof(Phone), Phone);
			info.AddValue(nameof(Fax), Fax);
			info.AddValue(nameof(HomePage), HomePage);
		}
	}
}