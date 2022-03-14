using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Employee.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Employees.Name)]
	public class Employee
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.EmployeeId)]
		[PrimaryKey]
		[Identity]
		public int Id { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.LastName)]
		[NotNull]
		public string LastName { get; set; }

		/// <summary>
		/// First name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.FirstName)]
		[NotNull]
		public string FirstName { get; set; }

		/// <summary>
		/// Title.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.Title)]
		[Nullable]
		public string Title { get; set; }

		/// <summary>
		/// Title of courtesy.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.TitleOfCourtesy)]
		[Nullable]
		public string TitleOfCourtesy { get; set; }

		/// <summary>
		/// Birth date.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.BirthDate)]
		[Nullable]
		public DateTime? Birthdate { get; set; }

		/// <summary>
		/// Hire date.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.HireDate)]
		[Nullable]
		public DateTime? HireDate { get; set; }

		/// <summary>
		/// Address.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.Address)]
		[Nullable]
		public string Address { get; set; }

		/// <summary>
		/// City.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.City)]
		[Nullable]
		public string City { get; set; }

		/// <summary>
		/// Region.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.Region)]
		[Nullable]
		public string Region { get; set; }

		/// <summary>
		/// Postal code.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.PostalCode)]
		[Nullable]
		public string PostalCode { get; set; }

		/// <summary>
		/// Country.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.Country)]
		[Nullable]
		public string Country { get; set; }

		/// <summary>
		/// Home phone.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.HomePhone)]
		[Nullable]
		public string HomePhone { get; set; }

		/// <summary>
		/// Extension.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.Extension)]
		[Nullable]
		public string Extension { get; set; }

		/// <summary>
		/// Notes.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.Notes)]
		[Nullable]
		public string Notes { get; set; }

		/// <summary>
		/// Employee manager.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.ReportsTo)]
		[Nullable]
		public int? ReportsTo { get; set; }

		/// <summary>
		/// Photo path.
		/// </summary>
		[Column(NorthwindConstants.Tables.Employees.Columns.PhotoPath)]
		[Nullable]
		public string PhotoPath { get; set; }

		/// <summary>
		/// Reports to employee.
		/// </summary>
		[Association(ThisKey = nameof(ReportsTo), OtherKey = nameof(Id), CanBeNull = true)]
		public Employee ReportsToEmployee { get; set; }

		/// <summary>
		/// Reported employees.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(ReportsTo), CanBeNull = false)]
		public IEnumerable<Employee> ReportedEmployees { get; set; }

		/// <summary>
		/// Referenced orders.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Order.EmployeeId), CanBeNull = false)]
		public IEnumerable<Order> Orders { get; set; }

		/// <summary>
		/// Referenced employee territories.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(EmployeeTerritory.EmployeeId), CanBeNull = false)]
		public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
	}
}