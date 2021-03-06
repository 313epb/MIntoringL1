using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class Customer
	{
		public Customer()
		{
			Orders = new HashSet<Order>();
			CustomerDemographics = new HashSet<CustomerDemographic>();
		}

		[Column("CustomerID")]
		[StringLength(5)]
		public string Id { get; set; }

		[Required]
		[StringLength(40)]
		public string CompanyName { get; set; }

		public DateTime? EstablishmentDate { get; set; }

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

		public ICollection<Order> Orders { get; set; }

		public ICollection<CustomerDemographic> CustomerDemographics { get; set; }
	}
}