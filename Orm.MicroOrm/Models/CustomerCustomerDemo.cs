using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Cutomer customer demo.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.CustomerCustomerDemo.Name)]
	public class CustomerCustomerDemo
	{
		/// <summary>
		/// Customer identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.CustomerCustomerDemo.Columns.CustomerId)]
		[PrimaryKey(1)]
		[NotNull]
		public string CustomerId { get; set; }

		/// <summary>
		/// Customer type identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.CustomerCustomerDemo.Columns.CustomerTypeId)]
		[PrimaryKey(2)]
		[NotNull]
		public string CustomerTypeId { get; set; }

		/// <summary>
		/// Referenced customer demographic.
		/// </summary>
		[Association(ThisKey = nameof(CustomerTypeId), OtherKey = nameof(Models.CustomerDemographic.CustomerTypeId),
			CanBeNull = false)]
		public CustomerDemographic CustomerDemographic { get; set; }
	}
}