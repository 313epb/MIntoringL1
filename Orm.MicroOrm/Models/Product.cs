using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Orm.MicroOrm.Models
{
	/// <summary>
	/// Product.
	/// </summary>
	[Table(Schema = NorthwindConstants.DefaultScheme, Name = NorthwindConstants.Tables.Products.Name)]
	public class Product
	{
		/// <summary>
		/// Identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.ProductId)]
		[PrimaryKey]
		[Identity]
		public int Id { get; set; }

		/// <summary>
		/// Product name.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.ProductName)]
		[NotNull]
		public string Name { get; set; }

		/// <summary>
		/// Supplier identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.SupplierId)]
		[Nullable]
		public int? SupplierId { get; set; }

		/// <summary>
		/// Category identifier.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.CategoryId)]
		[Nullable]
		public int? CategoryId { get; set; }

		/// <summary>
		/// Quantity per unit.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.QuantityPerUnit)]
		[Nullable]
		public string QuantityPerUnit { get; set; }

		/// <summary>
		/// Unit price.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.UnitPrice)]
		[Nullable]
		public decimal? UnitPrice { get; set; }

		/// <summary>
		/// Units in stock.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.UnitsInStock)]
		[Nullable]
		public short? UnitsInStock { get; set; }

		/// <summary>
		/// Units on order.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.UnitsOnOrder)]
		[Nullable]
		public short? UnitsOnOrder { get; set; }

		/// <summary>
		/// Reorder level.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.ReorderLevel)]
		[Nullable]
		public short? ReorderLevel { get; set; }

		/// <summary>
		/// Discontinued flag.
		/// </summary>
		[Column(NorthwindConstants.Tables.Products.Columns.Discontinued)]
		[NotNull]
		public bool Discontinued { get; set; }

		/// <summary>
		/// Referenced category.
		/// </summary>
		[Association(ThisKey = nameof(CategoryId), OtherKey = nameof(Models.Category.Id), CanBeNull = true)]
		public Category Category { get; set; }

		/// <summary>
		/// Referenced supplier.
		/// </summary>
		[Association(ThisKey = nameof(SupplierId), OtherKey = nameof(Models.Supplier.Id), CanBeNull = true)]
		public Supplier Supplier { get; set; }

		/// <summary>
		/// Referenced order details.
		/// </summary>
		[Association(ThisKey = nameof(Id), OtherKey = nameof(OrderDetail.ProductId), CanBeNull = false)]
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}