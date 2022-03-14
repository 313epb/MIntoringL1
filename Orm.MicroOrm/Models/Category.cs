using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Category.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Categories.Name)]
	public class Category
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Categories.Columns.CategoryId)]
		[PrimaryKey]
		[Identity]
		public int Id { get; set; }

		/// <summary>
		/// Category name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Categories.Columns.CategoryName)]
		[NotNull]
		public string Name { get; set; }

		/// <summary>
		/// Category description.
		/// </summary>
		[Column(NorthwindConstants.Tables.Categories.Columns.Description)]
		[Nullable]
		public string Description { get; set; }

		/// <summary>
		/// Referenced products.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(Product.CategoryId), CanBeNull = false)]
		public IEnumerable<Product> Products { get; set; }
	}
}