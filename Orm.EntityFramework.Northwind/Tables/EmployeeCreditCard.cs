using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orm.EntityFramework.Northwind.Tables
{
	public class EmployeeCreditCard
	{
		[Key]
		[Column(Order = 0)]
		[StringLength(16)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string CardNumber { get; set; }

		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int EmployeeId { get; set; }

		public DateTime ExpirationDate { get; set; }

		public string CardHolderName { get; set; }

		public virtual Employee Employee { get; set; }
	}
}