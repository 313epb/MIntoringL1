using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Custom demographic.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.CustomerDemographics.Name)]
	public class CustomerDemographic
	{
		/// <summary>
		/// Customer type identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.CustomerDemographics.Columns.CustomerTypeId)]
		[PrimaryKey]
		[NotNull]
		public string CustomerTypeId { get; set; }

		/// <summary>
		/// Customer description.
		/// </summary>
		[Column(NorthwindConstants.Tables.CustomerDemographics.Columns.CustomerDescription)]
		[Nullable]
		public string CustomerDescription { get; set; }

		/// <summary>
		/// Referenced customer customer demos.
		/// </summary>
		[Association(ThisKey = nameof(CustomerTypeId), OtherKey = nameof(CustomerCustomerDemo.CustomerTypeId),
			CanBeNull = false)]
		public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
	}
}