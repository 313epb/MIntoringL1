using System.Runtime.Serialization;
using Task.Database;

namespace Task.Serialization.SerializationSurrogates
{
	/// <summary>
	/// Implementes the <see cref="ISerializationSurrogate" /> to control serialization for the type <see cref="Product" />.
	/// </summary>
	public class ProductSerializationSurrogate : ISerializationSurrogate
	{
		/// <summary>
		/// Populates the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to
		/// serialize the object.
		/// </summary>
		/// <param name="obj">The object to serialize. </param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">
		/// The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this
		/// serialization.
		/// </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var product = obj as Product;

			if (product == null)
			{
				return;
			}

			info.AddValue(nameof(Product.ProductId), product.ProductId);
			info.AddValue(nameof(Product.ProductName), product.ProductName);
			info.AddValue(nameof(Product.SupplierId), product.SupplierId);
			info.AddValue(nameof(Product.CategoryId), product.CategoryId);
			info.AddValue(nameof(Product.QuantityPerUnit), product.QuantityPerUnit);
			info.AddValue(nameof(Product.UnitPrice), product.UnitPrice);
			info.AddValue(nameof(Product.UnitsInStock), product.UnitsInStock);
			info.AddValue(nameof(Product.UnitsOnOrder), product.UnitsOnOrder);
			info.AddValue(nameof(Product.ReorderLevel), product.ReorderLevel);
			info.AddValue(nameof(Product.Discontinued), product.Discontinued);
		}

		/// <summary>
		/// Populates the object using the information in the
		/// <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
		/// </summary>
		/// <returns>The populated deserialized object.</returns>
		/// <param name="obj">The object to populate. </param>
		/// <param name="info">The information to populate the object. </param>
		/// <param name="context">The source from which the object is deserialized. </param>
		/// <param name="selector">The surrogate selector where the search for a compatible surrogate begins. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			var product = obj as Product;

			if (product == null)
			{
				return null;
			}

			product.ProductId = info.GetInt32(nameof(Product.ProductId));
			product.ProductName = info.GetString(nameof(Product.ProductName));
			product.SupplierId = info.GetAs<int?>(nameof(Product.SupplierId));
			product.CategoryId = info.GetAs<int?>(nameof(Product.CategoryId));
			product.QuantityPerUnit = info.GetString(nameof(Product.QuantityPerUnit));
			product.UnitPrice = info.GetAs<decimal?>(nameof(Product.UnitPrice));
			product.UnitsOnOrder = info.GetAs<short?>(nameof(Product.UnitsOnOrder));
			product.ReorderLevel = info.GetAs<short?>(nameof(Product.ReorderLevel));
			product.Discontinued = info.GetBoolean(nameof(Product.Discontinued));
			product.ProductId = info.GetInt32(nameof(Product.ProductId));

			return product;
		}
	}
}