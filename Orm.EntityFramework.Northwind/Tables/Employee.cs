using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public sealed class Employee
	{
		public Employee()
		{
			Employees1 = new HashSet<Employee>();
			Orders = new HashSet<Order>();
			Territories = new HashSet<Territory>();
		}

		[Column("EmployeeID")]
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string LastName { get; set; }

		[Required]
		[StringLength(10)]
		public string FirstName { get; set; }

		[StringLength(30)]
		public string Title { get; set; }

		[StringLength(25)]
		public string TitleOfCourtesy { get; set; }

		public DateTime? BirthDate { get; set; }

		public DateTime? HireDate { get; set; }

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
		public string HomePhone { get; set; }

		[StringLength(4)]
		public string Extension { get; set; }

		[Column(TypeName = "image")]
		public byte[] Photo { get; set; }

		[Column(TypeName = "ntext")]
		public string Notes { get; set; }

		public int? ReportsTo { get; set; }

		[StringLength(255)]
		public string PhotoPath { get; set; }

		public ICollection<Employee> Employees1 { get; set; }

		public Employee Employee1 { get; set; }

		public ICollection<Order> Orders { get; set; }

		public ICollection<Territory> Territories { get; set; }

		public ICollection<EmployeeCreditCard> EmployeeCreditCards { get; set; }
	}
}